using System.ComponentModel.DataAnnotations;

namespace BeautyBooking.Data.ViewModels
{
    public class CreateMasterVM
    {
		[Display(Name = "Фото профілю")]
		public string? ProfilePhotoURL { get; set; }

		[Display(Name = "Прізвище")]
		[Required(ErrorMessage = "Введіть прізвище!")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Прізвище має бути більше 1 та менше 50 символів!")]
		public string Surname { get; set; }

		[Display(Name = "Ім'я")]
		[Required(ErrorMessage = "Введіть ім'я!")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Ім'я має бути більше 1 та менше 50 символів!")]
		public string Name { get; set; }

		[Display(Name = "Опис")]
		[StringLength(500, MinimumLength = 1, ErrorMessage = "Текст опису не має перевищувати 500 символів!")]
		public string? Info { get; set; }

		[Display(Name = "Ел. пошта")]
		[Required(ErrorMessage = "Введіть електронну пошту!")]
		[RegularExpression("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$", ErrorMessage = "Адреса ел. пошти має відповідати формату example@gmail.com")]
		public string Email { get; set; }
	}
}
