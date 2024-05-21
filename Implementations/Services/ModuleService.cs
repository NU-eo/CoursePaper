using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Implementations.Interfaсes;

namespace OnlineCourses.Implementations.Services
{
	public class ModuleService : IModuleService
	{
		private readonly AppDbContext _context;
		private readonly ILogger<AccountService> _logger;

		public ModuleService(AppDbContext context, ILogger<AccountService> logger)
		{
			_context = context;
			_logger = logger;
		}
		public async Task<Module> GetModuleByIdAsync(Guid? id)
		{
			return await _context.Module.FirstOrDefaultAsync(m => m.Id == id);
		}

		public IEnumerable<Module> GetModulesByCourseId(Guid courseId)
		{
			return _context.Module.Where(m => m.CourseId == courseId);
		}

		public async Task<int?> GetMaxOrderNumByCourseIdAsync(Guid courdeId)
		{
			return await _context.Module.Where(m => m.CourseId == courdeId).MaxAsync(m => (int?)m.OrderNum) ?? 0;
		}

		public async Task<Module?> GetFirstModuleByCourseIdAsync(Guid courseId)
		{
			return await _context.Module
				.Where(m => m.CourseId == courseId)
				.OrderBy(m => m.OrderNum)
				.FirstOrDefaultAsync();

		}
	}
}
