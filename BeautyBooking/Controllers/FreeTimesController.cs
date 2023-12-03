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

		//public IActionResult Index()
		//{
		//	return View();
		//}

		public IActionResult Create(CreateFreeTimeVM createFreeTimeVM)
		{
			// TODO: надо сделать так чтобы отображалось имя фамилия мастера AND PHOTOURL
			return View(createFreeTimeVM);
		}

		public IActionResult Edit(EditFreeTimeVM editFreeTimeVM)
		{

			// TODO: editFreeTimeVM.MastersFreeTime = //get fre time-s of master with editFreeTimeVM.Id
			// TODO: also get master's photoUrl, name and surname

			return View(editFreeTimeVM);
		}
		public async Task<IActionResult> Create(int masterId, CreateFreeTimeVM createFreeTimeVM)
		{
			if (!ModelState.IsValid) return View(createFreeTimeVM);

            //Check if free time for the master already exists in db
            var time = await _serviceF.GetByMaster(masterId, createFreeTimeVM.DateAndTime);
            if (time != null)
            {
                ModelState.AddModelError("DateAndTime", "Цей час вже існує для цього майстра.");
                return View(createFreeTimeVM);
            }
            var newFreeTime = new FreeTime
            {
                DateAndTime = createFreeTimeVM.DateAndTime,
                MasterId = masterId,
            };
            try
            {
                await _serviceF.AddAsync(newFreeTime);
                return RedirectToAction("FreeTimeDetails", "Masters", new { id = masterId });
            }
            catch
            {
                ModelState.AddModelError("DateAndTime", "Помилка додавання. Спробуйте ще раз.");
                return View(createFreeTimeVM);
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
                MastersFreeTime = (IEnumerable<DateTime>)master.FreeTimes,
            };
            return View(editFreeTimeVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(EditFreeTimeVM editFreeTimeVM)
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
