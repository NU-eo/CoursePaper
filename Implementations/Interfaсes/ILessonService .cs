using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Implementations.Interfaсes
{
	public interface ILessonService
	{
		Task<Lesson> GetLessonByIdAsync(Guid id);
		Task<LessonContent> GetContentByLessonIdAsync(Guid id);
		Task<int?> GetMaxOrderNumByModuleIdAsync(Guid moduleId);
		Task<Lesson> GetLessonByModuleId(Guid moduleId);
		IEnumerable<Lesson> GetLessonsByModuleId(Guid courseId);
		Task<Lesson?> GetFirstLessonByModuleIdAsync(Guid moduleId);
	}
}
