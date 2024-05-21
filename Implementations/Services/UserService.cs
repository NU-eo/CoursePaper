using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Implementations.Interfaсes;
using System.Security.Claims;

namespace OnlineCourses.Implementations.Services
{
	public class UserService : IUserService
	{
		private readonly AppDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<User> GetUserById(Guid userId)
		{
			return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
		}

		public User UserByLogin(string login)
		{
			return _context.User.FirstOrDefault(x => x.Login == login);
		}

		public void UpdateAvatar(Guid userId, string avatar)
		{
			// Получаем существующую запись по одному полю
			var existingRecord = _context.User.FirstOrDefault(r => r.Id == userId);

			if (existingRecord != null)
			{
				// Вносим изменения в запись
				existingRecord.AvatarPath = avatar;

				// Сохраняем изменения в базе данных
				_context.SaveChanges();
			}
		}

		public User GetCurrentUser()
		{
			return UserByLogin(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType));
		}
	}
}
