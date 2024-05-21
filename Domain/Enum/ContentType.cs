using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Enum
{
	public enum ContentType
	{
		[Display(Name = "Лекция")]
		Lecture = 0,

		[Display(Name = "Видео")]
		Video = 1,

		//Пока слишком сложно
		//[Display(Name = "Тест")]
		//Test = 2,
	}
}
