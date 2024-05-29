using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using ProductCatalog.Models;
using ProductCatalog.Service;

namespace ProductCatalog.Repository
{
    public class ProductDbRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductDbRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            List<Product> products = await _dbContext.Products
                .Include(product => product.Supplier)
                .Include(product => product.Category)
                .Include(product => product.Manufacturer)
                .ToListAsync(cancellationToken);

            return products.Count > 0 ? products : null;
        }

        public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Include(product => product.Supplier)
                .Include(product => product.Category)
                .Include(product => product.Manufacturer)
                .SingleOrDefaultAsync(product => product.Id == id, cancellationToken);
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