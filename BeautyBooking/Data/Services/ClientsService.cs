using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Models;
using Microsoft.EntityFrameworkCore;


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
	}
}
