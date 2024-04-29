using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; } = "";
		public string Address { get; set; } = "";
		public string PhoneNumber { get; set; } = "";
		public bool IsWelcome { get; set; } = true;
		public string ImageFileName { get; set; } = "";
		[NotMapped] public IFormFile? ImageFile { get; set; }
		public ICollection<BorrowedBook>? BorrowedBooks { get; set; }
	}
}