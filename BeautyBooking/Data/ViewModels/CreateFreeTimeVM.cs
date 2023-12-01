using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautyBooking.Data.ViewModels
{
    public class CreateFreeTimeVM
    {
        [Display(Name = "Дата і час")]
        [Required(ErrorMessage = "Введіть дату та час!")]
        public DateTime DateAndTime { get; set; }
    }
}
