using BeautyBooking.Data;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.Static;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyBooking.Controllers
{
	public class AccountController : Controller
	{
		private readonly IClientsService _service;

		public AccountController(IClientsService service)
		{
			_service = service;
		}

		//public async Task<IActionResult> Users()
		//{
		//	var users = await _context.Clients.ToListAsync();
		//	return View(users);
		//}


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


		public IActionResult Register() => View(new Client());

		[HttpPost]
		public async Task<IActionResult> Register(Client client)
		{
			if (!ModelState.IsValid) return View(client);

			//Check if user already exists in db
			if (_service.GetByEmail(client.Email) != null)
			{
				TempData["Error"] = "На цю ел. пошту вже було створено обліковий запис!";
				return View(client);
			}
			else
			{
				//Add new user
				await _service.AddAsync(client);
			}
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