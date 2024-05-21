namespace OnlineCourses.Domain.Entities
{
	public class Test : EntityBase
	{
		public string Title { get; set; }
		public DateTime DateCreate { get; set; } = DateTime.UtcNow;


		public LessonContent? LessonContent { get; set; }
		public ICollection<Question>? Questions { get; set; }
		public ICollection<TestResult>? TestResults { get; set; }
	}
}
