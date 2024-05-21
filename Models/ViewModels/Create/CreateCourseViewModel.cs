using OnlineCourses.Domain.Entities;
using OnlineCourses.Domain.Enum;
using System.ComponentModel.DataAnnotations;


namespace OnlineCourses.Models.ViewModels.Create
{
	public class CreateCourseViewModel
	{
		public Course course { get; set; }
		public Module? module { get; set; }
		public IQueryable<CreateModuleViewModel>? structure { get; set; }
	}

	public class CreateModuleViewModel
	{
		public Module? module { get; set; }
		public CreateLessonViewModel? lesson { get; set; }
		public IQueryable<Lesson>? lessons { get; set; }
	}

	public class CreateLessonViewModel
	{
		[Required]
		public Guid ModuleId { get; set; }

		[Display(Name = "Название урока")]
		[Required(ErrorMessage = "Укажите название урока")]
		[MaxLength(100, ErrorMessage = "Слишком длинно")]
		public string? Title { get; set; }
		[Required]
		[Display(Name = "Номер")]
		public int? OrderNum { get; set; }

		[Required]
		[Display(Name = "Тип урока")]
		public ContentType ContentType { get; set; }
	}
}
