using Library_Management.Models;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Library_Management.Services;

public class LogoutService : ILogoutService
{
	private readonly SignInManager<User> _signInManager;

	public LogoutService(SignInManager<User> signInManager)
	{
		_signInManager = signInManager;
	}

	public async Task LogoutAsync()
	{
		await _signInManager.SignOutAsync();
	}
}