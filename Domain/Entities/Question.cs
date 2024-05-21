namespace OnlineCourses.Domain.Entities
{
	public class Question : EntityBase
	{
		public Guid TestId { get; set; }
		public string QuestionText { get; set; }


		public Test? Test { get; set; }
		public ICollection<Answer>? Answers { get; set; }
	}
}
