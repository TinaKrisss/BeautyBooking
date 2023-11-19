using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BeautyBooking.Controllers
{
	public class ServicesController : Controller
	{
		private readonly IServicesService _service;

		public ServicesController(IServicesService service)
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
			var serviceDetails = await _service.GetByIdAsync(id);

			if (serviceDetails == null) return View("NotFound");
			return View(serviceDetails);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var serviceDetails = await _service.GetByIdAsync(id);
			if (serviceDetails == null) return View("NotFound");
			return View(serviceDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var serviceDetails = await _service.GetByIdAsync(id);
			if (serviceDetails == null) return View("NotFound");

			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Create(ServiceCreateVM serviceCreateVM)
		{
			if (!ModelState.IsValid)
			{
				//RETURN SERVICE CREATE VIEW
				//return View("Register");
			}
			//Check if service already exists in db
			var serv = await _service.GetByNameAsync(serviceCreateVM.Name);

			if (serv != null)
			{
				ModelState.AddModelError("Name", "Така послуга вже існує.");
				return View(serviceCreateVM);
			}
			var newService = new Service
			{
				Name = serviceCreateVM.Name,
				Description = serviceCreateVM.Description,
				Duration = serviceCreateVM.Duration,
				Price = serviceCreateVM.Price,
			};
            try
            {
				await _service.AddAsync(newService);
				return RedirectToAction("Index");
			}
			catch
            {
				ModelState.AddModelError("Name", "Помилка додавання. Спробуйте ще раз.");
				return View(serviceCreateVM);
			}
		}
	}
}
