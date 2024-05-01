using Library_Management.Models;
using Library_Management.Repositories.Interfaces;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library_Management.Controllers
{
	[Authorize(Roles = "admin")]
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
		private readonly IAuthorService _authorService;
		private readonly IPublisherService _publisherService;
		private readonly IGenreService _genreService;
		private readonly IWebHostEnvironment _environment;

		public BookController(IBookService bookService, IWebHostEnvironment environment, IAuthorService authorService, IPublisherService publisherService, IGenreService genreService)
		{
			_bookService = bookService;
			_environment = environment;
			_authorService = authorService;
			_publisherService = publisherService;
			_genreService = genreService;
		}

		public IActionResult Index()
		{
			return View(_bookService.FindAll());
		}

		public IActionResult Create()
		{
			ViewData["AuthorId"] = new SelectList(_authorService.FindAll(), "Id", "Name");
			ViewData["PublisherId"] = new SelectList(_publisherService.FindAll(), "Id", "Name");
			ViewData["GenreId"] = new SelectList(_genreService.FindAll(), "Id", "Name");

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Title,AuthorId,PublisherId,GenreId,ImageFileName,ImageFile")] Book book)
		{
			ViewData["AuthorId"] = new SelectList(_authorService.FindAll(), "Id", "Name");
			ViewData["PublisherId"] = new SelectList(_publisherService.FindAll(), "Id", "Name");
			ViewData["GenreId"] = new SelectList(_genreService.FindAll(), "Id", "Name");

			if (book.ImageFile == null)
			{
				ModelState.AddModelError("ImageFile", "The ImageFile field is required.");
			}
			if (ModelState.IsValid)
			{
				string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				newFileName += Path.GetExtension(book.ImageFile!.FileName);

				string imageFullPath = _environment.WebRootPath + "/books/" + newFileName;
				using (var stream = System.IO.File.Create(imageFullPath))
				{
					book.ImageFile.CopyTo(stream);
				}

				book.ImageFileName = newFileName;

				_bookService.Create(book);
				return RedirectToAction(nameof(Index));
			}
			return View(book);
		}

		public IActionResult Edit(int? id)
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

			ViewData["AuthorId"] = new SelectList(_authorService.FindAll(), "Id", "Name");
			ViewData["PublisherId"] = new SelectList(_publisherService.FindAll(), "Id", "Name");
			ViewData["GenreId"] = new SelectList(_genreService.FindAll(), "Id", "Name");

			return View(book);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,Title,AuthorId,PublisherId,GenreId,ImageFileName,ImageFile")] Book book)
		{
			ViewData["AuthorId"] = new SelectList(_authorService.FindAll(), "Id", "Name");
			ViewData["PublisherId"] = new SelectList(_publisherService.FindAll(), "Id", "Name");
			ViewData["GenreId"] = new SelectList(_genreService.FindAll(), "Id", "Name");

			if (id != book.Id)
			{
				return NotFound();
			}

			if (book.ImageFile == null)
			{
				ModelState.AddModelError("ImageFile", "The ImageFile field is required.");
			}

			if (ModelState.IsValid)
			{
				string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				newFileName += Path.GetExtension(book.ImageFile!.FileName);

				string imageFullPath = _environment.WebRootPath + "/books/" + newFileName;
				using (var stream = System.IO.File.Create(imageFullPath)) 
				{
					book.ImageFile.CopyTo(stream);
				}

				book.ImageFileName = newFileName;

				_bookService.Update(book);
				return RedirectToAction(nameof(Index));
			}
			return View(book);
		}

		public IActionResult Delete(int? id)
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

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var book = _bookService.FindById(id);
			if (book != null)
			{
				_bookService.Delete(book);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}