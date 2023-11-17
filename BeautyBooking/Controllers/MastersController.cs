using BeautyBooking.Data.Interfaces;
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
	}
}
