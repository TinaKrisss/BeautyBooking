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
		private readonly IRecordsService _serviceR;
		private readonly IGroupsOfServicesService _serviceG;
		private readonly IFreeTimeService _serviceF;

		public RecordsController(IRecordsService serviceR, IGroupsOfServicesService serviceG, IFreeTimeService serviceF)
        {
            _serviceR = serviceR;
            _serviceG = serviceG;
            _serviceF = serviceF;
        }

        public async Task<IActionResult> Index()
		{
			var recordsVMs = await _serviceR.GetRecords();
			return View(recordsVMs);
		}
		public async Task<IActionResult> Confirmation(int recordId)
		{
			if (recordId != -1)
            {
				ConfirmOrderVM confirmOrderVM = await _serviceR.GetRecordConfirmation(recordId);
				int hours = Convert.ToInt32(confirmOrderVM.Time) / 60;
				int minutes = Convert.ToInt32(confirmOrderVM.Time) % 60;
				confirmOrderVM.Time = $"{hours:D2}:{minutes:D2}";
				return View(confirmOrderVM);
			}
			return View("NotFound");

		}
		[HttpPost]
		public async Task<int> Confirmation(string cartData, string freeTimeIdString, string masterIdString)
		{
			int[] serviceIds = JsonConvert.DeserializeObject<string[]>(cartData).Select(int.Parse).ToArray();
			int freeTimeId = Convert.ToInt32(freeTimeIdString);
			int masterId = Convert.ToInt32(masterIdString);
			int? clientId = HttpContext.Session.GetInt32("userId");
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
				return record.Id;
			}
			catch
			{
				return -1;
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int recordId)
		{
			try
			{
				await _serviceR.DeleteAsync(recordId);
				return RedirectToAction("Index");
			}
			catch
			{
				return RedirectToAction("Edit", recordId);
			}
		}

		public async Task<IActionResult> Edit(int recordId)
		{
			var editRecordVM = await _serviceR.GetRecordInformation(recordId);
			return View(editRecordVM);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int recordId, EditRecordVM editRecordVM)
		{
			var time = await _serviceF.GetByMaster(editRecordVM.MasterId, editRecordVM.DateAndTime);
			if (time != null)
			{
				return RedirectToAction("Edit", recordId);
			}
			var record = await _serviceR.GetByIdAsync(recordId);
			if (record == null)
			{
				return RedirectToAction("Edit", recordId);
			}
			try
			{
				var freeTime = await _serviceF.GetByIdAsync(editRecordVM.FreeTimeId);
				freeTime.DateAndTime = editRecordVM.DateAndTime;
				record.Status = editRecordVM.Status;
				await _serviceF.UpdateAsync(editRecordVM.FreeTimeId, freeTime);
				return RedirectToAction("Index");
			}
			catch
			{
				return RedirectToAction("Edit", recordId);
			}
		}
	}
}
