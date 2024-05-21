using OnlineCourses.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Domain.Entities
{
	public class Course : EntityBase
	{
		[Display(Name = "���������")]
		[Required(ErrorMessage = "������� �������� �����")]
		[MaxLength(50, ErrorMessage = "�������� 50")]
		public string Title { get; set; }

		[Display(Name = "������� ��������")]
		[Required(ErrorMessage = "�������� ������")]
		[MaxLength(200, ErrorMessage = "�������� 100")]
		public string ShortDescription { get; set; }

		[Display(Name = "� �����")]
		[Required(ErrorMessage = "�������� ������")]
		[MaxLength(1200, ErrorMessage = "�������� 1500")]
		public string AboutCourse { get; set; }

		[Display(Name = "��� ���� ���� ����")]
		[Required(ErrorMessage = "���� �����")]
		[MaxLength(100, ErrorMessage = "�������� 50")]
		public string ForWhom { get; set; }

		[Display(Name = "������� �����")]
		[Required(ErrorMessage = "�������� ��������")]
		public string ImgLogoPath { get; set; } = "default_curse_logo.png";

		[Display(Name = "�������� �����")]
		[Required(ErrorMessage = "�������� ��������")]
		public string ImgTitlePath { get; set; } = "default_curse_title.png";
		public Guid UserId { get; set; }
		public DateTime DateCreate { get; set; } = DateTime.UtcNow;
		public StatusCourse Status { get; set; } = StatusCourse.Hidden;


		public User? User { get; set; }
		public ICollection<Module>? Modules { get; set; }
		public ICollection<Subscription>? Subscriptions { get; set; }

	}
}
