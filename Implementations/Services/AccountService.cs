using OnlineCourses.Implementations.Interfaсes;
using OnlineCourses.Models.ViewModels.Account;
using OnlineCourses.Implementations.Helpers;
using OnlineCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain.Response;
using OnlineCourses.Domain.Enum;
using System.Security.Claims;
using OnlineCourses.Domain;

namespace OnlineCourses.Implementations.Services
{
	public class AccountService : IAccountService
	{
		private readonly AppDbContext _context;
		private readonly ILogger<AccountService> _logger;

		public AccountService(AppDbContext context, ILogger<AccountService> logger)
		{
			_context = context;
			_logger = logger;
		}
		public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
		{
			try
			{
				var user = await _context.User.FirstOrDefaultAsync(x => x.Email == model.Email);
				if (user != null)
				{
					_logger.LogDebug("Пользователь с такой почтой уже существует");

					return new BaseResponse<ClaimsIdentity>()
					{
						Description = "Пользователь с такой почтой уже существует"
					};
				}

				if (!Config.cyrillicPattern.IsMatch(model.UserName))
				{
					_logger.LogDebug("Пожалуйста, введите только кириллические символы для Имени");
					return new BaseResponse<ClaimsIdentity>()
					{
						Description = "Пожалуйста, введите только кириллические символы для Имени"
					};
				}

				user = new User()
				{
					UserName = model.UserName,
					Login = model.Login,
					Email = model.Email,
					Password = HashPasswordHelper.HashPassword(model.Password),
					Role = Role.User,
				};

				await _context.User.AddAsync(user);
				await _context.SaveChangesAsync();

				var result = Authenticate(user);

				return new BaseResponse<ClaimsIdentity>()
				{
					Data = result,
					Description = "Объект добавился",
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"[Register]: {ex.Message}");
				return new BaseResponse<ClaimsIdentity>()
				{
					Description = ex.Message,
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
		{
			try
			{
				var user = await _context.User.FirstOrDefaultAsync(x => x.Login == model.Login && x.Email == model.Email && x.Status == SubStatus.Open);
				if (user == null)
				{
					return new BaseResponse<ClaimsIdentity>()
					{
						Description = "Пользователь не найден"
					};
				}
				if (user.Password != HashPasswordHelper.HashPassword(model.Password))
				{
					return new BaseResponse<ClaimsIdentity>()
					{
						Description = "Неверный пароль, почта или логин"
					};
				}

				var result = Authenticate(user);

				return new BaseResponse<ClaimsIdentity>()
				{
					Data = result,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"[Login]: {ex.Message}");
				return new BaseResponse<ClaimsIdentity>()
				{
					Description = ex.Message,
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		private ClaimsIdentity Authenticate(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
			};
			return new ClaimsIdentity(claims, "ApplicationCookie",
				ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
		}

		public async Task<bool> ChangePassword(string newPassword, User user)
		{
			user.Password = HashPasswordHelper.HashPassword(newPassword);
			_context.User.Update(user);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
