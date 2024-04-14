using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	[Authorize(Roles = "admin")]
	public class GenreController : Controller
	{
		private readonly IGenreService _genreService;

		public GenreController(IGenreService genreService)
		{
			_genreService = genreService;
		}

		public IActionResult Index()
		{
			return View(_genreService.FindAll());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Name,Description")] Genre genre)
		{
			if (ModelState.IsValid)
			{
				_genreService.Create(genre);
				return RedirectToAction(nameof(Index));
			}
			return View(genre);
		}

		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var genre = _genreService.FindById(id.Value);
			if (genre == null)
			{
				return NotFound();
			}
			return View(genre);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,Name,Description")] Genre genre)
		{
			if (id != genre.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				_genreService.Update(genre);
				return RedirectToAction(nameof(Index));
			}
			return View(genre);
		}

		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var genre = _genreService.FindById(id.Value);
			if (genre == null)
			{
				return NotFound();
			}

			return View(genre);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var genre = _genreService.FindById(id);
			if (genre != null)
			{
				_genreService.Delete(genre);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}