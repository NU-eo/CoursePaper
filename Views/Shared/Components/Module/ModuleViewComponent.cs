using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Models.ViewModels.Create;

namespace OnlineCourses.Views.Shared.Components.Module
{
	public class ModuleViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(CreateModuleViewModel module, int index)
		{
			// Сохраняем индекс в ViewData
			ViewData["Index"] = index;
			return View(module);
		}
	}
}
