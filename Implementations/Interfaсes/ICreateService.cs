using OnlineCourses.Domain.Entities;
using OnlineCourses.Domain.Enum;
using OnlineCourses.Domain.Response;

namespace OnlineCourses.Implementations.Interfaсes
{
	public interface ICreateService
	{
		Task<SubStatus> SwapStatusSubscription(Subscription subscription);
		Task<StatusCourse> SwapStatusCourse(Course course);
		Task<BaseResponse<Course>> AddCourseAsync(Course course);
		Task<bool> DeleteCourseAsync(Course course);

		Task<bool> AddModuleAsync(Module module);
		Task<bool> UpdateModuleAsync(Module module);
		Task<bool> DeleteModuleAsync(Module module);

		Task<bool> AddLessonAsync(Lesson lesson);
		Task<bool> UpdateLessonAsync(Lesson lesson);
		Task<bool> DeleteLessonAsync(Lesson lesson);

		Task<bool> AddContentAsync(LessonContent lessonContent);
		Task<bool> UpdateContentAsync(LessonContent lessonContent);
		Task<bool> DeleteContentAsync(LessonContent lessonContent);

		Task<bool> AddLessonAndContentAsync(Lesson lesson, LessonContent lessonContent);
		Task<bool> DeleteLessonAndContentAsync(Lesson lesson, LessonContent lessonContent);
	}
}
