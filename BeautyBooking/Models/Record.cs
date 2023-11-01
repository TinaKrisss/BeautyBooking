using BeautyBooking.Data.Enums;
using BeautyBooking.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautyBooking.Models
{
	public class Record : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Відгук")]
		[StringLength(1000, MinimumLength = 1, ErrorMessage = "Текст опису не має перевищувати 1000 символів!")]
		public string? Feedback { get; set; }

		[Display(Name = "Статус")]
		public Status Status { get; set; }

		//Relationships
		//FreeTime
		public int FreeTimeId { get; set; }
		[ForeignKey("FreeTimeId")]
		public FreeTime FreeTime { get; set; }

		//Clients
		public int ClientId { get; set; }
		[ForeignKey("ClientId")]
		public Client Client { get; set; }

		//GroupOfServices
		public List<GroupOfServices> GroupOfServices { get; set; }
	}
}
