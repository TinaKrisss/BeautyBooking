namespace BeautyBooking.Data.ViewModels
{
	public class EditFreeTimeVM
	{
		public int Id { get; set; }
		public string? ProfilePhotoURL { get; set; }
		public string? Surname { get; set; }
		public string? Name { get; set; }
		public IEnumerable<DateTime> MastersFreeTime { get; set; }
		public int FreeTimeId { get; set; } //to edit
		public DateTime NewDateAndTime { get; set; }
	}
}
