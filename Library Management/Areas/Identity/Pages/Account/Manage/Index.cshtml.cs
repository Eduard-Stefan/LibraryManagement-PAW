using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Library_Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library_Management.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public User? user;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUploadImageAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var imageFile = Request.Form.Files["ImageFile"];
            if (imageFile != null && imageFile.Length > 0)
            {
                // Save the image file
                var fileName = $"{Guid.NewGuid()}{System.IO.Path.GetExtension(imageFile.FileName)}";
                var imagePath = System.IO.Path.Combine("wwwroot", "profile-images", fileName);
                using (var fileStream = new System.IO.FileStream(imagePath, System.IO.FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Update the user's profile picture
                user.ImageFileName = fileName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    StatusMessage = "Profile picture updated successfully.";
                }
                else
                {
                    StatusMessage = "Error updating profile picture.";
                }
            }

            return RedirectToPage();
        }
    }
}
