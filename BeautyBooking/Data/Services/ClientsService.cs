using BeautyBooking.Data.Base;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Data.Entity;

namespace BeautyBooking.Data.Services
{
    public class ClientsService : EntityBaseRepository<Client>, IClientsService
	{
		private readonly AppDbContext _context;

		public ClientsService(AppDbContext context) : base(context) 
		{
			_context = context;
		}

		public async Task<Client> GetByEmail(string email)
		{
			IQueryable<Client> query = _context.Set<Client>();
			return await query.FirstOrDefaultAsync(c => c.Email.Equals(email));
		}
	}
}
