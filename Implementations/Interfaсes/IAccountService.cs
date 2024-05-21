using OnlineCourses.Domain.Entities;
using OnlineCourses.Domain.Response;
using OnlineCourses.Models.ViewModels.Account;
using System.Security.Claims;

namespace OnlineCourses.Implementations.Interfaсes
{
	public interface IAccountService
	{
		Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
		Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
		Task<bool> ChangePassword(string newPassword, User user);
	}
}
