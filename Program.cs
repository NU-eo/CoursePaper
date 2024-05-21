using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Domain;
using OnlineCourses.Implementations.Helpers;

// Установка отметки времени для postgrey
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Подключение конфиг из appsettings.json
builder.Configuration.Bind("Project", new Config());

// db UseSqlServer
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Config.ConnectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
	});

builder.Services.AddHttpContextAccessor();
builder.Services.InitializeServices();

// Настройка политики авторизации для admin area
builder.Services.AddAuthorization(x =>
	{ x.AddPolicy("AdminArea", policy => { policy.RequireRole("Admin"); }); });

// Поддержка контроллеров и представлений
builder.Services.AddControllersWithViews(x =>
{
	x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
