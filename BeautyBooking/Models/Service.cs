using BeautyBooking.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace BeautyBooking.Models
{
	public class Service : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Назва")]
		[Required(ErrorMessage = "Введіть назву процедури!")]
		[StringLength(70, MinimumLength = 1, ErrorMessage = "Назва має бути більше 1 та менше 70 символів!")]
		public string Name { get; set; }

		[Display(Name = "Опис")]
		[StringLength(500, MinimumLength = 1, ErrorMessage = "Текст опису не має перевищувати 500 символів!")]
		public string? Description { get; set; }

		[Display(Name = "Тривалість, хв.")]
		[Required(ErrorMessage = "Введіть тривалість процедури!")]
		public int Duration { get; set; }

		[Display(Name = "Ціна, грн")]
		[Required(ErrorMessage = "Введіть ціну процедури!")]
		public decimal Price { get; set; }

		//Relationships
		//GroupOfServices
		public List<GroupOfServices> GroupOfServices { get; set; }
	}
}
