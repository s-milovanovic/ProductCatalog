using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}