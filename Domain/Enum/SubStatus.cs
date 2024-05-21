using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Enum
{
	public enum SubStatus
	{
		[Display(Name = "Открыт")]
		Open = 0,

		[Display(Name = "Заблокирован")]
		Blocked = 1,
	}
}
