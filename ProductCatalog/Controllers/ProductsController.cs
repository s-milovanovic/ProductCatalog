using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Service;
using ProductCatalog.ViewModels;

namespace ProductCatalog.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var cancellationToken = HttpContext.Request.TimedOutToken;

            IEnumerable<Product> products = await _productRepository.GetAllProductsAsync(cancellationToken);

            if (products is null)
            {
                return HttpNotFound();
            }

            return View(products);
        }

        // GET: Products/1
        public async Task<ActionResult> Details(int id)
        {
            var cancellationToken = HttpContext.Request.TimedOutToken;

            var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/New
        public async Task<ActionResult> New()
        {
            var cancellationToken = HttpContext.Request.TimedOutToken;

            IEnumerable<Category> categories = await _productRepository.GetAllProductCategoriesAsync(cancellationToken);
            IEnumerable<Manufacturer> manufacturers = await _productRepository.GetAllManufacturersAsync(cancellationToken);
            IEnumerable<Supplier> suppliers = await _productRepository.GetAllSuppliersAsync(cancellationToken);

            var productCreateViewModel = new ProductFormViewModel
            {
                Categories = categories,
                Manufacturers = manufacturers,
                Suppliers = suppliers,
                Product = new Product()
            };

            return View("ProductForm", productCreateViewModel);
        }
    }
}