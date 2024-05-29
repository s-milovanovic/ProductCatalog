using ProductCatalog.Models;
using System.Collections.Generic;

namespace ProductCatalog.ViewModels
{
    public class ProductFormViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public Product Product { get; set; }
    }
}