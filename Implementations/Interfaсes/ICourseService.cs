using OnlineCourses.Domain.Entities;
using OnlineCourses.Models.ViewModels.Create;
using OnlineCourses.Models.ViewModels.Learning;

namespace OnlineCourses.Implementations.Interfaсes
{
	public interface ICourseService
	{
		IEnumerable<Course> GetCourses();
		IEnumerable<Course> GetPublishedCourse();
		Task<Course> GetCourseByIdAsync(Guid id);
		IEnumerable<Course> GetCourseByUserId(Guid userId);
		IEnumerable<Course> GetSubsCourseByUserId(Guid userId);
		int GetSubsCountByCourseId(Guid courseId);
		Task<List<UserAndSub>> GetUserAndSubListByCourseId(Guid courseId);
		Task<List<CompleteCourseList>> GetCompleteCourseList(Guid courseId, Guid userId);
		CreateCourseViewModel GetStructureCreateCourseById(Guid id);
		Task<CourseViewModel> GetStructureCourseByCourseAndUserId(Guid courseId, Guid userId);
	}
}
