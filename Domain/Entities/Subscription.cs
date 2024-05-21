using OnlineCourses.Domain.Enum;

namespace OnlineCourses.Domain.Entities
{
	public class Subscription : EntityBase
	{
		public Guid UserId { get; set; }
		public Guid CourseId { get; set; }
		public bool isComplete { get; set; } = false;
		public SubStatus subStatus { get; set; } = SubStatus.Open;
		public DateTime DateCreate { get; set; } = DateTime.UtcNow;


		public User? User { get; set; }
		public Course? Course { get; set; }
	}
}
