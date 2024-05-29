using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}