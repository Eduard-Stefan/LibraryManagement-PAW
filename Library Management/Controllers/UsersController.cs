using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library_Management.Models;
using Microsoft.AspNetCore.Authorization;

namespace Library_Management.Controllers
{
	//[Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Email,Address,PhoneNumber,IsWelcome")] User user)
        {
	        if (id != user.Id)
	        {
		        return NotFound();
	        }

	        if (ModelState.IsValid)
	        {
		        // Retrieve the existing user from the database
		        var existingUser = await _context.Users.FindAsync(id);
		        if (existingUser == null)
		        {
			        return NotFound();
		        }

		        // Update the properties of the existing user with the values from the user input
		        existingUser.Name = user.Name;
		        existingUser.Email = user.Email;
		        existingUser.Address = user.Address;
		        existingUser.PhoneNumber = user.PhoneNumber;
		        existingUser.IsWelcome = user.IsWelcome;

		        // Save changes to the user's properties (excluding the password)
		        _context.Update(existingUser);
		        await _context.SaveChangesAsync();

		        // Redirect to the index page after successful update
		        return RedirectToAction(nameof(Index));
	        }

	        return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
