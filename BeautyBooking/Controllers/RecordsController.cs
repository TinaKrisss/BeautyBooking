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
		public async Task<IActionResult> Edit(int recordId)
		{
			var editRecordVM = await _serviceR.GetRecordInformation(recordId);
			return View(editRecordVM);
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

		[HttpPost]
		public async Task<IActionResult> Confirm(int recordId, EditRecordVM editRecordVM)
		{
			var record = await _serviceR.GetByIdAsync(recordId);
			if (record == null)
            {
				return RedirectToAction("Edit", recordId);
            }
            try
            {
				record.Status = editRecordVM.Status;
				await _serviceR.UpdateAsync(recordId, record);
			}
            catch
            {
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int recordId)
		{
			try
			{
				await _serviceR.DeleteAsync(recordId);
			}
			catch
			{

			}
			return RedirectToAction("Index");
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
				await _serviceF.UpdateAsync(editRecordVM.FreeTimeId, freeTime);
			}
			catch
			{
			}
			return RedirectToAction("Index");
		}
	}
}
