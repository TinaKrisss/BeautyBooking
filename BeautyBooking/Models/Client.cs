using BeautyBooking.Data.Base;
using BeautyBooking.Data.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace BeautyBooking.Models
{
	public class Client:IEntityBase
	{
		[Key]
		public int Id { get; set; }

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

		[Display(Name = "Дата народження")]
		[Required(ErrorMessage = "Введіть дату народження!")]
		public DateTime BirthDate { get; set; }

		[Display(Name = "Стать")]
		public Gender Gender { get; set; }

		[Display(Name = "Номер телефону")]
		[Required(ErrorMessage = "Введіть номер телефону!")]
		[RegularExpression("^\\+380 \\(\\d{2}\\) \\d{3} \\d{4}$", ErrorMessage = "Номер телефону повинен мати 13 цифр та починатися з +380")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Ел. пошта")]
		[Required(ErrorMessage = "Введіть електронну пошту!")]
		[RegularExpression("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$", ErrorMessage = "Адреса ел. пошти має відповідати формату example@gmail.com")]
		public string Email { get; set; }

		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Введіть пароль!")]
		[RegularExpression("^(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).{8,}$", ErrorMessage = "Пароль повинен містити щонайменше 8 символів, велику букву, цифру та спеціальний символ.")]
		public string Password { get; set; }

		//Relationships
		//Records
		[BindNever]
		public List<Record> Records { get; set; }

	}
}
