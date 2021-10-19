using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email не должен быть пустым!")]
        [EmailAddress(ErrorMessage = "Формат Email - а должет быть правильным")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
