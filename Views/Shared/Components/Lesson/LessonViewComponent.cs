using Microsoft.AspNetCore.Mvc;

namespace OnlineCourses.Views.Shared.Components.Lesson
{
	public class LessonViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(Domain.Entities.Lesson lesson, int MIndex, int LIndex)
		{
			// Сохраняем индекс в ViewData
			ViewData["LIndex"] = LIndex;
			ViewData["MIndex"] = MIndex;
			return View(lesson);
		}
	}
}
