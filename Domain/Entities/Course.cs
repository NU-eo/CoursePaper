using OnlineCourses.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Entities
{
	public class Course : EntityBase
	{
		[Display(Name = "Заголовок")]
		[Required(ErrorMessage = "Укажите название курса")]
		[MaxLength(50, ErrorMessage = "Максимум 50")]
		public string Title { get; set; }

		[Display(Name = "Краткое Описание")]
		[Required(ErrorMessage = "Описание пустое")]
		[MaxLength(200, ErrorMessage = "Максимум 100")]
		public string ShortDescription { get; set; }

		[Display(Name = "О курсе")]
		[Required(ErrorMessage = "Описание пустое")]
		[MaxLength(1200, ErrorMessage = "Максимум 1500")]
		public string AboutCourse { get; set; }

		[Display(Name = "Для кого этот курс")]
		[Required(ErrorMessage = "Поле пусто")]
		[MaxLength(100, ErrorMessage = "Максимум 50")]
		public string ForWhom { get; set; }

		[Display(Name = "Логотип курса")]
		[Required(ErrorMessage = "Выберете картинку")]
		public string ImgLogoPath { get; set; } = "default_curse_logo.png";

		[Display(Name = "Картинка курса")]
		[Required(ErrorMessage = "Выберете картинку")]
		public string ImgTitlePath { get; set; } = "default_curse_title.png";
		public Guid UserId { get; set; }
		public DateTime DateCreate { get; set; } = DateTime.UtcNow;
		public StatusCourse Status { get; set; } = StatusCourse.Hidden;


		public User? User { get; set; }
		public ICollection<Module>? Modules { get; set; }
		public ICollection<Subscription>? Subscriptions { get; set; }

	}
}
