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
    public class BookSubsidiariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookSubsidiariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookSubsidiaries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BooksSubsidiaries.Include(b => b.Book).Include(b => b.Subsidiary);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookSubsidiaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSubsidiary = await _context.BooksSubsidiaries
                .Include(b => b.Book)
                .Include(b => b.Subsidiary)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookSubsidiary == null)
            {
                return NotFound();
            }

            return View(bookSubsidiary);
        }

        // GET: BookSubsidiaries/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["SubsidiaryId"] = new SelectList(_context.Subsidiaries, "Id", "Id");
            return View();
        }

        // POST: BookSubsidiaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,BookId,SubsidiaryId")] BookSubsidiary bookSubsidiary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookSubsidiary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", bookSubsidiary.BookId);
            ViewData["SubsidiaryId"] = new SelectList(_context.Subsidiaries, "Id", "Id", bookSubsidiary.SubsidiaryId);
            return View(bookSubsidiary);
        }

        // GET: BookSubsidiaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSubsidiary = await _context.BooksSubsidiaries.FindAsync(id);
            if (bookSubsidiary == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", bookSubsidiary.BookId);
            ViewData["SubsidiaryId"] = new SelectList(_context.Subsidiaries, "Id", "Id", bookSubsidiary.SubsidiaryId);
            return View(bookSubsidiary);
        }

        // POST: BookSubsidiaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,BookId,SubsidiaryId")] BookSubsidiary bookSubsidiary)
        {
            if (id != bookSubsidiary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookSubsidiary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookSubsidiaryExists(bookSubsidiary.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", bookSubsidiary.BookId);
            ViewData["SubsidiaryId"] = new SelectList(_context.Subsidiaries, "Id", "Id", bookSubsidiary.SubsidiaryId);
            return View(bookSubsidiary);
        }

        // GET: BookSubsidiaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookSubsidiary = await _context.BooksSubsidiaries
                .Include(b => b.Book)
                .Include(b => b.Subsidiary)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookSubsidiary == null)
            {
                return NotFound();
            }

            return View(bookSubsidiary);
        }

        // POST: BookSubsidiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookSubsidiary = await _context.BooksSubsidiaries.FindAsync(id);
            if (bookSubsidiary != null)
            {
                _context.BooksSubsidiaries.Remove(bookSubsidiary);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookSubsidiaryExists(int id)
        {
            return _context.BooksSubsidiaries.Any(e => e.Id == id);
        }
    }
}
