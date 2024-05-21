using OnlineCourses.Domain.Enum;

namespace OnlineCourses.Domain.Entities
{
	public class LessonContent : EntityBase
	{
		public ContentType ContentType { get; set; }
		public Guid? LessonId { get; set; }
		public Guid? TestId { get; set; }
		public string? Content {get; set;} = "Тут пустовато :(";
		public string? VideoUrl { get; set; } = "https://www.youtube.com/embed/MWsfHQaUzI0?si=49Ii-I6e0RGnwhAW";


		public Lesson? Lesson { get; set; }
		public Test? Test { get; set; }
	}
}
