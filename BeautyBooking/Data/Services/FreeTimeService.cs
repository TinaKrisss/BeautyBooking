using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace BeautyBooking.Data.Services
{
    public class FreeTimeService : EntityBaseRepository<FreeTime>, IFreeTimeService
	{
		private readonly AppDbContext _context;

		public FreeTimeService(AppDbContext context) : base(context) 
		{
			_context = context;
		}
		public async Task<FreeTime> GetByMaster(int masterId, DateTime dateTime)
        {
			return await _context.Set<FreeTime>().FirstOrDefaultAsync(t => t.DateAndTime == dateTime && t.MasterId == masterId);
		}
	}
}
