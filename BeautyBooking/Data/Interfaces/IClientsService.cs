using BeautyBooking.Data.Base;
using BeautyBooking.Models;

namespace BeautyBooking.Data.Interfaces
{
    public interface IClientsService : IEntityBaseRepository<Client>
	{
        Task<Client> GetByEmailAsync(string email);
		Task UpdatePasswordAsync(int id, string newPassword);
	}
}
