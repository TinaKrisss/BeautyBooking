using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyBooking.Data.Services
{
    public class RecordsService : EntityBaseRepository<Record>, IRecordsService
	{
		private readonly AppDbContext _context;

		public RecordsService(AppDbContext context) : base(context)
		{
			_context = context;
		}
        public async Task<List<RecordVM>> GetRecords()
        {
            var records = _context.Records
                .Include(r => r.FreeTime)
                    .ThenInclude(ft => ft.Master) 
                .Include(r => r.Client) 
                .Select(r => new RecordVM
                {
                    Id = r.Id,
                    MasterName = r.FreeTime.Master.Name, 
                    MasterSurname = r.FreeTime.Master.Surname, 
                    ClientName = r.Client.Name, 
                    ClientSurname = r.Client.Surname,
                    DateAndTime = r.FreeTime.DateAndTime, 
                    Status = r.Status
                })
                .ToList();

            return records;
        }
        public async Task<List<RecordVM>> GetMasterRecords(int masterId)
        {
            var records = _context.Records
                .Include(r => r.FreeTime)
                    .ThenInclude(ft => ft.Master)
                .Include(r => r.Client)
                .Where(r => r.FreeTime.MasterId == masterId)
                .Select(r => new RecordVM
                {
                    Id = r.Id,
                    MasterName = r.FreeTime.Master.Name,
                    MasterSurname = r.FreeTime.Master.Surname,
                    ClientName = r.Client.Name,
                    ClientSurname = r.Client.Surname,
                    DateAndTime = r.FreeTime.DateAndTime,
                    Status = r.Status
                })
                .ToList();

            return records;
        }
        public async Task<ConfirmOrderVM> GetRecordConfirmation(int id)
        {
            var confirmOrderVM = _context.Records
            .Include(record => record.FreeTime.Master)
            .Include(record => record.GroupOfServices) 
            .ThenInclude(group => group.Service).Where(record => record.Id == id)
            .Select(record => new ConfirmOrderVM
            {
                MasterName = record.FreeTime.Master.Name,
                MasterSurname = record.FreeTime.Master.Surname,
                Services = record.GroupOfServices.Select(group => group.Service).ToList(),
                Time = record.GroupOfServices.Sum(group => group.Service.Duration).ToString(),
                Price = record.GroupOfServices.Sum(group => group.Service.Price)
            })
            .ToList();
            return confirmOrderVM[0];
        }
        public async Task<EditRecordVM> GetRecordInformation(int id)
        {
            var editRecordVM = _context.Records
            .Include(record => record.FreeTime.Master)
            .Include(record => record.Client)
            .Include(record => record.GroupOfServices)
            .ThenInclude(group => group.Service).Where(record => record.Id == id)
            .Select(record => new EditRecordVM
            {
				Id = record.Id,
				FreeTimeId = record.FreeTime.Id,
                ClientId = record.ClientId,
                MasterId = record.FreeTime.MasterId,
                ClientName = record.Client.Name,
                ClientSurname = record.Client.Surname,
                MasterName = record.FreeTime.Master.Name,
                MasterSurname = record.FreeTime.Master.Surname,
                Services = record.GroupOfServices.Select(group => group.Service).ToList(),
                DateAndTime = record.FreeTime.DateAndTime,
                Feedback = record.Feedback,
                Status = record.Status
            })
            .ToList();
            return editRecordVM[0];
        }
        public async Task<List<RecordVM>> GetClientHistory(int clientId)
        {
            var records = _context.Records
                .Include(r => r.FreeTime)
                    .ThenInclude(ft => ft.Master)
                .Include(r => r.Client)
                .Where(r => r.ClientId == clientId && r.Status == Enums.Status.Done)
                .Select(r => new RecordVM
                {
                    Id = r.Id,
                    MasterName = r.FreeTime.Master.Name,
                    MasterSurname = r.FreeTime.Master.Surname,
                    ClientName = r.Client.Name,
                    ClientSurname = r.Client.Surname,
                    DateAndTime = r.FreeTime.DateAndTime,
                    Status = r.Status
                })
                .ToList();

            return records;
        }
    }
}
