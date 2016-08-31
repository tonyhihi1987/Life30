using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.ViewModels
{
    public class LoginViewModel
    {
        [Required]        
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi?")]
        public bool RememberMe { get; set; }
    }
}
