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

        public async Task<int> InsertProductAsync(Product product, CancellationToken cancellationToken)
        {
            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }

        public async Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            _dbContext.Products.Attach(product);

            _dbContext.Entry(product).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync(cancellationToken) != 0;
        }

        public async Task<IEnumerable<Category>> GetAllProductCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Suppliers.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Manufacturers.ToListAsync(cancellationToken);
        }
    }
}