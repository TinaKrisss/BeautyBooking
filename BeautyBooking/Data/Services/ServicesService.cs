using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Models;

namespace BeautyBooking.Data.Services
{
    public class ServicesService : EntityBaseRepository<Service>, IServicesService
	{
		private readonly AppDbContext _context;

		public ServicesService(AppDbContext context) : base(context)
		{
			_context = context;
		}
    }
}
