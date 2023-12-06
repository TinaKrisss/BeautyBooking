using BeautyBooking.Data.Enums;
using BeautyBooking.Models;

namespace BeautyBooking.Data.ViewModels
{
    public class EditRecordVM
    {
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string MasterName { get; set; }
        public string MasterSurname { get; set; }
        public List<Service> Services { get; set; }
        public FreeTime FreeTime { get; set; }
        public Status Status { get; set; }
    }
}
