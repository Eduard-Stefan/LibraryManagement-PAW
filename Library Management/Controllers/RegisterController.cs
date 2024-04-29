using Library_Management.Models;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	public class RegisterController : Controller
	{
		private readonly IRegisterService _registerService;

		public RegisterController(IRegisterService registerService)
		{
			_registerService = registerService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _registerService.RegisterAsync(model);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(model);
		}

	}
}
