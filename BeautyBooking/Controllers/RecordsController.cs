using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BeautyBooking.Controllers
{
	public class RecordsController : Controller
	{
		private readonly IServicesService _serviceS;
		private readonly IRecordsService _serviceR;
		private readonly IGroupsOfServicesService _serviceG;
		private readonly IClientsService _serviceC;
		private readonly IMastersService _serviceM;

		public RecordsController(IServicesService serviceS, IRecordsService serviceR, IGroupsOfServicesService serviceG, IClientsService serviceC, IMastersService serviceM)
        {
            _serviceS = serviceS;
            _serviceR = serviceR;
            _serviceG = serviceG;
            _serviceC = serviceC;
            _serviceM = serviceM;
        }

        public async Task<IActionResult> Index()
		{
			var recordsVMs = await _serviceR.GetRecords();
			return View(recordsVMs);
		}

		//Get: Services/Create
		public IActionResult Create()
		{
			return View();
		}

	}
}
