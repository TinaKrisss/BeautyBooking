using BeautyBooking.Data.Enums;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BeautyBooking.Controllers
{
	public class RecordsController : Controller
	{
		private readonly IServicesService _serviceS;
		private readonly IRecordsService _serviceR;
		private readonly IGroupsOfServicesService _serviceG;
		private readonly IClientsService _serviceC;
		private readonly IMastersService _serviceM;
		private readonly IFreeTimeService _serviceF;

		public RecordsController(IServicesService serviceS, IRecordsService serviceR, IGroupsOfServicesService serviceG, IClientsService serviceC, IMastersService serviceM, IFreeTimeService serviceF)
        {
            _serviceS = serviceS;
            _serviceR = serviceR;
            _serviceG = serviceG;
            _serviceC = serviceC;
            _serviceM = serviceM;
			_serviceF = serviceF;
        }

        public async Task<IActionResult> Index()
		{
			var recordsVMs = await _serviceR.GetRecords();
			return View(recordsVMs);
		}
        [HttpPost]
		public async Task<IActionResult> Confirmation(string cartData, string totalDuration, string totalPrice, string freeTimeIdString, string masterIdString)
		{
            int[] serviceIds = JsonConvert.DeserializeObject<string[]>(cartData).Select(int.Parse).ToArray();
			int freeTimeId = Convert.ToInt32(freeTimeIdString);
			int masterId = Convert.ToInt32(masterIdString);
			//int? clientId = HttpContext.Session.GetInt32("UserId");
			int? clientId = 1;
            try
            {
				var record = new Record
				{
					Status = Status.NotConfirmed,
					FreeTimeId = freeTimeId,
					ClientId = Convert.ToInt32(clientId)
				};
				await _serviceR.AddAsync(record);
				foreach (int serviceId in serviceIds)
                {
					var group = new GroupOfServices
					{
						RecordId = record.Id,
						ServiceId = serviceId,
					};
					await _serviceG.AddAsync(group);
                }
				ConfirmOrderVM confirmOrderVM = await _serviceR.GetRecordConfirmation(record.Id);
				confirmOrderVM.Price = Convert.ToInt32(totalPrice);
				confirmOrderVM.Time = totalDuration;
				return View(confirmOrderVM);
			}
            catch
            {
				return View("NotFound");
			}
		}
	}
}
