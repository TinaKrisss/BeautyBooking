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
    }
}
