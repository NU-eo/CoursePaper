using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Implementations.Interfaсes
{
	public interface IUserService
	{
		Task<User> GetUserById(Guid userId);
		User UserByLogin(string login);
		void UpdateAvatar(Guid userId, string avatar);
		User GetCurrentUser();
	}
}
