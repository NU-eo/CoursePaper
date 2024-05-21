using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Enum
{
	public enum Role
	{
		[Display(Name = "Пользователь")]
		User = 0,

		[Display(Name = "Админ")]
		Admin = 1,
	}
}
