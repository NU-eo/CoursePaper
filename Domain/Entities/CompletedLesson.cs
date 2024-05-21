namespace OnlineCourses.Domain.Entities
{
	public class CompletedLesson : EntityBase
	{
		public Guid UserId { get; set; }
		public Guid LessonId { get; set; }
		public DateTime DateSubscribed{ get; set; } = DateTime.UtcNow;


		public User? User { get; set; }
		public Lesson? Lesson { get; set; }

	}
}
