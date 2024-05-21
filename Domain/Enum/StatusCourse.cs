using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Enum
{
	public enum StatusCourse
	{
		[Display(Name = "Опубликован")]
		Published = 0,

		[Display(Name = "Скрыт")]
		Hidden = 1,

		[Display(Name = "Заблокирован")]
		Blocked = 2,
	}
}
