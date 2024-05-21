using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Implementations.Interfaсes;
using Microsoft.AspNetCore.Authorization;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Models.ViewModels.Create;
using OnlineCourses.Implementations.Services;
using OnlineCourses.Domain.Enum;

namespace OnlineCourses.Controllers
{
	[Authorize]
	public class CreateController : Controller
	{
		private readonly IUserService _userService;
		private readonly ICourseService _courseService;
		private readonly ILearningService _learningService;
		private readonly IModuleService _moduleService;
		private readonly ILessonService _lessonService;
		private readonly ICreateService _createService;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly ILogger<HomeController> _logger;

		public CreateController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnviroment, IUserService userService, ICourseService courseService, IModuleService moduleService, ILessonService lessonService, ICreateService createService, ILearningService learningService)
		{
			_hostingEnvironment = hostingEnviroment;
			_learningService = learningService;
			_courseService = courseService;
			_moduleService = moduleService;
			_lessonService = lessonService;
			_createService = createService;
			_userService = userService;
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View(_courseService.GetCourseByUserId(_userService.GetCurrentUser().Id));
		}

		[HttpGet]
		public async Task<IActionResult> TitleCreate(Guid id)
		{
			var entity = id == default ? new Course() : await _courseService.GetCourseByIdAsync(id);
			return View(new CourseTitleCreateViewModel() { course = entity });
		}

		[HttpPost]
		public async Task<IActionResult> TitleCreate(CourseTitleCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var logoName = "default_curse_logo.png";
				var titleName = "default_curse_title.png";
				if (model.CourseLogo != null && model.CourseLogo.Length > 0)
				{
					// Генерируем уникальное имя для файла
					logoName = Guid.NewGuid().ToString() + Path.GetExtension(model.CourseLogo.FileName);

					// Путь для сохранения аватарки
					var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", "curselogo", logoName);

					// Сохраняем аватарку на сервере
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await model.CourseLogo.CopyToAsync(fileStream);
					}
				}
				if (model.CourseTitleImg != null && model.CourseTitleImg.Length > 0)
				{
					// Генерируем уникальное имя для файла
					titleName = Guid.NewGuid().ToString() + Path.GetExtension(model.CourseTitleImg.FileName);

					// Путь для сохранения аватарки
					var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", "courseTitleImg", titleName);

					// Сохраняем аватарку на сервере
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await model.CourseTitleImg.CopyToAsync(fileStream);
					}
				}

				model.course.UserId = _userService.GetCurrentUser().Id;
				model.course.ImgLogoPath = logoName;
				model.course.ImgTitlePath = titleName;
				var response = await _createService.AddCourseAsync(model.course);
				if (response.StatusCode == Domain.Enum.StatusCode.OK)
				{
					var mo = new TitleCreateCourseViewModel()
					{
						course = response.Data,
						isSubscribted = false,
						SubCount = _courseService.GetSubsCountByCourseId(response.Data.Id)
					};

					return View("TitleView", mo);
				}
			}

			return View(model);
		}

		public async Task<IActionResult> TitleView(Guid id)
		{
			var course = await _courseService.GetCourseByIdAsync(id);

			var model = new TitleCreateCourseViewModel()
			{
				course = course,
				isSubscribted = false,
				SubCount = _courseService.GetSubsCountByCourseId(id)
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> CourseDelete(Guid id)
		{
			if (_moduleService.GetModulesByCourseId(id).Count() == 0)
			{
				var course = await _courseService.GetCourseByIdAsync(id);
				await _createService.DeleteCourseAsync(course);
			}
			else
			{
				ViewBag.Message = "Удалите все модули перед удалением курса";
			}

			return View("Index", _courseService.GetCourseByUserId(_userService.GetCurrentUser().Id));
		}

		public IActionResult StructureCreate(Guid id)
		{
			var model = _courseService.GetStructureCreateCourseById(id);
			return View(model);
		}


		[HttpPost]
		public async Task<IActionResult> CreateModule(CreateCourseViewModel model)
		{
			var module = model.module;
			module.OrderNum = await _moduleService.GetMaxOrderNumByCourseIdAsync(module.CourseId) + 1;
			await _createService.AddModuleAsync(module);
			return RedirectToAction("StructureCreate", "Create", new { id = module.CourseId });
		}

		[HttpPost]
		public async Task<IActionResult> UpdateModule(CreateModuleViewModel model)
		{
			var module = await _moduleService.GetModuleByIdAsync(model.module.Id);
			if (module != null)
			{
				if (module.Title != model.module.Title)
				{
					module.Title = model.module.Title;
				}
				await _createService.UpdateModuleAsync(module);
			}

			return RedirectToAction("StructureCreate", "Create", new { id = module.CourseId });
		}

		[HttpPost]
		public async Task<IActionResult> DeleteModule(CreateModuleViewModel model)
		{
			var module = await _moduleService.GetModuleByIdAsync(model.module.Id);

			if (module.Id == default)
			{
				return RedirectToAction("Index");
			}

			if ((await _lessonService.GetLessonByModuleId(module.Id)) == null)
			{
				await _createService.DeleteModuleAsync(module);
			}


			return RedirectToAction("StructureCreate", "Create", new { id = module.CourseId });
		}


		[HttpPost]
		public async Task<IActionResult> CreateLesson(CreateModuleViewModel model)
		{
			if (model.lesson.Title != null)
			{
				var lesson = new Lesson
				{
					Id = Guid.NewGuid(),
					Title = model.lesson.Title,
					ModuleId = model.lesson.ModuleId,
					OrderNum = await _lessonService.GetMaxOrderNumByModuleIdAsync(model.lesson.ModuleId) + 1
				};

				var lessonContent = new LessonContent
				{
					LessonId = lesson.Id,
					ContentType = model.lesson.ContentType
				};

				bool les = await _createService.AddLessonAndContentAsync(lesson, lessonContent);

				if (les)
				{
					return RedirectToAction("StructureCreate", "Create", new { id = model.module.CourseId });
				}
			}
			return RedirectToAction("Error");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateLessonName(Lesson model)
		{
			var lesson = await _lessonService.GetLessonByIdAsync(model.Id);
			if (lesson == null)
			{
				return RedirectToAction("Index");
			}

			if (lesson.Title != model.Title)
			{
				lesson.Title = model.Title;
			}

			var module = await _moduleService.GetModuleByIdAsync(lesson.ModuleId);

			if (module == null)
			{
				return RedirectToAction("Index");
			}

			await _createService.UpdateLessonAsync(lesson);

			return RedirectToAction("StructureCreate", "Create", new { id = module.CourseId });
		}

		[HttpPost]
		public async Task<IActionResult> DeleteLesson(Lesson model)
		{
			if (model.Id == default)
			{
				return RedirectToAction("Index");
			}

			var content = await _lessonService.GetContentByLessonIdAsync(model.Id);
			var lesson = await _lessonService.GetLessonByIdAsync(model.Id);
			var module = await _moduleService.GetModuleByIdAsync(lesson.ModuleId);

			if (content != null && lesson != null)
			{
				await _createService.DeleteLessonAndContentAsync(lesson, content);
			}

			return RedirectToAction("StructureCreate", "Create", new { id = module.CourseId });
		}


		[HttpGet]
		public async Task<IActionResult> ContentCreate(Guid id)
		{
			var lesson = await _lessonService.GetLessonByIdAsync(id);
			var module = await _moduleService.GetModuleByIdAsync(lesson.ModuleId);
			var content = await _lessonService.GetContentByLessonIdAsync(id);

			var contentModel = new ContentViewModel()
			{
				lessonContent = content,
				lesson = lesson,
				courseId = module.CourseId
			};
			return View(contentModel);
		}

		[HttpPost]
		public async Task<IActionResult> ContentCreate(ContentViewModel model)
		{
			if (ModelState.IsValid)
			{
				await _createService.UpdateContentAsync(model.lessonContent);
				return RedirectToAction("StructureCreate", "Create", new { id = model.courseId });
			}
			return View(model);
		}


		public async Task<IActionResult> UserListCourse(Guid courseId)
		{
			var usersAndSubs = await _courseService.GetUserAndSubListByCourseId(courseId);

			var model = new UserListViewModel
			{
				courseId = courseId,
				userAndSubs = usersAndSubs.AsQueryable()
			};

			return View(model);
		}

		public async Task<IActionResult> CompleteCourseList(Guid courseId, Guid userId)
		{
			var completeCourseLists = await _courseService.GetCompleteCourseList(courseId, userId);

			var model = new CompleteCourseListViewModel
			{
				courseId = courseId,
				user = await _userService.GetUserById(userId),
				completeCourseLists = completeCourseLists.AsQueryable()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> UserSubStatusSwap(Guid userId)
		{
			var sub = await _learningService.GetSubById(userId);
			var newSubStatus = await _createService.SwapStatusSubscription(sub);
			return Json(new { newSubStatus = newSubStatus.GetDisplayName() });
		}

		[HttpPost]
		public async Task<IActionResult> CourseStatusSwap(Guid courseId)
		{
			var course = await _courseService.GetCourseByIdAsync(courseId);
			var status = await _createService.SwapStatusCourse(course);

			return Json(new { newStatus = status.GetDisplayName() });
		}

		//// Проверка
		//public async Task<bool> IsCurrentUsersCourse(Guid courseId, Guid? moduleId)
		//{
		//	var identity = User.Identity as ClaimsIdentity;
		//	if (identity != null)
		//	{
		//		var role = identity.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
		//		if (role == Role.Admin.ToString())
		//		{
		//			Console.WriteLine($"Роль пользователя: {role} - разрешаю");
		//			return true;
		//		}
		//		else
		//		{
		//			var user = _userService.GetCurrentUser();

		//			Console.WriteLine($"Идентификатор пользователя: {user.Id}");

		//			if (courseId != default)
		//			{
		//				var course = await _courseService.GetCourseByIdAsync(courseId);

		//				Console.WriteLine($"Идентификатор пользователя в курсе: {course.UserId}");

		//				var Bool = course.UserId == user.Id ? true : false;
		//				Console.WriteLine($"Результат разрешения по id курса: {Bool}");
		//				return course.UserId == user.Id ? true : false;
		//			}
		//			else if (moduleId != default)
		//			{
		//				var module = await _moduleService.GetModuleByIdAsync(moduleId);
		//				var course = await _courseService.GetCourseByIdAsync(module.CourseId);

		//				Console.WriteLine($"Идентификатор пользователя в курсе: {course.UserId}");

		//				var Bool = course.UserId == user.Id ? true : false;
		//				Console.WriteLine($"Результат разрешения по id модуля: {Bool}");
		//				return course.UserId == user.Id ? true : false;
		//			}
		//		}
		//	}

		//	Console.WriteLine($"Ничего хорошего");
		//	return false;

		//}
		//

	}
}
