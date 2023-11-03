using BeautyBooking.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BeautyBooking.Data.ViewModels
{
	public class SignInVM
	{
		[Display(Name = "Ел. пошта")]
		[Required(ErrorMessage = "Введіть електронну пошту!")]
		public string Email { get; set; }

		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Введіть пароль!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}