using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;


namespace BeautyBooking.Controllers
{
    public class MastersController : Controller
    {
        private readonly IMastersService _service;

        public MastersController(IMastersService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
        public async Task<IActionResult> Details(int id)
        {
            var masterDetails = await _service.GetByIdAsync(id);

            if (masterDetails == null) return View("NotFound");
            return View(masterDetails);
        }

		public async Task<IActionResult> Delete(int id)
		{
			var masterDetails = await _service.GetByIdAsync(id);
			if (masterDetails == null) return View("NotFound");
			return View(masterDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var masterDetails = await _service.GetByIdAsync(id);
			if (masterDetails == null) return View("NotFound");

			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Create(MasterCreateVM masterCreateVM)
		{
			if (!ModelState.IsValid)
			{
				//RETURN MASTER CREATE VIEW
				//return View("Register");
			}
			//Check if master already exists in db
			var mast = _service.GetByEmailAsync(masterCreateVM.Email);
			if (await mast != null)
			{
				ModelState.AddModelError("Email", "На цю ел. пошту вже було створено майстра.");
				return View(masterCreateVM);
			}
			var newMaster = new Master
			{
				ProfilePhotoURL = masterCreateVM.ProfilePhotoURL,
				Name = masterCreateVM.Name,
				Surname = masterCreateVM.Surname,
				Info = masterCreateVM.Info,
				Email = masterCreateVM.Email,
				Password = "",
			};
			try
			{
				await _service.AddAsync(newMaster);
				return RedirectToAction("Index");
			}
			catch
			{
				ModelState.AddModelError("Name", "Помилка додавання. Спробуйте ще раз.");
				return View(masterCreateVM);
			}
		}
	}
}
