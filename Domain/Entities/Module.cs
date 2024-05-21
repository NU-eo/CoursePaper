using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Entities
{
	public class Module : EntityBase
	{
		[Display(Name = "�������� ������")]
		[Required(ErrorMessage = "������� �������� ������")]
		[MaxLength(150, ErrorMessage = "������� ������")]
		public string Title { get; set; } = "��������� ��� ������ ������";
		public Guid CourseId { get; set; }

		[Required]
		[Display(Name = "�����")]
		public int? OrderNum {get; set;} = 0;
		public DateTime DateCreate { get; set; } = DateTime.UtcNow;


		public Course? Course { get; set; }
		public ICollection<Lesson>? Lessons { get; set; }
	}
}
