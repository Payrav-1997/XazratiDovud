using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email не должен быть пустым!")]
        [EmailAddress(ErrorMessage = "Формат Email - а должет быть правильным")]
        [Display(Name = "Email")]
        public string Eamil { get; set; }
        [Required(ErrorMessage = "Пароль не должен быть пустым!")]
        [StringLength(100, ErrorMessage = "Минимальная длина пароля 6 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Пароли не совподают!")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }
    }
}
