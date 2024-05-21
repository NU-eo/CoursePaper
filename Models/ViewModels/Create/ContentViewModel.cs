using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Models.ViewModels.Create
{
	public class ContentViewModel
	{
		public LessonContent lessonContent { get; set; }
		public Lesson? lesson { get; set; }
		public Guid courseId { get; set; }
	}
}
