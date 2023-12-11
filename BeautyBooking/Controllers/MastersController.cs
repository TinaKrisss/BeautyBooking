﻿using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;


namespace BeautyBooking.Controllers
{
    public class MastersController : Controller
    {
        private readonly IMastersService _serviceM;
		private readonly IRecordsService _serviceR;
		private readonly IPasswordCreator _creator;

        public MastersController(IMastersService serviceM, IPasswordCreator creator, IRecordsService serviceR)
        {
            _serviceM = serviceM;
            _creator = creator;
            _serviceR = serviceR;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _serviceM.GetAllAsync();
            return View(data);
        }
		public async Task<IActionResult> Records(int masterId)
		{
			var recordsVM = await _serviceR.GetMasterRecords(masterId);
			return View(recordsVM);
		}
		public async Task<IActionResult> Details(int id)
        {
            var masterDetails = await _serviceM.GetByIdAsync(id);

            if (masterDetails == null) return View("NotFound");
            return View(masterDetails);
        }
		public async Task<IActionResult> FreeTimeDetails(int id)
		{
			var master = await _serviceM.GetWithTime(id);

			if (master == null) return View("NotFound");
			return View(master);
		}
		public async Task<IActionResult> Delete(int id)
		{
			var masterDetails = await _serviceM.GetByIdAsync(id);
			if (masterDetails == null) return View("NotFound");
			return View(masterDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var masterDetails = await _serviceM.GetByIdAsync(id);
			if (masterDetails == null) return View("NotFound");

			await _serviceM.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		//Get: Masters/Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateMasterVM masterCreateVM)
		{
			if (!ModelState.IsValid) return View(masterCreateVM);

			//Check if master already exists in db
			var mast = _serviceM.GetByEmailAsync(masterCreateVM.Email);
			if (await mast != null)
			{
				ModelState.AddModelError("Email", "На цю ел. пошту вже було створено майстра.");
				return View(masterCreateVM);
			}
			var newMaster = new Master
			{
				ProfilePhotoURL = "~/img/360_F_97000908_wwH2goIihwrMoeV9QF3BW6HtpsVFaNVM.jpg",
				Name = masterCreateVM.Name,
				Surname = masterCreateVM.Surname,
				Info = masterCreateVM.Info,
				Email = masterCreateVM.Email,
				Password = await _creator.CreatePassword(),
			};
			try
			{
				await _serviceM.AddAsync(newMaster);
				return RedirectToAction("Index");
			}
			catch
			{
				ModelState.AddModelError("Name", "Помилка додавання. Спробуйте ще раз.");
				return View(masterCreateVM);
			}
		}

		public async Task<IActionResult> Edit(int id)
		{
			var masterDetails = await _serviceM.GetByIdAsync(id);
			if (masterDetails == null) return View("NotFound");
			return View(new EditMasterVM
			{
				Id = masterDetails.Id,
				ProfilePhotoURL = masterDetails.ProfilePhotoURL,
				Surname = masterDetails.Surname,
				Name = masterDetails.Name,
				Info = masterDetails.Info,
				Email =masterDetails.Email,
				OldPassword = masterDetails.Password
			});
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePhotoURL,Surname,Name,BirthDate,Gender,PhoneNumber,Email,OldPassword")] EditMasterVM editMasterVM)
		{
			if (!ModelState.IsValid) return View(editMasterVM);
			var master = new Master
			{
				Id = id,
				ProfilePhotoURL = editMasterVM.ProfilePhotoURL,
				Name = editMasterVM.Name,
				Surname = editMasterVM.Surname,
				Info = editMasterVM.Info,
				Email = editMasterVM.Email,
				Password = editMasterVM.OldPassword
			};

			await _serviceM.UpdateAsync(id, master);
			return RedirectToAction("Details", new { id });
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(int id, [Bind("Id,OldPassword,NewPassword")] EditMasterVM editMasterVM)
		{
			await _serviceM.UpdatePasswordAsync(id, editMasterVM.NewPassword);
			return RedirectToAction("Edit", new { id });
		}
	}
}
