using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Models.ViewModels.Learning
{
	public class CourseViewModel
	{
		public Course course { get; set; }
		public IEnumerable<ModuleViewModel>? structure { get; set; }
	}

	public class ModuleViewModel
	{
		public Module? module { get; set; }
		public IEnumerable<LessonViewModel>? lessons { get; set; }
	}

	public class LessonViewModel
	{
		public Lesson lesson { get; set; }
		public bool isComplete { get; set; }
	}
}
