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
                ClientName = record.Client.Name,
                ClientSurname = record.Client.Surname,
                MasterName = record.FreeTime.Master.Name,
                MasterSurname = record.FreeTime.Master.Surname,
                Services = record.GroupOfServices.Select(group => group.Service).ToList(),
                FreeTime = record.FreeTime,
                Status = record.Status
            })
            .ToList();
            return editRecordVM[0];
        }
    }
}
