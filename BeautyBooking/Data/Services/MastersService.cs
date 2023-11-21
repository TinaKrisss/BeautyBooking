using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautyBooking.Data.Services
{
    public class MastersService : EntityBaseRepository<Master>, IMastersService
	{
		private readonly AppDbContext _context;

		public MastersService(AppDbContext context) : base(context)
		{
			_context = context;
		}
		public async Task<Master> GetByEmailAsync(string email)
		{
			return await _context.Set<Master>().FirstOrDefaultAsync(c => c.Email == email);
		}
	}
}
