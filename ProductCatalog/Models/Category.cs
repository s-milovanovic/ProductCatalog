using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
    public class Category
    { 
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}