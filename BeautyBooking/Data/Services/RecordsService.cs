using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
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
    }
}
