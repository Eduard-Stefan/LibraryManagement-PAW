using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Library_Management.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IBookService _bookService;
		private readonly ISubsidiaryService _subsidiaryService;
		private readonly IBookSubsidiaryService _bookSubsidiaryService;
		private readonly IBorrowedBookService _borrowedBookService;
		private readonly IUserService _userService;

		public HomeController(ILogger<HomeController> logger, IBookService bookService, ISubsidiaryService subsidiaryService, IBookSubsidiaryService bookSubsidiaryService, IBorrowedBookService borrowedBookService, IUserService userService)
		{
			_logger = logger;
			_bookService = bookService;
			_subsidiaryService = subsidiaryService;
			_bookSubsidiaryService = bookSubsidiaryService;
			_borrowedBookService = borrowedBookService;
			_userService = userService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult Books()
		{
			return View(_bookService.FindAll());
		}

		public IActionResult Subsidiaries()
		{
			return View(_subsidiaryService.FindAll());
		}

		[HttpGet]
		public IActionResult Borrow(int id)
		{
			return View(_bookSubsidiaryService.FindByBookIdAndQuantityGreaterThanZero(id));
		}
		
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Borrow(int id, int subsidiaryId)
		{
			_userService.UnwelcomeUsers();
			var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
			var alreadyBorrowed = _borrowedBookService.FindByBookIdAndUserId(id, userId);

			if (alreadyBorrowed != null)
			{
				TempData["ErrorMessage"] = "You have already borrowed this book.";
				return RedirectToAction("Books");
			}

			var user = _userService.FindById(userId);
			if (!user.IsWelcome)
			{
				TempData["ErrorMessage"] = "You are not welcome to borrow books.";
				return RedirectToAction("Books");
			}

			var bookSubsidiary = _bookSubsidiaryService.FindByBookIdAndSubsidiaryId(id, subsidiaryId);
			if (bookSubsidiary == null)
			{
				return NotFound();
			}
			bookSubsidiary.Quantity--;
			_bookSubsidiaryService.Update(bookSubsidiary);

			var bookSubsidiaryId = bookSubsidiary.Id;
			var borrowedDate = DateTime.Now;
			var returnDate = borrowedDate.AddDays(30);
			BorrowedBook borrowedBook = new BorrowedBook
			{
				UserId = userId,
				BookSubsidiaryId = bookSubsidiaryId,
				BorrowedDate = borrowedDate,
				ReturnDate = returnDate
			};
			_borrowedBookService.Create(borrowedBook);
			TempData["SuccessMessage"] = "The book has been successfully borrowed.";
			return RedirectToAction("Books");
		}

		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = _bookService.FindById(id.Value);
			if (book == null)
			{
				return NotFound();
			}
			return View(book);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
