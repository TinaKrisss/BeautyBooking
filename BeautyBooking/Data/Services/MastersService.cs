using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Models;

namespace BeautyBooking.Data.Services
{
    public class MastersService : EntityBaseRepository<Master>, IMastersService
	{
		private readonly AppDbContext _context;

		public MastersService(AppDbContext context) : base(context)
		{
			_context = context;
		}
    }
}
