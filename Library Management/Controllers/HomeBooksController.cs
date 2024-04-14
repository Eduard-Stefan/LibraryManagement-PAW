using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	public class HomeBooksController : Controller
	{
		public IActionResult Borrow(int id)
		{
			return View();
		}

		public IActionResult Details(int id)
		{
			return View();
		}
	}
}
