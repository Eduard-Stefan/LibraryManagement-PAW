using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library_Management.Controllers
{
	[Authorize(Roles = "admin")]
	public class BookSubsidiaryController : Controller
	{
		private readonly IBookSubsidiaryService _bookSubsidiaryService;
		private readonly IBookService _bookService;
		private readonly ISubsidiaryService _subsidiaryService;

		public BookSubsidiaryController(IBookSubsidiaryService bookSubsidiaryService, IBookService bookService, ISubsidiaryService subsidiaryService)
		{
			_bookSubsidiaryService = bookSubsidiaryService;
			_bookService = bookService;
			_subsidiaryService = subsidiaryService;
		}

		public IActionResult Index()
		{
			return View(_bookSubsidiaryService.FindAll());
		}

		public IActionResult Create()
		{
			ViewData["BookId"] = new SelectList(_bookService.FindAll(), "Id", "Title");
			ViewData["SubsidiaryId"] = new SelectList(_subsidiaryService.FindAll(), "Id", "Name");

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Quantity,BookId,SubsidiaryId")] BookSubsidiary bookSubsidiary)
		{
			ViewData["BookId"] = new SelectList(_bookService.FindAll(), "Id", "Title");
			ViewData["SubsidiaryId"] = new SelectList(_subsidiaryService.FindAll(), "Id", "Name");

			if (ModelState.IsValid)
			{
				_bookSubsidiaryService.Create(bookSubsidiary);
				return RedirectToAction(nameof(Index));
			}
			return View(bookSubsidiary);
		}

		public IActionResult Edit(int? id)
		{
			ViewData["BookId"] = new SelectList(_bookService.FindAll(), "Id", "Title");
			ViewData["SubsidiaryId"] = new SelectList(_subsidiaryService.FindAll(), "Id", "Name");

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
			ViewData["BookId"] = new SelectList(_bookService.FindAll(), "Id", "Title");
			ViewData["SubsidiaryId"] = new SelectList(_subsidiaryService.FindAll(), "Id", "Name");

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