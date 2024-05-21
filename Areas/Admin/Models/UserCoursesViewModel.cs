using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Areas.Admin.Models
{
	public class UserCoursesViewModel
	{
		public User user { get; set; }
		public IQueryable<Course> courses { get; set; }
	}
}
