using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library_Management.Models;

namespace Library_Management.Controllers
{
	//[Authorize(Roles = "admin")]
    public class BooksController : Controller
    {
	    private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Books.Include(b => b.Author).Include(b => b.Genre).Include(b => b.Publisher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,AuthorId,PublisherId,GenreId,ImageFileName,ImageFile")] Book book)
        {
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

		        _context.Add(book);
		        await _context.SaveChangesAsync();
		        return RedirectToAction(nameof(Index));
	        }

	        ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id", book.PublisherId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id", book.PublisherId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,AuthorId,PublisherId,GenreId,ImageFileName,ImageFile")] Book book)
        {
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
                try
                {
	                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
	                newFileName += Path.GetExtension(book.ImageFile!.FileName);

	                string imageFullPath = _environment.WebRootPath + "/books/" + newFileName;
	                using (var stream = System.IO.File.Create(imageFullPath)) 
	                {
		                book.ImageFile.CopyTo(stream);
	                }

	                book.ImageFileName = newFileName;

	                _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Id", book.PublisherId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
