using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace BeautyBooking.Data.Services
{
    public class ClientsService : EntityBaseRepository<Client>, IClientsService
	{
		private readonly AppDbContext _context;

		public ClientsService(AppDbContext context) : base(context) 
		{
			_context = context;
		}

		public async Task<Client> GetByEmailAsync(string email)
		{
			return await _context.Set<Client>().FirstOrDefaultAsync(c => c.Email == email);
		}

		public async Task UpdatePasswordAsync(int id, string newPassword)
		{
			var cl = await _context.Set<Client>().FirstOrDefaultAsync(c => c.Id == id);		
			cl.Password = newPassword;

			await _context.SaveChangesAsync();
		}
		
	}
}
