using BeautyBooking.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BeautyBooking.Data.ViewModels
{
	public class SignInVM
	{
		[Display(Name = "Ел. пошта")]
		[Required(ErrorMessage = "Не знайдено облікового запису з цією ел. поштою.")]
		public string Email { get; set; }

		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Неправильний пароль.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}