using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models;

public class RegisterViewModel
{
	public string ImageFileName { get; set; } = "";
	public IFormFile? ImageFile { get; set; }

	[Required]
	[StringLength(100, ErrorMessage = "The name must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
	public string Name { get; set; }

	[Required]
	[EmailAddress]
	[Display(Name = "Email")]
	public string Email { get; set; }

	[Required]
	[Phone]
	[Display(Name = "Phone")]
	public string PhoneNumber { get; set; }

	[Required]
	[Display(Name = "Address")]
	public string Address { get; set; }

	[Required]
	[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
	[DataType(DataType.Password)]
	[Display(Name = "Password")]
	public string Password { get; set; }

	[DataType(DataType.Password)]
	[Display(Name = "Confirm password")]
	[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
	public string ConfirmPassword { get; set; }
}
