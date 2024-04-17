using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Library_Management.Services.Interfaces;

namespace Library_Management.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IBookService _bookService;
		private readonly ISubsidiaryService _subsidiaryService;

		public HomeController(ILogger<HomeController> logger, IBookService bookService, ISubsidiaryService subsidiaryService)
		{
			_logger = logger;
			_bookService = bookService;
			_subsidiaryService = subsidiaryService;
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

		public IActionResult Borrow(int id)
		{
			return View();
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
