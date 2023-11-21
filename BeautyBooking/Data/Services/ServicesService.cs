using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyBooking.Data.Services
{
    public class ServicesService : EntityBaseRepository<Service>, IServicesService
	{
		private readonly AppDbContext _context;

		public ServicesService(AppDbContext context) : base(context)
		{
			_context = context;
		}
		public async Task<Service> GetByNameAsync(string name)
		{
			return await _context.Set<Service>().FirstOrDefaultAsync(s => s.Name == name);
		}
	}
}
