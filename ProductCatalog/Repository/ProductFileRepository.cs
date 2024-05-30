using System;
using Newtonsoft.Json;
using ProductCatalog.Models;
using ProductCatalog.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private const string FileName = "ProductCatalog.json";
        private readonly FileContext _fileContext;

        public ProductFileRepository()
        {
            _fileContext = LoadFileContext();
        }

        private FileContext LoadFileContext()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            const string relativeFolderPath = "Files";
            const string fileName = "ProductCatalog.json";
            string filePath = Path.Combine(baseDirectory, relativeFolderPath, fileName);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<FileContext>(json);
            }

            return new FileContext();
        }

        private Task SaveFileContextAsync(CancellationToken cancellationToken)
        {
            string json = JsonConvert.SerializeObject(_fileContext);

            File.WriteAllText(FileName, json);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult<IEnumerable<Product>>(_fileContext.Products);
        }

        public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_fileContext.Products.Find(p => p.Id == id));
        }

        public async Task<int> InsertProductAsync(Product product, CancellationToken cancellationToken)
        {
            int id = _fileContext.Products.Max(it => it.Id);

            id++;

            product.Id = id;

            await SaveFileContextAsync(cancellationToken);

            return product.Id;
        }

        public async Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            var existingProduct = _fileContext.Products.Find(it => it.Id == product.Id);

            if (existingProduct is null)
            {
                return false;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            await SaveFileContextAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<Category>> GetAllProductCategoriesAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult<IEnumerable<Category>>(_fileContext.Categories);
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult<IEnumerable<Supplier>>(_fileContext.Suppliers);
        }

        public async Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult<IEnumerable<Manufacturer>>(_fileContext.Manufacturers);
        }
    }
}