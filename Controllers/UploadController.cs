using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Implementations.Interfaсes;
using System.Security.Claims;

namespace OnlineCourses.Controllers
{
    [Authorize]
	public class UploadController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly IUserService _userService;

		public UploadController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnviroment, IUserService userService)
		{
			_hostingEnvironment = hostingEnviroment;
			_userService = userService;
			_logger = logger;
		}

		[HttpPost]
		public IActionResult UploadAvatar(IFormFile file)
		{
			if (file != null && file.Length > 0)
			{
				// Генерируем новое имя файла
				var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
				var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "image", "avatars", newFileName);

				// Сохраняем файл
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
				
				_userService.UpdateAvatar(_userService.GetCurrentUser().Id, newFileName);

				// Возвращаем имя файла обратно клиенту
				return Json(new { fileName = newFileName });
			}

			return BadRequest();
		}

		[HttpPost]
		[Route("api/[controller]/[action]")]
		public IActionResult Post(IFormFile upload)
		{
			if (upload.Length <= 0) return BadRequest();

			var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(upload.FileName);
			var path = Path.Combine(_hostingEnvironment.WebRootPath, "image", "ContentImages", fileName);

			using (var stream = new FileStream(path, FileMode.Create))
			{
				upload.CopyTo(stream);
			}

			var url = $"{Request.Scheme}://{Request.Host}/image/ContentImages/{fileName}";

			return Ok(new { uploaded = true, url });
		}
	}
}
