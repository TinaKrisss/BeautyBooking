using BeautyBooking.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
		
		[JsonIgnore]
		[ForeignKey("MasterId")]
		public Master Master { get; set; }

		//Record
		[JsonIgnore]
		public Record Record { get; set; }

	}
}
