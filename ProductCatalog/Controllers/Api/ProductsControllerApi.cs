using System.Web.Http;
using ProductCatalog.Service;

namespace ProductCatalog.Controllers.Api
{
    public class ProductsControllerApi : ApiController
    {
        private readonly IProductRepository _productRepository;

        public ProductsControllerApi(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}