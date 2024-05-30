using ProductCatalog.Models;

namespace ProductCatalog.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Supplier Supplier { get; set; }
        public decimal Price { get; set; }
    }
}