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
    }
}
