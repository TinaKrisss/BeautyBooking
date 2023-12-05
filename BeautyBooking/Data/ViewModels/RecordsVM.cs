﻿using BeautyBooking.Data.Enums;

namespace BeautyBooking.Data.ViewModels
{
    public class RecordsVM
    {
        public int Id { get; set; }
        public string MasterName { get; set; }
        public string MasterSurname { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public DateTime Time { get; set; }
        public Status Status { get; set; }
    }
}
