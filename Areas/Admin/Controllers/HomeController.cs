using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Areas.Admin.Models;
using OnlineCourses.Domain;
using OnlineCourses.Domain.Enum;

namespace OnlineCourses.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		private readonly AppDbContext _context;
		public HomeController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var users = _context.User.ToList();

			return View(users);
		}

		public IActionResult UserCoursesList(Guid userId)
		{
			var course = _context.Course.Where(c => c.UserId == userId);
			var user = _context.User.FirstOrDefault(u => u.Id == userId);

			if (user != null)
			{
				var model = new UserCoursesViewModel
				{
					user = user,
					courses = course.AsQueryable()
				};

				return View(model);
			}
			return RedirectToAction("Error", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> SwapStatusCourse(Guid courseId)
		{
			var course = await _context.Course.FindAsync(courseId);
			string status = "Ошибка";

			if (course != null)
			{
				if (course.Status == StatusCourse.Published || course.Status == StatusCourse.Hidden)
				{
					course.Status = StatusCourse.Blocked;
				}
				else
				{
					course.Status = StatusCourse.Hidden;
				}
				await _context.SaveChangesAsync();
				status = course.Status.GetDisplayName();
			}

			return Json(new { newStatus = status });
		}

		[HttpPost]
		public async Task<IActionResult> SwapStatusUser(Guid userId)
		{
			var user = await _context.User.FindAsync(userId);
			string status = "Ошибка";


			if (user != null)
			{
				if (user.Status == SubStatus.Open)
				{
					user.Status = SubStatus.Blocked;
				}
				else
				{
					user.Status = SubStatus.Open;
				}
				await _context.SaveChangesAsync();
				status = user.Status.GetDisplayName();
			}

			return Json(new { newStatus = status });
		}
	}
}
