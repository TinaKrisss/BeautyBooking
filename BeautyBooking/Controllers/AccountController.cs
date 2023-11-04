using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Mvc;

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
				return RedirectToAction("Details", new { id = 1 });
				//return RedirectToAction("Index", "Services");
			}
			TempData["Error"] = "Неправильна адреса ел. пошти або пароль.";
			return View(signInVM);
		}

		public IActionResult Register() => View(new Client());

		[HttpPost]
		public async Task<IActionResult> Register([Bind("ProfilePhotoURL,Surname,Name,BirthDate,Gender,PhoneNumber,Email,Password")] Client client)
		{
			//FOR TEST
			client.ProfilePhotoURL = "test";

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

		//Get: Account/Details/1
		public async Task<IActionResult> Details(int id)
		{
			var clientDetails = await _service.GetByIdAsync(id);

			if (clientDetails == null) return View("NotFound");
			return View(clientDetails);
		}

		//Get: Account/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var clientDetails = await _service.GetByIdAsync(id);
			if (clientDetails == null) return View("NotFound");
			return View(new EditProfileVM { 
				ProfilePhotoURL = clientDetails.ProfilePhotoURL,
				Surname = clientDetails.Surname,
				Name = clientDetails.Name,
				BirthDate = clientDetails.BirthDate,
				Gender = clientDetails.Gender,
				PhoneNumber = clientDetails.PhoneNumber,
				Email = clientDetails.Email,
				OldPassword = clientDetails.Password
			});
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("ProfilePhotoURL,Surname,Name,BirthDate,PhoneNumber,Email")] EditProfileVM editProfileVM)
		{
			//Gender

			var cl = new Client
			{
				Id = id,
				ProfilePhotoURL = editProfileVM.ProfilePhotoURL,
				Name = editProfileVM.Name,
				Surname = editProfileVM.Surname,
				BirthDate = editProfileVM.BirthDate,
				Gender = editProfileVM.Gender,
				PhoneNumber = editProfileVM.PhoneNumber,
				Email = editProfileVM.Email,
				Password = editProfileVM.OldPassword
			};

			await _service.UpdateAsync(id, cl);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(int id, [Bind("OldPassword,NewPassword")] EditProfileVM editProfileVM)
		{
			await _service.UpdatePasswordAsync(id, editProfileVM.NewPassword);
			return View("Edit");
		}
	}
}