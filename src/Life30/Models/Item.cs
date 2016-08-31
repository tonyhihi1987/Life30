using System.ComponentModel.DataAnnotations;

namespace Life30.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }        
        public int ObjectifTypeId { get; set; }

    }
}