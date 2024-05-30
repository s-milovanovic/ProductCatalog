using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ProductCatalog.Models;
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