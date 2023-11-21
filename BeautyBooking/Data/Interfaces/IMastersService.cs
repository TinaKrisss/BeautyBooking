using BeautyBooking.Data.Base;
using BeautyBooking.Models;

namespace BeautyBooking.Data.Interfaces
{
    public interface IMastersService : IEntityBaseRepository<Master>
    {
        Task<Master> GetByEmailAsync(string email);
    }
}
