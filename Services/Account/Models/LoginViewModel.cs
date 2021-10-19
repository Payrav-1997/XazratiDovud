using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email не должен быть пустым!")]
        [EmailAddress(ErrorMessage ="Формат Email - а должен быть правильным")]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пароль не должен быть пустым!")]
        [StringLength(100, ErrorMessage = "Минимальная длина пароля 6 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}
