using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautyBooking.Models
{
	public class FreeTime : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Дата і час")]
		[Required(ErrorMessage = "Введіть дату та час!")]
		public DateTime DateAndTime { get; set; }

		//Relationships
		//Master
		public int MasterId { get; set; }
		[ForeignKey("MasterId")]
		public Master Master { get; set; }

		//Record
		public Record Record { get; set; }

	}
}
