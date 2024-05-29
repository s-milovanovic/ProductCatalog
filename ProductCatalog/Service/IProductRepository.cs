using ProductCatalog.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalog.Service
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken);

        Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken);

        Task<int> InsertProductAsync(Product product, CancellationToken cancellationToken);

        Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken);

        Task<IEnumerable<Category>> GetAllProductCategoriesAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Supplier>> GetAllSuppliersAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync(CancellationToken cancellationToken);
    }
}
