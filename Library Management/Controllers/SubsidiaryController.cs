using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	[Authorize(Roles = "admin")]
	public class SubsidiaryController : Controller
	{
		private readonly ISubsidiaryService _subsidiaryService;

		public SubsidiaryController(ISubsidiaryService subsidiaryService)
		{
			_subsidiaryService = subsidiaryService;
		}

		public IActionResult Index()
		{
			return View(_subsidiaryService.FindAll());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Name,Address")] Subsidiary subsidiary)
		{
			if (ModelState.IsValid)
			{
				_subsidiaryService.Create(subsidiary);
				return RedirectToAction(nameof(Index));
			}
			return View(subsidiary);
		}

		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var subsidiary = _subsidiaryService.FindById(id.Value);
			if (subsidiary == null)
			{
				return NotFound();
			}
			return View(subsidiary);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,Name,Address")] Subsidiary subsidiary)
		{
			if (id != subsidiary.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				_subsidiaryService.Update(subsidiary);
				return RedirectToAction(nameof(Index));
			}
			return View(subsidiary);
		}

		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var subsidiary = _subsidiaryService.FindById(id.Value);
			if (subsidiary == null)
			{
				return NotFound();
			}

			return View(subsidiary);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var subsidiary = _subsidiaryService.FindById(id);
			if (subsidiary != null)
			{
				_subsidiaryService.Delete(subsidiary);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}