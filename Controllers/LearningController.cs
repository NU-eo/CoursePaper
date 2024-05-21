using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Implementations.Interfaсes;
using OnlineCourses.Models.ViewModels.Create;

namespace OnlineCourses.Controllers
{
	[Authorize]
	public class LearningController : Controller
	{
		private readonly IUserService _userService;
		private readonly ICourseService _courseService;
		private readonly IModuleService _moduleService;
		private readonly ILearningService _learningService;
		private readonly ILessonService _lessonService;
		private readonly ILogger<HomeController> _logger;

		public LearningController(ILogger<HomeController> logger, IUserService userService, ICourseService courseService, ILearningService learningService, ILessonService lessonService, IModuleService moduleService)
		{
			_logger = logger;
			_userService = userService;
			_courseService = courseService;
			_learningService = learningService;
			_lessonService = lessonService;
			_moduleService = moduleService;
		}
		public IActionResult Index()
		{
			var courses = _courseService.GetSubsCourseByUserId(_userService.GetCurrentUser().Id);
			return View(courses);
		}

		public async Task<IActionResult> TitleCourse(Guid id)
		{
			var model = new TitleCreateCourseViewModel()
			{
				course = await _courseService.GetCourseByIdAsync(id),
				isSubscribted = (await _learningService.GetSubscription(_userService.GetCurrentUser().Id, id)) != null,
				SubCount = _courseService.GetSubsCountByCourseId(id)
			};
			return View(model);
		}

		public async Task<IActionResult> Course(Guid id)
		{
			Module? module = await _moduleService.GetFirstModuleByCourseIdAsync(id);

			Lesson lesson = new Lesson();
			LessonContent content = new LessonContent()
			{
				Content = "Курс не заполнен"
			};

			if (module != null)
			{
				lesson = await _lessonService.GetFirstLessonByModuleIdAsync(module.Id);
				if (lesson != null)
				{
					content = await _lessonService.GetContentByLessonIdAsync(lesson.Id);
					await _learningService.CompleteLesson(lesson.Id, _userService.GetCurrentUser().Id);
				}
			}

			var contentModel = new ContentViewModel()
			{
				lessonContent = content,
				lesson = lesson,
				courseId = id
			};


			return View("Lesson", contentModel);
		}

		public async Task<IActionResult> Lesson(Guid lessonId)
		{
			var lesson = await _lessonService.GetLessonByIdAsync(lessonId);
			var module = await _moduleService.GetModuleByIdAsync(lesson.ModuleId);
			var content = await _lessonService.GetContentByLessonIdAsync(lessonId);

			var contentModel = new ContentViewModel()
			{
				lessonContent = content,
				lesson = lesson,
				courseId = module.CourseId
			};

			await _learningService.CompleteLesson(lesson.Id, _userService.GetCurrentUser().Id);

			return View(contentModel);
		}

		[HttpPost]
		public async Task<IActionResult> Subscribe(Guid id)
		{
			Guid userId = _userService.GetCurrentUser().Id;

			if ((await _learningService.GetSubscription(userId, id)) == null)
			{
				var sub = new Subscription()
				{
					UserId = userId,
					CourseId = id
				};

				await _learningService.AddSubscribeAsync(sub);
			}

			return RedirectToAction("Index", "Learning");
		}

	}
}
