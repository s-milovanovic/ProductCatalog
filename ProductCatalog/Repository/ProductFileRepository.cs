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
        private const string RelativeFolderPath = "Files";
        private readonly FileContext _fileContext;

        public ProductFileRepository()
        {
            _fileContext = LoadFileContext();
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
            if (product == null) throw new ArgumentNullException(nameof(product));

            int id = 0;

            if (_fileContext.Products.Any())
            {
                id = _fileContext.Products.Max(it => it.Id);
            }

            id++;

            product.Id = id;

            SetProductAttributes(product);

            _fileContext.Products.Add(product);

            await SaveFileContextAsync();

            return product.Id;
        }

        public async Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            var existingProduct = _fileContext.Products.Find(it => it.Id == product.Id);

            if (existingProduct is null)
            {
                return false;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            SetProductAttributes(product, existingProduct);

            await SaveFileContextAsync();

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

        #region PrivateMethods
        private void SetProductAttributes(Product product)
        {
            product.Category = _fileContext.Categories.SingleOrDefault(it => it.Id == product.CategoryId);
            product.Manufacturer = _fileContext.Manufacturers.SingleOrDefault(it => it.Id == product.ManufacturerId);
            product.Supplier = _fileContext.Suppliers.SingleOrDefault(it => it.Id == product.SupplierId);
        }

        private void SetProductAttributes(Product product, Product existingProduct)
        {
            existingProduct.Category = _fileContext.Categories.SingleOrDefault(it => it.Id == product.CategoryId);
            existingProduct.Manufacturer = _fileContext.Manufacturers.SingleOrDefault(it => it.Id == product.ManufacturerId);
            existingProduct.Supplier = _fileContext.Suppliers.SingleOrDefault(it => it.Id == product.SupplierId);
        }

        private FileContext LoadFileContext()
        {
            string filePath = GetFilePath();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<FileContext>(json);
            }

            return new FileContext();
        }

        private string GetFilePath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDirectory, RelativeFolderPath, FileName);
        }

        private Task SaveFileContextAsync()
        {
             string filePath = GetFilePath();

            string json = JsonConvert.SerializeObject(_fileContext);

            File.WriteAllText(filePath, json);

            return Task.CompletedTask;
        }

        #endregion
    }
}