using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Domain.Enum;
using OnlineCourses.Domain.Response;
using OnlineCourses.Implementations.Interfaсes;

namespace OnlineCourses.Implementations.Services
{
	public class CreateService : ICreateService
	{
		private readonly AppDbContext _context;
		private readonly ILogger<AccountService> _logger;

		public CreateService(AppDbContext context, ILogger<AccountService> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<StatusCourse> SwapStatusCourse(Course course)
		{
			if (course.Status != StatusCourse.Blocked)
			{
				if (course.Status == StatusCourse.Hidden)
				{
					course.Status = StatusCourse.Published;
				}
				else if (course.Status == StatusCourse.Published)
				{
					course.Status = StatusCourse.Hidden;
				}
				await _context.SaveChangesAsync();
			}
			return course.Status;
		}

		public async Task<SubStatus> SwapStatusSubscription(Subscription subscription)
		{
			if (subscription.subStatus == SubStatus.Open)
			{
				subscription.subStatus = SubStatus.Blocked;
			}
			else
			{
				subscription.subStatus = SubStatus.Open;
			}
			await _context.SaveChangesAsync();
			return subscription.subStatus;
		}

		public async Task<BaseResponse<Course>> AddCourseAsync(Course course)
		{
			try
			{
				if (course.Id == default)
					_context.Entry(course).State = EntityState.Added;
				else
					_context.Entry(course).State = EntityState.Modified;

				await _context.SaveChangesAsync();

				return new BaseResponse<Course>()
				{
					Data = course,
					Description = "Объект добавился",
					StatusCode = StatusCode.OK
				};

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"[Course]: {ex.Message}");
				return new BaseResponse<Course>()
				{
					Description = ex.Message,
					StatusCode = StatusCode.InternalServerError
				};
			}
		}
		public async Task<bool> DeleteCourseAsync(Course course)
		{
			_context.Course.Remove(course);
			await _context.SaveChangesAsync();
			return true;
		}


		public async Task<bool> AddModuleAsync(Module module)
		{
			_context.Module.Add(module);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> UpdateModuleAsync(Module module)
		{
			_context.Module.Update(module);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> DeleteModuleAsync(Module module)
		{
			_context.Module.Remove(module);
			await _context.SaveChangesAsync();
			return true;
		}


		public async Task<bool> AddLessonAsync(Lesson lesson)
		{
			_context.Lesson.Add(lesson);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> UpdateLessonAsync(Lesson lesson)
		{
			_context.Lesson.Update(lesson);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> DeleteLessonAsync(Lesson lesson)
		{
			_context.Lesson.Remove(lesson);
			await _context.SaveChangesAsync();
			return true;
		}


		public async Task<bool> AddContentAsync(LessonContent lessonContent)
		{
			try
			{
				if (lessonContent.Id == default)
					_context.Entry(lessonContent).State = EntityState.Added;
				else
					_context.Entry(lessonContent).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				// Логирование ошибки
				_logger.LogError(ex, "Ошибка при добавлении LessonContent в базу данных");
				return false;
			}
		}
		public async Task<bool> UpdateContentAsync(LessonContent lessonContent)
		{
			_context.LessonContent.Update(lessonContent);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> DeleteContentAsync(LessonContent lessonContent)
		{
			_context.LessonContent.Remove(lessonContent);
			await _context.SaveChangesAsync();
			return true;
		}


		public async Task<bool> AddLessonAndContentAsync(Lesson lesson, LessonContent lessonContent)
		{
			_context.Lesson.Add(lesson);
			_context.LessonContent.Add(lessonContent);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> DeleteLessonAndContentAsync(Lesson lesson, LessonContent lessonContent)
		{
			_context.LessonContent.Remove(lessonContent);
			_context.Lesson.Remove(lesson);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
