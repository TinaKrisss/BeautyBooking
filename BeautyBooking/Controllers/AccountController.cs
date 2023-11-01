using BeautyBooking.Data;
using BeautyBooking.Data.Static;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyBooking.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly AppDbContext _context;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
		}


		public async Task<IActionResult> Users()
		{
			var users = await _context.Clients.ToListAsync();
			return View(users);
		}


		//public IActionResult Login() => View(new LoginVM());

		//[HttpPost]
		//public async Task<IActionResult> Login(LoginVM loginVM)
		//{
		//	if (!ModelState.IsValid) return View(loginVM);

		//	var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
		//	if (user != null)
		//	{
		//		var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
		//		if (passwordCheck)
		//		{
		//			var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
		//			if (result.Succeeded)
		//			{
		//				return RedirectToAction("Index", "Movies");
		//			}
		//		}
		//		TempData["Error"] = "Wrong credentials. Please, try again!";
		//		return View(loginVM);
		//	}

		//	TempData["Error"] = "Wrong credentials. Please, try again!";
		//	return View(loginVM);
		//}


		public IActionResult Register() => View(new RegisterVM());

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM registerVM)
		{
			if (!ModelState.IsValid) return View(registerVM);

			var user = await _userManager.FindByEmailAsync(registerVM.Email);
			if (user != null)
			{
				TempData["Error"] = "На цю ел. пошту вже було створено обліковий запис!";
				return View(registerVM);
			}

			var newUser = new ApplicationUser()
			{
				Surname = registerVM.Surname,
				Name = registerVM.Name,
				BirthDate = registerVM.BirthDate,
				Gender = registerVM.Gender,
				PhoneNumber = registerVM.PhoneNumber,
				Email = registerVM.Email,
				Password = registerVM.Password
			};

			var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

			if (newUserResponse.Succeeded)
				await _userManager.AddToRoleAsync(newUser, UserRoles.Client);

			return View("RegisterCompleted");
		}

		//[HttpPost]
		//public async Task<IActionResult> Logout()
		//{
		//	await _signInManager.SignOutAsync();
		//	return RedirectToAction("Index", "Movies");
		//}

		//public IActionResult AccessDenied(string ReturnUrl)
		//{
		//	return View();
		//}

	}
}