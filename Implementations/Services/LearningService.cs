using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Implementations.Interfaсes;

namespace OnlineCourses.Implementations.Services
{
	public class LearningService : ILearningService
	{
		private readonly AppDbContext _context;

		public LearningService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Subscription> GetSubById(Guid subId)
		{
			return await _context.Subscription.FirstOrDefaultAsync(s => s.Id == subId);
		}

		public async Task<CompletedLesson> GetCompleteByLessonAndUserId(Guid lessonId, Guid userId)
		{
			return await _context.CompletedLesson.FirstOrDefaultAsync(s => s.LessonId == lessonId && s.UserId == userId);
		}

		public async Task<int> GetCompletedLessonCount(Guid userId, Guid courseId)
		{
			return await _context.CompletedLesson
				.Where(cl => cl.UserId == userId && cl.Lesson.Module.CourseId == courseId)
				.CountAsync();
		}

		public async Task<Subscription> GetSubscription(Guid userId, Guid courseId)
		{
			return await _context.Subscription.FirstOrDefaultAsync(s => s.CourseId == courseId && s.UserId == userId);
		}

		public async Task<bool> AddSubscribeAsync(Subscription sub)
		{
			_context.Subscription.Add(sub);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> CompleteLesson(Guid lessonId, Guid userId)
		{
            if (await GetCompleteByLessonAndUserId(lessonId, userId) == null)
			{
				var complete = new CompletedLesson()
				{
					LessonId = lessonId,
					UserId = userId
				};
				_context.CompletedLesson.Add(complete);
				await _context.SaveChangesAsync();
			}

			return true;
		}
	}
}
