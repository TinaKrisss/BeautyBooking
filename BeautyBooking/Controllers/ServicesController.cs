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

		//Get: Services/Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateServiceVM serviceCreateVM)
		{
			if (!ModelState.IsValid) return View(serviceCreateVM);
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

		public async Task<IActionResult> Details(int id)
		{
			var serviceDetails = await _service.GetByIdAsync(id);

			if (serviceDetails == null) return View("NotFound");
			return View(serviceDetails);
		}

		//Get: Service/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var serviceDetails = await _service.GetByIdAsync(id);
			if (serviceDetails == null) return View("NotFound");
			return View(new EditServiceVM
			{
				Id = serviceDetails.Id,
				Name = serviceDetails.Name,
				Description = serviceDetails.Description,
				Duration = serviceDetails.Duration,
				Price = serviceDetails.Price,
			});
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Duration,Price")] EditServiceVM editServiceVM)
		{
			if (!ModelState.IsValid) return View(editServiceVM);
			var serv = new Service
			{
				Id = editServiceVM.Id,
				Name = editServiceVM.Name,
				Description = editServiceVM.Description,
				Duration = editServiceVM.Duration,
				Price = editServiceVM.Price,
			};
			try
			{
				await _service.UpdateAsync(id, serv);
				return RedirectToAction("Details", new { id });
			}
            catch(Exception ex)
            {
				ModelState.AddModelError("Name", "Помилка редагування. Спробуйте ще раз.");
				return View(editServiceVM);
			}
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
	}
}
