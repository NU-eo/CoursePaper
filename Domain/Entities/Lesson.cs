using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Entities
{
	public class Lesson : EntityBase
	{
		public string? Title { get; set; }
		public Guid? ModuleId { get; set; }
		public int? OrderNum {get; set;}
		public DateTime? DateCreate { get; set; } = DateTime.UtcNow;


		public Module? Module { get; set; }
		public ICollection<LessonContent>? LessonContent { get; set; }
		public ICollection<CompletedLesson>? CompletedLessons { get; set; } 
	}
}
