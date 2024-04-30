using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Library_Management.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library_Management.Areas.Identity.Pages.Account.Manage
{
	public class IndexModel : PageModel
	{
		private readonly UserManager<User> _userManager;
		private readonly IWebHostEnvironment _environment;

		public User? user;

		public IndexModel(
			UserManager<User> userManager,
			IWebHostEnvironment environment)
		{
			_userManager = userManager;
			_environment = environment;
		}

		[BindProperty] public IFormFile ImageFile { get; set; }

		[TempData] public string StatusMessage { get; set; }

		private const string DefaultImageFileName = "default.png";

		public async Task<IActionResult> OnGetAsync()
		{
			var userTask = _userManager.GetUserAsync(User);
			userTask.Wait();
			user = userTask.Result;

			return Page();
		}

		public async Task<IActionResult> OnPostUploadImageAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			if (ImageFile != null)
			{
				string uploadsFolder = Path.Combine(_environment.WebRootPath, "profile-images");
				string uniqueFileName = $"{user.Id}_{ImageFile.FileName}";
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await ImageFile.CopyToAsync(stream);
				}

				user.ImageFileName = uniqueFileName;
				await _userManager.UpdateAsync(user);
			}

			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostRemoveImageAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			// Remove the current profile picture
			string uploadsFolder = Path.Combine(_environment.WebRootPath, "profile-images");
			string filePath = Path.Combine(uploadsFolder, user.ImageFileName);
			if (System.IO.File.Exists(filePath))
			{
				System.IO.File.Delete(filePath);
			}

			// Revert to the default image
			user.ImageFileName = DefaultImageFileName;
			await _userManager.UpdateAsync(user);

			return RedirectToPage();
		}
	}
}