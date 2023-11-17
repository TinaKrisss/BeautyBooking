using BeautyBooking.Data.Interfaces;
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
	}
}
