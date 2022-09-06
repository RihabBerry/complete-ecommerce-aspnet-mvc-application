using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public string EmailAddress { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password  is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password doesnt match")]
        [Display(Name="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
