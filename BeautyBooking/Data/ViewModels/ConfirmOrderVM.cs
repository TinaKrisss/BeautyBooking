using BeautyBooking.Models;

namespace BeautyBooking.Data.ViewModels
{
    public class ConfirmOrderVM
    {
        public string MasterName { get; set; }
        public string MasterSurname { get; set; }
        public List<Service> Services { get; set; }
        public string Time { get; set; }
        public int Price { get; set; }
    }
}
