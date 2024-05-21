using OnlineCourses.Domain.Enum;

namespace OnlineCourses.Domain.Entities
{
	public class User : EntityBase
	{
		public string UserName { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string AvatarPath { get; set; } = "default.png";
		public Role Role { get; set; } = 0;
		public SubStatus Status { get; set; } = 0;
		public DateTime DateCreate { get; set; } = DateTime.UtcNow;


		public ICollection<Course>? Courses { get; set; }
		public ICollection<Subscription>? Subscriptions { get; set; }
		public ICollection<CompletedLesson>? CompletedLessons { get; set; }
		public ICollection<TestResult>? TestResults { get; set; }
	}
}
