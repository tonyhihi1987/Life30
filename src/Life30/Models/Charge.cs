using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.Models
{
    public class Charge
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Libelle { get; set; }
        public string Type { get; set; }
        public float Montant { get; set; }
    }
}
