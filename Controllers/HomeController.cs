using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Implementations.Interfañes;
using OnlineCourses.Models;
using System.Diagnostics;
using X.PagedList;

namespace OnlineCourses.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUserService _userService;
		private readonly ICourseService _courseService;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IUserService userService, ICourseService courseService)
		{
			_logger = logger;
			_userService = userService;
			_courseService = courseService;
		}

		public IActionResult Index(string searchString, int? page)
		{
			int pageNumber = page ?? 1;
			int pageSize = 10;
			ViewBag.SearchString = searchString;

			var courses = _courseService.GetPublishedCourse();

			if (!String.IsNullOrEmpty(searchString))
			{
				courses = courses.Where(s =>
					$"{s.Title} {s.ShortDescription} {s.ForWhom}".ToLower().Contains(searchString.ToLower()));
			}

			return View(courses.ToPagedList(pageNumber, pageSize));
		}

		[Authorize]
		public IActionResult Profile()
		{
			var user = _userService.GetCurrentUser();

			if (user != null)
			{
				return View(user);
			}
			return View("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


	}
}
