using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Implementations.Interfaсes;
using OnlineCourses.Models.ViewModels.Learning;

namespace OnlineCourses.Views.Shared.Components.Sidebar
{
	public class SidebarViewComponent : ViewComponent
	{
		private readonly ICourseService _courseService;
		private readonly IUserService _userService;

		public SidebarViewComponent(ICourseService courseService, IUserService userService)
		{
			_courseService = courseService;
			_userService = userService;
		}
		public async Task<IViewComponentResult> InvokeAsync(Guid id)
		{
			CourseViewModel module = id == default ? new CourseViewModel() : await _courseService.GetStructureCourseByCourseAndUserId(id, _userService.GetCurrentUser().Id);
			return View(module);
		}
	}
}
