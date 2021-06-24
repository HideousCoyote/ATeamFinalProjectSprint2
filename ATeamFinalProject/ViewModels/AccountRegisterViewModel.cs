using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATeamFinalProject.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required, MaxLength(256), EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required, MinLength(6), MaxLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required, MinLength(6), MaxLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don't Match")]
        public string ConfirmPassword { get; set; }
    }
}
