using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Pseudo")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]        
        //[DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le password et la confirmation sont différent...")]
        public string ConfirmPassword { get; set; }
    }
}
