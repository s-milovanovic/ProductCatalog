using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductCatalog.Models;
using ProductCatalog.Service;

namespace ProductCatalog.Repository
{
    public class ProductDbRepository : IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> InsertProductAsync(Product product, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllProductCategoriesAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Supplier>> GetAllSuppliersAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}