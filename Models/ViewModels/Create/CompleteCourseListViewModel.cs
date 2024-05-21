using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Models.ViewModels.Create
{
	public class CompleteCourseListViewModel
	{
		public Guid courseId { get; set; }
		public User user { get; set; }
		public IQueryable<CompleteCourseList> completeCourseLists { get; set; }
	}
	public class CompleteCourseList
	{
		public Guid ModuleId { get; set; }
		public string ModuleTitle { get; set; }
		public Guid LessonId { get; set; }
		public string LessonTitle { get; set; }
		public string LessonStatus { get; set; }
	}

}
