using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Implementations.Interfaсes
{
	public interface ILearningService
	{
		Task<Subscription> GetSubById(Guid subId);
		Task<CompletedLesson> GetCompleteByLessonAndUserId(Guid lessonId, Guid userId);
		Task<int> GetCompletedLessonCount(Guid userId, Guid courseId);
		Task<bool> AddSubscribeAsync(Subscription sub);
		Task<Subscription> GetSubscription(Guid userId, Guid courseId);
		Task<bool> CompleteLesson(Guid lessonId, Guid userId);
	}
}
