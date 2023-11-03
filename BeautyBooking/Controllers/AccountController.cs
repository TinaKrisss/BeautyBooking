using BeautyBooking.Data;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.Static;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.AspNet.Identity;
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

		public IActionResult SignIn() => View(new SignInVM());

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInVM signInVM)
		{
			if (!ModelState.IsValid) return View(signInVM);

			//Check user exists
			var cl = await _service.GetByEmailAsync(signInVM.Email);

			//If user exists and password is correct
			if (cl != null && cl.Password.Equals(signInVM.Password))
			{
				HttpContext.Session.SetInt32("userId", cl.Id);
				return RedirectToAction("Index", "Services");
			}
			TempData["Error"] = "Неправильна адреса ел. пошти або пароль.";
			return View(signInVM);
		}

		public IActionResult Register() => View(new Client());

		[HttpPost]
		public async Task<IActionResult> Register([Bind("ProfilePhotoURL,Surname,Name,BirthDate,PhoneNumber,Email,Password")] Client client)
		{
			client.ProfilePhotoURL = "test";
			client.Gender = Data.Enums.Gender.Female;

			//Check if user already exists in db
			var cl = await _service.GetByEmailAsync(client.Email);

			if (cl != null)
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
	}
}