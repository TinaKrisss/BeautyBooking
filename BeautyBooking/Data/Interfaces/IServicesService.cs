using BeautyBooking.Data.Base;
using BeautyBooking.Models;

namespace BeautyBooking.Data.Interfaces
{
    public interface IServicesService : IEntityBaseRepository<Service>
	{
        Task<Service> GetByNameAsync(string name);
    }
}
