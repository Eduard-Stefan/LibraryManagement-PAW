using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	[Authorize(Roles = "admin")]
	public class BookSubsidiaryController : Controller
	{
		private readonly IBookSubsidiaryService _bookSubsidiaryService;

		public BookSubsidiaryController(IBookSubsidiaryService bookSubsidiaryService)
		{
			_bookSubsidiaryService = bookSubsidiaryService;
		}

		public IActionResult Index()
		{
			return View(_bookSubsidiaryService.FindAll());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Quantity,BookId,SubsidiaryId")] BookSubsidiary bookSubsidiary)
		{
			if (ModelState.IsValid)
			{
				_bookSubsidiaryService.Create(bookSubsidiary);
				return RedirectToAction(nameof(Index));
			}
			return View(bookSubsidiary);
		}

		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var bookSubsidiary = _bookSubsidiaryService.FindById(id.Value);
			if (bookSubsidiary == null)
			{
				return NotFound();
			}
			return View(bookSubsidiary);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,Quantity,BookId,SubsidiaryId")] BookSubsidiary bookSubsidiary)
		{
			if (id != bookSubsidiary.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				_bookSubsidiaryService.Update(bookSubsidiary);
				return RedirectToAction(nameof(Index));
			}
			return View(bookSubsidiary);
		}

		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var bookSubsidiary = _bookSubsidiaryService.FindById(id.Value);
			if (bookSubsidiary == null)
			{
				return NotFound();
			}

			return View(bookSubsidiary);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var bookSubsidiary = _bookSubsidiaryService.FindById(id);
			if (bookSubsidiary != null)
			{
				_bookSubsidiaryService.Delete(bookSubsidiary);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}