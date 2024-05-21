using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Domain.Enum;
using OnlineCourses.Implementations.Helpers;
using System.Security.Cryptography.Xml;

namespace OnlineCourses.Domain
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Answer> Answer { get; set; }
		public DbSet<CompletedLesson> CompletedLesson { get; set; }
		public DbSet<Course> Course { get; set; }
		public DbSet<Lesson> Lesson { get; set; }
		public DbSet<LessonContent> LessonContent { get; set; }
		public DbSet<Module> Module { get; set; }
		public DbSet<Question> Question { get; set; }
		public DbSet<Subscription> Subscription { get; set; }
		public DbSet<Test> Test { get; set; }
		public DbSet<TestResult> TestResult { get; set; }
		public DbSet<User> User { get; set; }
		public object CompletedLessons { get; internal set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Answer>(builder =>
			{
				//Определение Id
				builder.ToTable("Answer").HasKey(x => x.Id);
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.QuestionId).IsRequired();
				builder.Property(x => x.AnswerText).HasMaxLength(100).IsRequired();
				builder.Property(x => x.IsCorrect).IsRequired();

				//Связь с таблицей
				builder.HasOne(a => a.Question).WithMany(q => q.Answers).HasForeignKey(a => a.QuestionId);
			});

			modelBuilder.Entity<CompletedLesson>(builder =>
			{
				//Определение Id
				builder.ToTable("CompletedLesson").HasKey(x => x.Id);
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.UserId).IsRequired();
				builder.Property(x => x.LessonId).IsRequired();
				builder.Property(x => x.DateSubscribed).IsRequired();

				//Связь с таблицей
				builder.HasOne(l => l.User).WithMany(u => u.CompletedLessons).HasForeignKey(a => a.UserId);
				builder.HasOne(l => l.Lesson).WithMany(u => u.CompletedLessons).HasForeignKey(a => a.LessonId);
			});

			modelBuilder.Entity<Course>(builder =>
			{
				//Определение Id
				builder.ToTable("Course").HasKey(x => x.Id);
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
				builder.Property(x => x.ShortDescription).HasMaxLength(200).IsRequired();
				builder.Property(x => x.AboutCourse).HasMaxLength(1200).IsRequired();
				builder.Property(x => x.ForWhom).HasMaxLength(100).IsRequired();
				builder.Property(x => x.UserId).IsRequired();
				builder.Property(x => x.DateCreate).IsRequired();

				//Связь с таблицей
				builder.HasOne(c => c.User).WithMany(u => u.Courses).HasForeignKey(a => a.UserId);
				builder.HasMany(c => c.Modules).WithOne(m => m.Course).HasForeignKey(m => m.CourseId);
				builder.HasMany(c => c.Subscriptions).WithOne(s => s.Course).HasForeignKey(s => s.CourseId);
			});

			modelBuilder.Entity<Lesson>(builder =>
			{
				//Определение Id
				builder.ToTable("Lesson").HasKey(x => x.Id);
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
				builder.Property(x => x.ModuleId).IsRequired();
				builder.Property(x => x.OrderNum).IsRequired();
				builder.Property(x => x.DateCreate).IsRequired();

				//Связь с таблицей
				builder.HasOne(l => l.Module).WithMany(m => m.Lessons).HasForeignKey(l => l.ModuleId);
				builder.HasMany(u => u.LessonContent).WithOne(b => b.Lesson).HasForeignKey(b => b.LessonId);
				builder.HasMany(c => c.CompletedLessons).WithOne(l => l.Lesson).HasForeignKey(l => l.LessonId);
			});

			modelBuilder.Entity<LessonContent>(builder =>
			{
				//Определение Id
				builder.ToTable("LessonContent").HasKey(x => x.Id);
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.LessonId).IsRequired();

				//Связь с таблицей
				builder.HasOne(b => b.Lesson).WithMany(u => u.LessonContent).HasForeignKey(b => b.LessonId);
				builder.HasOne(l => l.Test).WithOne(l => l.LessonContent).HasForeignKey<Test>(l => l.Id);
			});

			modelBuilder.Entity<Module>(builder =>
			{
				//Определение Id
				builder.ToTable("Module").HasKey(x => x.Id);
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
				builder.Property(x => x.CourseId).IsRequired();
				builder.Property(x => x.OrderNum).IsRequired();
				builder.Property(x => x.DateCreate).IsRequired();

				//Связь с таблицей
				builder.HasOne(l => l.Course).WithMany(m => m.Modules).HasForeignKey(l => l.CourseId);
				builder.HasMany(c => c.Lessons).WithOne(l => l.Module).HasForeignKey(l => l.ModuleId);
			});

			modelBuilder.Entity<Question>(builder =>
			{
				//Определение Id
				builder.ToTable("Question").HasKey(x => x.Id);
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.TestId).IsRequired();
				builder.Property(x => x.QuestionText).HasMaxLength(100).IsRequired();

				//Связь с таблицей
				builder.HasOne(l => l.Test).WithMany(m => m.Questions).HasForeignKey(l => l.TestId);
				builder.HasMany(c => c.Answers).WithOne(l => l.Question).HasForeignKey(l => l.QuestionId);
			});

			modelBuilder.Entity<Subscription>(builder =>
			{
				//Определение Id
				builder.ToTable("Subscription").HasKey(x => x.Id);
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.UserId).IsRequired();
				builder.Property(x => x.CourseId).IsRequired();
				builder.Property(x => x.DateCreate).IsRequired();

				//Связь с таблицей
				builder.HasOne(l => l.User).WithMany(m => m.Subscriptions).HasForeignKey(l => l.UserId);
				builder.HasOne(l => l.Course).WithMany(m => m.Subscriptions).HasForeignKey(l => l.CourseId);
			});

			modelBuilder.Entity<Test>(builder =>
			{
				//Определение Id
				builder.ToTable("Test").HasKey(x => x.Id);
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
				builder.Property(x => x.DateCreate).IsRequired();

				//Связь с таблицей
				builder.HasOne(l => l.LessonContent).WithOne(l => l.Test).HasForeignKey<LessonContent>(l => l.TestId);
				builder.HasMany(u => u.Questions).WithOne(c => c.Test).HasForeignKey(c => c.TestId);
				builder.HasMany(u => u.TestResults).WithOne(c => c.Test).HasForeignKey(c => c.TestId);
			});

			modelBuilder.Entity<TestResult>(builder =>
			{
				//Определение Id
				builder.ToTable("TestResult").HasKey(x => x.Id);
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.UserId).IsRequired();
				builder.Property(x => x.TestId).IsRequired();
				builder.Property(x => x.DateComplete).IsRequired();
				builder.Property(x => x.Grade).IsRequired();

				//Связь с таблицей
				builder.HasOne(l => l.User).WithMany(m => m.TestResults).HasForeignKey(l => l.UserId);
				builder.HasOne(l => l.Test).WithMany(m => m.TestResults).HasForeignKey(l => l.TestId);
			});

			modelBuilder.Entity<User>(builder =>
			{
				//Определение Id
				builder.ToTable("User").HasKey(x => x.Id);
				//Начатьные параметры
				builder.HasData(new User[]
				{
					new User()
					{
						Id = Guid.NewGuid(),
						UserName = "admin",
						Email = "admin@mail.com",
						Login = "Admin",
						Password = HashPasswordHelper.HashPassword("123456"),
						Status = SubStatus.Open,
						Role = Role.Admin
					}
				});
				//Автогенерация Id
				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				//Условия
				builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
				builder.Property(x => x.Login).HasMaxLength(100).IsRequired();
				builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
				builder.Property(x => x.Password).IsRequired();

				//Определение связи с таблицами
				builder.HasMany(u => u.Courses).WithOne(c => c.User).HasForeignKey(c => c.UserId);
				builder.HasMany(u => u.Subscriptions).WithOne(s => s.User).HasForeignKey(s => s.UserId);
				builder.HasMany(u => u.CompletedLessons).WithOne(l => l.User).HasForeignKey(l => l.UserId);
				builder.HasMany(u => u.TestResults).WithOne(t => t.User).HasForeignKey(t => t.UserId);
			});


		}


	}
}
