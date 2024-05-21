namespace OnlineCourses.Domain.Entities
{
	public class TestResult : EntityBase
	{
		public Guid UserId { get; set; }
		public Guid TestId { get; set; }
		public DateTime DateComplete { get; set; } = DateTime.UtcNow;
		public byte Grade { get; set; }


		public User? User { get; set; }
		public Test? Test { get; set; }
	}
}
