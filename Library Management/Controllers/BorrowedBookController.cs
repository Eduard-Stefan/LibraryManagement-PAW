using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	[Authorize(Roles = "admin")]
	public class BorrowedBookController : Controller
	{
		private readonly IBorrowedBookService _borrowedBookService;

		public BorrowedBookController(IBorrowedBookService borrowedBookService)
		{
			_borrowedBookService = borrowedBookService;
		}

		public IActionResult Index()
		{
			return View(_borrowedBookService.FindAll());
		}

		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var borrowedBook = _borrowedBookService.FindById(id.Value);
			if (borrowedBook == null)
			{
				return NotFound();
			}

			return View(borrowedBook);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var borrowedBook = _borrowedBookService.FindById(id);
			if (borrowedBook != null)
			{
				borrowedBook.BookSubsidiary.Quantity++;
				_borrowedBookService.Update(borrowedBook);
				_borrowedBookService.Delete(borrowedBook);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}