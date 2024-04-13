﻿using Microsoft.AspNetCore.Identity;

namespace Library_Management.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; } = "";
		public string Address { get; set; } = "";
		public string PhoneNumber { get; set; } = "";
		public bool IsWelcome { get; set; } = true;
		public ICollection<BorrowedBook>? BorrowedBooks { get; set; }
	}
}