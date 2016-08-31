using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Life30.Models
{
    public class ObjectifType
    {
        public int Id { get; set; }
                
        public string Name { get; set; }

        public List<Item> Items { get; set; } 

        

    }
}