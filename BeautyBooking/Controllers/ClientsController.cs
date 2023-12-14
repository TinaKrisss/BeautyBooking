using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.Static;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Data.Enums;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeautyBooking.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientsService _service;

        public ClientsController(IClientsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var clients = await _service.GetAllAsync();
            if (clients == null) return View("NotFound");
            return View(clients);
        }
        public async Task<IActionResult> Details(int id)
        {
            var clientDetails = await _service.GetByIdAsync(id);

            if (clientDetails == null) return View("NotFound");
            return View(clientDetails);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _service.GetByIdAsync(id);
            if (client == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
