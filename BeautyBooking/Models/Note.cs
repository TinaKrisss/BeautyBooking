using BeautyBooking.Data.Enums;
using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautyBooking.Models
{
	public class Note : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Назва")]
		[Required(ErrorMessage = "Введіть назву позиції!")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Назва має бути більше 1 та менше 50 символів!")]
		public string Title { get; set; }

		[Display(Name = "Ціна")]
		[Required(ErrorMessage = "Введіть ціну процедури!")]
		public decimal Price { get; set; }

		[Display(Name = "Приорітет")]
		public Priority Priority { get; set; }

		//Relationships
		//Master
		public int MasterId { get; set; }
		[ForeignKey("MasterId")]
		public Master Master { get; set; }
	}
}
