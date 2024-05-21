using OnlineCourses.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Models.ViewModels.Create
{
	public class CourseTitleCreateViewModel
	{
		[Required]
		public Course course { get; set; }

		public IFormFile? CourseLogo { get; set; }

		public IFormFile? CourseTitleImg { get; set; }
	}
}
