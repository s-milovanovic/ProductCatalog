using ProductCatalog.Models;
using System.Data.Entity;

namespace ProductCatalog.Repository
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext() : base("Server=PC300;Database=ProductCatalog;Trusted_Connection=True;")
        {
            Database.SetInitializer<ProductDbContext>(null);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }
    }
}