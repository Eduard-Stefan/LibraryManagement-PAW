using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}

		public async Task<IActionResult> Books()
		{
			var applicationDbContext = _context.Books.Include(b => b.Author).Include(b => b.Genre).Include(b => b.Publisher);
			return View(await applicationDbContext.ToListAsync());
		}

		public async Task<IActionResult> Subsidiaries()
		{
			return View(await _context.Subsidiaries.ToListAsync());
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
