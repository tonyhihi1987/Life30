using Life30.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.ViewModels
{
    public class FinanceViewModel
    {
        public List<string> Types { get; set; } = new List<string>
        {
            "ElectroMénager",
            "Alimentation",
            "Tabac",
            "Hifi/tv"
        };
        public Depense CurrentDepense { get; set; } = new Depense();
        public List<Charge> Charges { get; set; }        
    }
}
