using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Entities
{
	public class Module : EntityBase
	{
		[Display(Name = "Название модуля")]
		[Required(ErrorMessage = "Укажите название модуля")]
		[MaxLength(150, ErrorMessage = "Слишком длинно")]
		public string Title { get; set; } = "Заголовок для ващего модуля";
		public Guid CourseId { get; set; }

		[Required]
		[Display(Name = "Номер")]
		public int? OrderNum {get; set;} = 0;
		public DateTime DateCreate { get; set; } = DateTime.UtcNow;


		public Course? Course { get; set; }
		public ICollection<Lesson>? Lessons { get; set; }
	}
}
