using BeautyBooking.Data.Base;
using BeautyBooking.Models;

namespace BeautyBooking.Data.Interfaces
{
    public interface IFreeTimeService : IEntityBaseRepository<FreeTime>
    {
        Task<FreeTime> GetByMaster(int masterId, DateTime dateTime);
    }
}
