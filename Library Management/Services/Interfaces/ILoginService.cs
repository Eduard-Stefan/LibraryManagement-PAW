using Microsoft.AspNetCore.Identity;

namespace Library_Management.Services.Interfaces;

public interface ILoginService
{
	Task<SignInResult> SignInAsync(string email, string password, bool rememberMe);
}