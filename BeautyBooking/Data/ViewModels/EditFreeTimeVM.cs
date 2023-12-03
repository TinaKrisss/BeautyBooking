using BeautyBooking.Models;

namespace BeautyBooking.Data.ViewModels
{
	public class EditFreeTimeVM
	{
		public int Id { get; set; }
		public string? ProfilePhotoURL { get; set; }
		public string? Surname { get; set; }
		public string? Name { get; set; }
		public IEnumerable<FreeTime> MastersFreeTime { get; set; }
		public int? FreeTimeId { get; set; } //to editF
		public DateTime NewDateAndTime { get; set; }
	}
}
