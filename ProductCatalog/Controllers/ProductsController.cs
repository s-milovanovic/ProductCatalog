using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
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

        // GET: Products/Details/1
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

        // POST: Products
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductFormViewModel model)
        {
            var cancellationToken = HttpContext.Request.TimedOutToken;

            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Product.Name,
                    Description = model.Product.Description,
                    CategoryId = model.Product.CategoryId,
                    ManufacturerId = model.Product.ManufacturerId,
                    SupplierId = model.Product.SupplierId,
                    Price = model.Product.Price
                };

                int productId = await _productRepository.InsertProductAsync(product, cancellationToken);

                return RedirectToAction("Details", "Products", new RouteValueDictionary { { "Id", productId } });
            }

            await SetProductFormViewModel(model, cancellationToken);

            return View("ProductForm", model);
        }

        // GET: Products/Edit/1
        public async Task<ActionResult> Edit(int id)
        {
            var cancellationToken = HttpContext.Request.TimedOutToken;

            var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);

            if (product is null)
            {
                return HttpNotFound();
            }

            IEnumerable<Category> categories = await _productRepository.GetAllProductCategoriesAsync(cancellationToken);
            IEnumerable<Manufacturer> manufacturers = await _productRepository.GetAllManufacturersAsync(cancellationToken);
            IEnumerable<Supplier> suppliers = await _productRepository.GetAllSuppliersAsync(cancellationToken);

            var viewModel = new ProductFormViewModel
            {
                Product = product,
                Categories = categories,
                Manufacturers = manufacturers,
                Suppliers = suppliers
            };

            return View("ProductForm", viewModel);
        }

        #region PrivateMethods

        private async Task SetProductFormViewModel(ProductFormViewModel model, CancellationToken cancellationToken)
        {
            model.Categories = await _productRepository.GetAllProductCategoriesAsync(cancellationToken);
            model.Manufacturers = await _productRepository.GetAllManufacturersAsync(cancellationToken);
            model.Suppliers = await _productRepository.GetAllSuppliersAsync(cancellationToken);
        }

        #endregion
    }
}