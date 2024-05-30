using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Category field is required")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required(ErrorMessage = "Manufacturer field is required")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [Required(ErrorMessage = "Supplier field is required")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Required]
        [Range(1, 999999999, ErrorMessage = "Price must be between 1 and 999999999.")]
        public decimal? Price { get; set; }
    }
}