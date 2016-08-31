using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.Models
{
    public class Objectif
    {
        public int Id { get; set; }        
        public int NbPoint { get; set; }
        public string Tache { get; set; }
        public int ObjTypeId { get; set; }
        public DateTime Date { get; set; }
        public string Commentaire { get; set; }
        public int UserId { get; set; }



    }
}
