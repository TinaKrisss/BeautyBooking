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
		public async Task<Master> GetWithTime(int id)
		{
			return await _context.Set<Master>().Include(m => m.FreeTimes.Where(ft => ft.Record == null).OrderBy(ft => ft.DateAndTime))
			.FirstOrDefaultAsync(c => c.Id == id); 
		}
		public async Task UpdatePasswordAsync(int id, string newPassword)
		{
			var master = await _context.Set<Master>().FirstOrDefaultAsync(m => m.Id == id);
			master.Password = newPassword;

			await _context.SaveChangesAsync();
		}
	}
}
