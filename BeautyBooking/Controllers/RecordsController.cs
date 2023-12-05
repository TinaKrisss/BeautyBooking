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

		public RecordsController(IServicesService serviceS, IRecordsService serviceR, IGroupsOfServicesService serviceG)
		{
			_serviceS = serviceS;
			_serviceR = serviceR;
			_serviceG = serviceG;
		}

		public async Task<IActionResult> Index()
		{
			return View();
		}

		//Get: Services/Create
		public IActionResult Create()
		{
			return View();
		}

	}
}
