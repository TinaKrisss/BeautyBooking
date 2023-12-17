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
        private readonly IClientsService _serviceC;
        private readonly IRecordsService _serviceR;
        public ClientsController(IClientsService serviceC, IRecordsService serviceR)
        {
            _serviceC = serviceC;
            _serviceR = serviceR;
        }
        public async Task<IActionResult> Index()
        {
            var clients = await _serviceC.GetAllAsync();
            if (clients == null) return View("NotFound");
            return View(clients);
        }
        public async Task<IActionResult> Details(int clientId)
        {
            var clientDetails = await _serviceC.GetByIdAsync(clientId);

            if (clientDetails == null) return View("NotFound");
            return View(clientDetails);
        }
        public async Task<IActionResult> Delete(int clientId)
        {
            var client = await _serviceC.GetByIdAsync(clientId);
            if (client == null) return View("NotFound");

            await _serviceC.DeleteAsync(clientId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> History(int id)
        {
            var history = await _serviceR.GetClientHistory(id);
            if (history == null) return View("NotFound");
            return View(history);
        }
    }
}
