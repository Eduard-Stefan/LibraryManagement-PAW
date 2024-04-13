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
    public class BorrowedBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BorrowedBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BorrowedBooks
        public async Task<IActionResult> Index()
        {
	        var applicationDbContext = _context.BorrowedBooks
		        .Include(b => b.BookSubsidiary)
		        .ThenInclude(bs => bs.Book)
		        .Include(b => b.BookSubsidiary)
		        .ThenInclude(bs => bs.Subsidiary)
		        .Include(b => b.User);
	        return View(await applicationDbContext.ToListAsync());
        }

        // GET: BorrowedBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedBook = await _context.BorrowedBooks
                .Include(b => b.BookSubsidiary)
		        .ThenInclude(bs => bs.Book)
		        .Include(b => b.BookSubsidiary)
		        .ThenInclude(bs => bs.Subsidiary)
		        .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowedBook == null)
            {
                return NotFound();
            }

            return View(borrowedBook);
        }

        // POST: BorrowedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowedBook = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBook != null)
            {
                _context.BorrowedBooks.Remove(borrowedBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowedBookExists(int id)
        {
            return _context.BorrowedBooks.Any(e => e.Id == id);
        }
    }
}
