using Life30.Models;
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

        public int Id { get; set; }
        [Required]
        public int NbPoint { get; set; }
        public List<string> Items { get; set; } = new List<string>();
        [Required(ErrorMessage = "La Tache est obligatoire")]
        public string Tache { get; set; }
        public DateTime Date { get; set; }
        public string Commentaire { get; set; }
        public int UserId { get; set; }
        public int[] nbPoints { get; set; } = new int[30];
        public string Type { get; set; }


    }
}
