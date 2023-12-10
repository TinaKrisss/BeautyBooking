using BeautyBooking.Data.Enums;
using BeautyBooking.Models;

namespace BeautyBooking.Data.ViewModels
{
    public class EditRecordVM
    {
        public int Id { get; set; }
        public int FreeTimeId { get; set; }
        public int MasterId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string MasterName { get; set; }
        public string MasterSurname { get; set; }
        public List<Service> Services { get; set; }
        public DateTime DateAndTime { get; set; }
        public Status Status { get; set; }
    }
}
