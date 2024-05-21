using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Implementations.Interfaсes;

namespace OnlineCourses.Implementations.Services
{
	public class LessonService : ILessonService
	{
		private readonly AppDbContext _context;
		private readonly ILogger<AccountService> _logger;

		public LessonService(AppDbContext context, ILogger<AccountService> logger)
		{
			_context = context;
			_logger = logger;
		}
		public async Task<Lesson> GetLessonByIdAsync(Guid id)
		{
			return await _context.Lesson.FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task<LessonContent> GetContentByLessonIdAsync(Guid id)
		{
			return await _context.LessonContent.FirstOrDefaultAsync(m => m.LessonId == id);
		}

		public IEnumerable<Lesson> GetLessonsByModuleId(Guid moduleId)
		{
			return _context.Lesson.Where(m => m.ModuleId == moduleId);
		}

		public async Task<Lesson> GetLessonByModuleId(Guid moduleId)
		{
			return await _context.Lesson.FirstOrDefaultAsync(m => m.ModuleId == moduleId);
		}

		public async Task<int?> GetMaxOrderNumByModuleIdAsync(Guid moduleId)
		{
			return await _context.Lesson.Where(l => l.ModuleId == moduleId).MaxAsync(m => (int?)m.OrderNum) ?? 0;
		}

		public async Task<Lesson?> GetFirstLessonByModuleIdAsync(Guid moduleId)
		{
			return await _context.Lesson
				.Where(l => l.ModuleId == moduleId)
				.OrderBy(l => l.OrderNum)
				.FirstOrDefaultAsync();
		}
	}
}
