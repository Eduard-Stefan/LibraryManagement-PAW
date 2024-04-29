using Library_Management.Models;
using Microsoft.AspNetCore.Identity;

namespace Library_Management.Services.Interfaces;

public interface IRegisterService
{
	Task<IdentityResult> RegisterAsync(RegisterViewModel model);
}