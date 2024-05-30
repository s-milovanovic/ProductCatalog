using ProductCatalog.Models;
using ProductCatalog.Service;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalog.Repository
{
    public class FileContext
    {
        public List<Product> Products { get; } = new List<Product>();

        public List<Category> Categories { get; } = new List<Category>();

        public List<Supplier> Suppliers { get; } = new List<Supplier>();

        public List<Manufacturer> Manufacturers { get; } = new List<Manufacturer>();
    }

    public class ProductFileRepository : IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertProductAsync(Product product, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllProductCategoriesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Supplier>> GetAllSuppliersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}