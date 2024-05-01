using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Library_Management.Controllers
{
	public class BorrowedBookController : Controller
	{
		private readonly IBorrowedBookService _borrowedBookService;

		public BorrowedBookController(IBorrowedBookService borrowedBookService)
		{
			_borrowedBookService = borrowedBookService;
		}

		[Authorize(Roles = "admin")]
		public IActionResult Index()
		{
			return View(_borrowedBookService.FindAll());
		}

		[Authorize(Roles = "admin")]
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

		[Authorize(Roles = "admin")]
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

		[Authorize]
		public IActionResult MyBorrowedBooks()
		{
			var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
			return View(_borrowedBookService.FindAllByUserId(userId));
		}
	}
}