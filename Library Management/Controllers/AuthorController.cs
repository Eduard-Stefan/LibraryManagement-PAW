using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	[Authorize(Roles = "admin")]
	public class AuthorController : Controller
	{
		private readonly IAuthorService _authorService;

		public AuthorController(IAuthorService authorService)
		{
			_authorService = authorService;
		}

		public IActionResult Index()
		{
			return View(_authorService.FindAll());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Name,Bio")] Author author)
		{
			if (ModelState.IsValid)
			{
				_authorService.Create(author);
				return RedirectToAction(nameof(Index));
			}
			return View(author);
		}

		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var author = _authorService.FindById(id.Value);
			if (author == null)
			{
				return NotFound();
			}
			return View(author);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,Name,Bio")] Author author)
		{
			if (id != author.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				_authorService.Update(author);
				return RedirectToAction(nameof(Index));
			}
			return View(author);
		}

		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var author = _authorService.FindById(id.Value);
			if (author == null)
			{
				return NotFound();
			}

			return View(author);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var author = _authorService.FindById(id);
			if (author != null)
			{
				_authorService.Delete(author);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}