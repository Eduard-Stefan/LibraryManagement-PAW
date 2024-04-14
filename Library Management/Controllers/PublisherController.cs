using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
	[Authorize(Roles = "admin")]
	public class PublisherController : Controller
	{
		private readonly IPublisherService _publisherService;

		public PublisherController(IPublisherService publisherService)
		{
			_publisherService = publisherService;
		}

		public IActionResult Index()
		{
			return View(_publisherService.FindAll());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Name,Address")] Publisher publisher)
		{
			if (ModelState.IsValid)
			{
				_publisherService.Create(publisher);
				return RedirectToAction(nameof(Index));
			}
			return View(publisher);
		}

		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publisher = _publisherService.FindById(id.Value);
			if (publisher == null)
			{
				return NotFound();
			}
			return View(publisher);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,Name,Address")] Publisher publisher)
		{
			if (id != publisher.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				_publisherService.Update(publisher);
				return RedirectToAction(nameof(Index));
			}
			return View(publisher);
		}

		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publisher = _publisherService.FindById(id.Value);
			if (publisher == null)
			{
				return NotFound();
			}

			return View(publisher);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var publisher = _publisherService.FindById(id);
			if (publisher != null)
			{
				_publisherService.Delete(publisher);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}