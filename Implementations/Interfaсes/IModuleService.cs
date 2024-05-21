using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Implementations.Interfaсes
{
	public interface IModuleService
	{
		Task<Module> GetModuleByIdAsync(Guid? id);
		Task<int?> GetMaxOrderNumByCourseIdAsync(Guid courdeId);
		IEnumerable<Module> GetModulesByCourseId(Guid courseId);
		Task<Module?> GetFirstModuleByCourseIdAsync(Guid courseId);
	}
}
