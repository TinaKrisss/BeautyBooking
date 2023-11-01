using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautyBooking.Models
{
	public class GroupOfServices : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		//Relationships
		//Record
		public int RecordId { get; set; }
		[ForeignKey("RecordId")]
		public Record Record { get; set; }

		//Service
		public int ServiceId { get; set; }
		[ForeignKey("ServiceId")]
		public Service Service { get; set; }

	}
}
