using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Models.ViewModels.Create
{
	public class TitleCreateCourseViewModel
	{
		public Course course { get; set; }
		public bool isSubscribted { get; set; }
		public int SubCount { get; set; }
	}
}
