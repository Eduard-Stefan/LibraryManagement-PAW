using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Library_Management.Models;
using Library_Management.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Library_Management.Services;

public class RegisterService : IRegisterService
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public RegisterService(UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
	{
		var user = new User
		{
			Name = model.Name,
			UserName = model.Email,
			Email = model.Email,
			PhoneNumber = model.PhoneNumber,
			Address = model.Address,
			ImageFileName = model.ImageFileName
		};

		var result = await _userManager.CreateAsync(user, model.Password);
		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, "client");
			await _signInManager.SignInAsync(user, isPersistent: false);
		}

		return result;
	}
}