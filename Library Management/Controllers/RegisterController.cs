using Library_Management.Models;
using Library_Management.Services;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	public class RegisterController : Controller
	{
		private readonly IRegisterService _registerService;
		private readonly IWebHostEnvironment _environment;

		public RegisterController(IRegisterService registerService, IWebHostEnvironment environment)
		{
			_registerService = registerService;
			_environment = environment;
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
				string newFileName = "default.png";
				if (model.ImageFile != null)
				{
					newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
					newFileName += Path.GetExtension(model.ImageFile!.FileName);

					string imageFullPath = _environment.WebRootPath + "/profile-images/" + newFileName;
					using (var stream = System.IO.File.Create(imageFullPath))
					{
						model.ImageFile.CopyTo(stream);
					}
				}

				model.ImageFileName = newFileName;

				var result = await _registerService.RegisterAsync(model);
				if (result.Succeeded)
				{
					TempData["SuccessMessage"] = "Registration successful!";
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
