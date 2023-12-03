namespace BeautyBooking.Data.ViewModels
{
	public class CreateFreeTimeVM
	{
		public int Id { get; set; }
		public string? ProfilePhotoURL { get; set; }
		public string? Surname { get; set; }
		public string? Name { get; set; }
		public DateTime DateAndTime { get; set; }
	}
}
