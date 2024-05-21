namespace OnlineCourses.Domain.Entities
{
	public class Answer : EntityBase
	{
		public Guid QuestionId { get; set; }
		public string? AnswerText { get; set; }
		public bool IsCorrect { get; set; }


		public Question? Question { get; set; }
	}
}
