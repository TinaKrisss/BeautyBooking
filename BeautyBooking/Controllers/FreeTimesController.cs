using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeautyBooking.Controllers
{
    public class FreeTimesController : Controller
    {
		private readonly IFreeTimeService _serviceF;
        private readonly IMastersService _serviceM;

        public FreeTimesController(IFreeTimeService serviceF, IMastersService serviceM)
		{
            _serviceF = serviceF;
            _serviceM = serviceM;
        }

		public IActionResult Create(CreateFreeTimeVM createFreeTimeVM)
		{
			// TODO: надо сделать так чтобы отображалось имя фамилия мастера AND PHOTOURL
			return View(createFreeTimeVM);
		}
		public async Task<IActionResult> CreateConfirmed([Bind("DateAndTime,Id")] CreateFreeTimeVM createFreeTimeVM)
		{
			if (!ModelState.IsValid) return View("Create", createFreeTimeVM);

            //Check if free time for the master already exists in db
            var time = await _serviceF.GetByMaster(createFreeTimeVM.Id, createFreeTimeVM.DateAndTime);
            if (time != null)
            {
                ModelState.AddModelError("DateAndTime", "Цей час вже існує для цього майстра.");
                return View("Create", createFreeTimeVM);
            }
            var newFreeTime = new FreeTime
            {
                DateAndTime = createFreeTimeVM.DateAndTime,
                MasterId = createFreeTimeVM.Id,
            };
            try
            {
                await _serviceF.AddAsync(newFreeTime);
                return RedirectToAction("FreeTimeDetails", "Masters", new { id = createFreeTimeVM.Id });
            }
            catch
            {
                ModelState.AddModelError("DateAndTime", "Помилка додавання. Спробуйте ще раз.");
                return View("Create", createFreeTimeVM);
            }
        }
        public async Task<IActionResult> Edit(int masterId)
        {
            var master = await _serviceM.GetWithTime(masterId);
            var editFreeTimeVM = new EditFreeTimeVM
            {
                Id = masterId,
                Name = master.Name,
                Surname = master.Surname,
                ProfilePhotoURL = master.ProfilePhotoURL,
                MastersFreeTime = master.FreeTimes,
            };
            return View(editFreeTimeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("FreeTimeId,Id,ProfilePhotoURL,Surname,Name,MastersFreeTime,NewDateAndTime")] EditFreeTimeVM editFreeTimeVM)
        {
            if (!ModelState.IsValid || editFreeTimeVM.FreeTimeId == null) return View("Edit", editFreeTimeVM);
            //Check if free time for the master already exists in db
            var time = await _serviceF.GetByMaster(editFreeTimeVM.Id, editFreeTimeVM.NewDateAndTime);
            if (time != null)
            {
                ModelState.AddModelError("DateAndTime", "Цей час вже існує для цього майстра.");
                return View(editFreeTimeVM);
            }
            time = new FreeTime
            {
                MasterId = editFreeTimeVM.Id,
                DateAndTime = editFreeTimeVM.NewDateAndTime,
                Id = Convert.ToInt32(editFreeTimeVM.FreeTimeId)
            };
            try
            {
                await _serviceF.UpdateAsync(time.Id, time);
                return RedirectToAction("FreeTimeDetails", "Masters", new { id = editFreeTimeVM.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("NewDateAndTime", "Помилка редагування. Спробуйте ще раз.");
                return View(editFreeTimeVM);
            }
        }
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete([Bind("FreeTimeId,Id,ProfilePhotoURL,Surname,Name,MastersFreeTime")] EditFreeTimeVM editFreeTimeVM)
        {
            if (!ModelState.IsValid || editFreeTimeVM.FreeTimeId == null) return View("Edit", editFreeTimeVM);
            try
            {
                await _serviceF.DeleteAsync(Convert.ToInt32(editFreeTimeVM.FreeTimeId));
                return RedirectToAction("FreeTimeDetails", "Masters", new { id = editFreeTimeVM.Id });
            }
            catch
            {
                ModelState.AddModelError("DateAndTime", "Помилка видалення. Спробуйте ще раз.");
                return View("Edit", editFreeTimeVM);
            }
        }
    }
}
