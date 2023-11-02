using BeautyBooking.Data.Base;
using BeautyBooking.Models;

namespace BeautyBooking.Data.Interfaces
{
    public interface IClientsService : IEntityBaseRepository<Client>
	{
        Task<Client> GetByEmail(string email);
    }
}
