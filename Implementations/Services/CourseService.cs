using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Domain.Enum;
using OnlineCourses.Implementations.Interfaсes;
using OnlineCourses.Models.ViewModels.Create;
using OnlineCourses.Models.ViewModels.Learning;

namespace OnlineCourses.Implementations.Services
{
	public class CourseService : ICourseService
	{
		private readonly AppDbContext _context;
		private readonly ILogger<AccountService> _logger;

		public CourseService(AppDbContext context, ILogger<AccountService> logger)
		{
			_context = context;
			_logger = logger;
		}
		public IEnumerable<Course> GetCourses()
		{
			return _context.Course.AsQueryable();
		}

		public IEnumerable<Course> GetPublishedCourse()
		{
			return _context.Course.Where(c => c.Status == StatusCourse.Published);
		}

		public async Task<Course> GetCourseByIdAsync(Guid id)
		{
			return await _context.Course.FirstOrDefaultAsync(c => c.Id == id);
		}

		public IEnumerable<Course> GetCourseByUserId(Guid userId)
		{
			return _context.Course.Where(c => c.UserId == userId);
		}

		public IEnumerable<Course> GetSubsCourseByUserId(Guid userId)
		{
			var courses = _context.Course.Where(c => c.Status == StatusCourse.Published);

			var subs = _context.Subscription.Where(b => b.UserId == userId && b.subStatus == SubStatus.Open);

			return courses.Join(subs,
					t => t.Id,
					b => b.CourseId,
					(t, b) => t)
				.ToList();
		}

		public int GetSubsCountByCourseId(Guid courseId)
		{
			return _context.Subscription.Where(s => s.CourseId == courseId).Count();
		}

		public async Task<List<UserAndSub>> GetUserAndSubListByCourseId(Guid courseId)
		{
			return await _context.Subscription.Include(s => s.User)
				.Where(s => s.CourseId == courseId)
				.Select(s => new UserAndSub
				{
					user = s.User,
					subscription = s
				}).ToListAsync();
		}

		public async Task<List<CompleteCourseList>> GetCompleteCourseList(Guid courseId, Guid userId)
		{

			var query = from l in _context.Lesson
						let cl = _context.CompletedLesson.FirstOrDefault(cl => cl.LessonId == l.Id && cl.UserId == userId)
						join m in _context.Module on l.ModuleId equals m.Id
						join c in _context.Course on m.CourseId equals c.Id
						join s in _context.Subscription.Where(s => s.UserId == userId && s.CourseId == courseId) on c.Id equals s.CourseId into subscription
						from s in subscription.DefaultIfEmpty()
						where s != null
						orderby m.OrderNum, l.OrderNum
						select new CompleteCourseList
						{
							ModuleId = m.Id,
							ModuleTitle = m.Title,
							LessonId = l.Id,
							LessonTitle = l.Title,
							LessonStatus = cl == null ? "Не пройден" : "Пройден"
						};
			return await query.ToListAsync();
		}

		public CreateCourseViewModel GetStructureCreateCourseById(Guid id)
		{
			var course = _context.Course
				.Include(c => c.Modules)
				.ThenInclude(m => m.Lessons)
				.FirstOrDefault(c => c.Id == id);

			if (course == null)
			{
				return null;
			}

			var CreateCourseViewModel = new CreateCourseViewModel
			{
				course = course,
				structure = course.Modules
					.OrderBy(m => m.OrderNum)
					.Select(m => new CreateModuleViewModel
					{
						module = m,
						lessons = m.Lessons
							.OrderBy(l => l.OrderNum)
							.AsQueryable()
					})
					.AsQueryable()
			};

			return CreateCourseViewModel;
		}

		public async Task<CourseViewModel> GetStructureCourseByCourseAndUserId(Guid courseId, Guid userId)
		{
			var course = await _context.Course
				.Include(c => c.Modules)
				.ThenInclude(m => m.Lessons)
				.FirstOrDefaultAsync(c => c.Id == courseId);

			var structure = course.Modules.OrderBy(m => m.OrderNum).Select(m => new ModuleViewModel
			{
				module = m,
				lessons = m.Lessons.OrderBy(m => m.OrderNum).Select(l => new LessonViewModel
				{
					lesson = l,
					isComplete = _context.CompletedLesson.Any(cl => cl.LessonId == l.Id && cl.UserId == userId)
				})
			});

			var courseViewModel = new CourseViewModel
			{
				course = course,
				structure = structure
			};

			return courseViewModel;
		}
	}

}
