using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.Static;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Data.Enums;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Mvc;
using BeautyBooking.Data.Static;
namespace BeautyBooking.Controllers
{
	public class AccountController : Controller
	{
		private readonly IClientsService _serviceC;
		private readonly IMastersService _serviceM;

		public AccountController(IClientsService serviceC, IMastersService serviceM)
		{
			_serviceC = serviceC;
			_serviceM = serviceM;
		}

		public IActionResult SignIn() => View(new SignInVM());

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInVM signInVM)
		{
			if (!ModelState.IsValid) return View(signInVM);
			
			if (signInVM.Email.Equals(AdminData.Username) && signInVM.Password.Equals(AdminData.Password))
			{
				CurrentUser.User = UserRole.Admin;
				return RedirectToAction("Index", "Services");
			}
			//Check master exists
			var master = await _serviceM.GetByEmailAsync(signInVM.Email);

			//If master exists and password is correct
			if (master != null && master.Password.Equals(signInVM.Password))
			{
				CurrentUser.User = UserRole.Master;
				return RedirectToAction("Details", "Masters", new { id = master.Id });
				//return RedirectToAction("Index", "Services");
			}

			//Check user exists
			var cl = await _serviceC.GetByEmailAsync(signInVM.Email);

			//If user exists and password is correct
			if (cl != null && cl.Password.Equals(signInVM.Password))
			{
				CurrentUser.User = UserRole.Client;
				HttpContext.Session.SetInt32("userId", cl.Id);
				return RedirectToAction("Details", new { id = cl.Id });
				//return RedirectToAction("Index", "Services");
			}
			ModelState.AddModelError("Password", "Неправильна адреса ел. пошти або пароль.");
			return View(signInVM);
		}

		public IActionResult Register() => View(new RegisterVM());

		[HttpPost]
		public async Task<IActionResult> Register([Bind("ProfilePhotoURL,Surname,Name,BirthDate,Gender,PhoneNumber,Email,Password,ConfirmPassword")] RegisterVM registerVM)
		{
			//FOR TEST
			registerVM.ProfilePhotoURL = "~/img/360_F_97000908_wwH2goIihwrMoeV9QF3BW6HtpsVFaNVM.jpg";

			if (!ModelState.IsValid)
			{
				return View("Register");
			}

			//Check if user already exists in db
			var cl = await _serviceC.GetByEmailAsync(registerVM.Email);

			if (cl != null)
			{
				ModelState.AddModelError("Email", "На цю ел. пошту вже було створено обліковий запис.");
				return View(registerVM);
			}
			else
			{
				//Add new user
				var newClient = new Client
				{ 
					ProfilePhotoURL = registerVM.ProfilePhotoURL,
					Surname = registerVM.Surname,
					Name = registerVM.Name,
					BirthDate = registerVM.BirthDate,
					Gender = registerVM.Gender,
					PhoneNumber = registerVM.PhoneNumber,
					Email = registerVM.Email,
					Password = registerVM.Password
				};

				await _serviceC.AddAsync(newClient);
			}
			return RedirectToAction("SignIn");
		}

		//Get: Account/Details/1
		public async Task<IActionResult> Details(int id)
		{
			var clientDetails = await _serviceC.GetByIdAsync(id);

			if (clientDetails == null) return View("NotFound");
			return View(clientDetails);
		}

		//Get: Account/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var clientDetails = await _serviceC.GetByIdAsync(id);
			if (clientDetails == null) return View("NotFound");
			return View(new EditProfileVM {
				Id = clientDetails.Id,
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
		public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePhotoURL,Surname,Name,BirthDate,Gender,PhoneNumber,Email,OldPassword")] EditProfileVM editProfileVM)
		{
			if (!ModelState.IsValid) return View(editProfileVM);
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

			await _serviceC.UpdateAsync(id, cl);
			return RedirectToAction("Details", new {id});
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(int id, [Bind("Id,OldPassword,NewPassword")] EditProfileVM editProfileVM)
		{
			await _serviceC.UpdatePasswordAsync(id, editProfileVM.NewPassword);
			return RedirectToAction("Edit", new { id });
		}

	}
}