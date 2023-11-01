using BeautyBooking.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BeautyBooking.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Display(Name = "Прізвище")]
		public string Surname { get; set; }

		[Display(Name = "Ім'я")]
		public string Name { get; set; }

		[Display(Name = "Дата народження")]
		public DateTime BirthDate { get; set; }

		[Display(Name = "Стать")]
		public Gender Gender { get; set; }

		[Display(Name = "Пароль")]
		public string Password { get; set; }
	}
}