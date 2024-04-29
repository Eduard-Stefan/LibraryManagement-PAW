using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	public class LogoutController : Controller
	{
		private readonly ILogoutService _logoutService;

		public LogoutController(ILogoutService logoutService)
		{
			_logoutService = logoutService;
		}

		public async Task<IActionResult> Index()
		{
			await _logoutService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
