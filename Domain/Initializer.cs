using OnlineCourses.Implementations.Interfaсes;
using OnlineCourses.Implementations.Services;

namespace OnlineCourses.Domain
{
	public static class Initializer
	{

		public static void InitializeServices(this IServiceCollection services)
		{
			services.AddScoped<ILearningService, LearningService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<ICourseService, CourseService>();
			services.AddScoped<ICreateService, CreateService>();
			services.AddScoped<ILessonService, LessonService>();
			services.AddScoped<IModuleService, ModuleService>();
			services.AddScoped<IUserService, UserService>();
		}
	}
}
