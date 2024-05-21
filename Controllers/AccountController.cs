using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Models.ViewModels.Account;
using System.Security.Claims;
using OnlineCourses.Implementations.Interfaсes;

namespace OnlineCourses.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly IUserService _userService;
		private readonly ILogger<AccountController> _logger;

		public AccountController(IAccountService accountService, IUserService userService, ILogger<AccountController> logger)
		{
			_accountService = accountService;
			_userService = userService;
			_logger = logger;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register() => View();

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _accountService.Register(model);
				if (response.StatusCode == Domain.Enum.StatusCode.OK)
				{
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(response.Data));

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", response.Description);
			}
			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login() => View();

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _accountService.Login(model);
				if (response.StatusCode == Domain.Enum.StatusCode.OK)
				{
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(response.Data));

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", response.Description);
			}
			return View(model);
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}

		public IActionResult ChangePassword() => View();

		[HttpPost]
		public async Task<IActionResult> ChangePassword(string newPassword)
		{
			var user = _userService.GetCurrentUser();
			await _accountService.ChangePassword(newPassword, user);

			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
	}
}
