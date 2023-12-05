using BeautyBooking.Data.Base;
using BeautyBooking.Data.ViewModels;
using BeautyBooking.Models;

namespace BeautyBooking.Data.Interfaces
{
    public interface IRecordsService : IEntityBaseRepository<Record>
	{
        public Task<List<RecordsVM>> GetRecords();
        public Task<ConfirmOrderVM> GetRecordConfirmation(int id);
    }
}
