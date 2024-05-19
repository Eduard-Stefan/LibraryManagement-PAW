using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	[Authorize(Roles = "admin")]
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		public IActionResult Index()
		{
			_userService.UnwelcomeUsers();
			return View(_userService.FindAll());
		}

		public IActionResult Edit(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = _userService.FindById(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(string id, [Bind("Id,Name,Email,Address,PhoneNumber,IsWelcome")] User user)
		{
			if (id != user.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				var existingUser = _userService.FindById(id);
				if (existingUser == null)
				{
					return NotFound();
				}

				existingUser.Name = user.Name;
				existingUser.Email = user.Email;
				existingUser.Address = user.Address;
				existingUser.PhoneNumber = user.PhoneNumber;
				existingUser.IsWelcome = user.IsWelcome;

				_userService.Update(existingUser);
				return RedirectToAction(nameof(Index));
			}

			return View(user);
		}

		public IActionResult Delete(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = _userService.FindById(id);
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(string id)
		{
			var user = _userService.FindById(id);
			if (user != null)
			{
				_userService.Delete(user);
			}
			return RedirectToAction(nameof(Index));
		}

		public IActionResult UnwelcomeUsers()
		{
			return View(_userService.UnwelcomeUsers());
		}
	}
}