using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Implementations.Interfaсes;
using OnlineCourses.Models.ViewModels.Create;

namespace OnlineCourses.Views.Shared.Components.Sidebar
{
	public class SidebarCreateViewComponent : ViewComponent
	{
		private readonly ICourseService _courseService;

		public SidebarCreateViewComponent(ICourseService courseService)
		{
			_courseService = courseService;
		}
		public IViewComponentResult Invoke(Guid id)
		{
			CreateCourseViewModel module = id == default ? new CreateCourseViewModel() : _courseService.GetStructureCreateCourseById(id);
			return View(module);
		}
	}
}
