using Life30.Models;
using Life30.Models.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.ViewModels
{
    public class ObjectifViewModel
    {
        public ObjectifViewModel()
        {


        }
        public List<Objectif> Objectifs { get; set; }
        public Boolean isValid { get; set; } = true;
        public int Id { get; set; }
        [Required]        
        public int NbPoint { get; set; }
        public List<string> Items { get; set; } = new List<string>();
        [Required(ErrorMessage = "La Tache est obligatoire")]
        public string Tache { get; set; }
        public DateTime Date { get; set; }
        public string Commentaire { get; set; }
        public int UserId { get; set; }
        public List<int> nbPoints { get; set; } = new List<int>{ 1, 2, 3, 4, 5};
        public string Type { get; set; }
        public List<ACharts> Charts { get; set; } = new List<ACharts>();


    }
}
