using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Service;

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
    }
}