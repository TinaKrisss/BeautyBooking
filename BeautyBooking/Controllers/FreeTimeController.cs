using Microsoft.AspNetCore.Mvc;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;

namespace BeautyBooking.Controllers
{
    public class FreeTimeController : Controller
    {
        private readonly IFreeTimeService _service;

        public FreeTimeController(IFreeTimeService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int masterId)
        {
            // надо во вью кидать айди?
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int masterId, CreateFreeTimeVM freeTimeCreateVM)
        {
			if (!ModelState.IsValid) return View(freeTimeCreateVM);

			//Check if free time for the master already exists in db
			var time = await _service.GetByMaster(masterId, freeTimeCreateVM.DateAndTime);
			if (time != null)
			{
				ModelState.AddModelError("DateAndTime", "Цей час вже існує для цього майстра.");
				return View(freeTimeCreateVM);
			}
			var newFreeTime = new FreeTime
			{
				DateAndTime = freeTimeCreateVM.DateAndTime,
				MasterId = masterId,
			};
			try
			{
				await _service.AddAsync(newFreeTime);
				return RedirectToAction("FreeTimeDetails", "Masters", new {id = masterId});
			}
			catch
			{
				ModelState.AddModelError("DateAndTime", "Помилка додавання. Спробуйте ще раз.");
				return View(freeTimeCreateVM);
			}
		}
    }
}
