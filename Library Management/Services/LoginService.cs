using Library_Management.Models;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Library_Management.Services
{
	public class LoginService : ILoginService
	{
		private readonly SignInManager<User> _signInManager;

		public LoginService(SignInManager<User> signInManager)
		{
			_signInManager = signInManager;
		}

		public async Task<SignInResult> SignInAsync(string email, string password, bool rememberMe)
		{
			return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
		}
	}
}
