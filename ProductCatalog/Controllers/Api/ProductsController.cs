using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using ProductCatalog.Dtos;
using ProductCatalog.Models;
using ProductCatalog.Service;

namespace ProductCatalog.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //GET /api/products
        public async Task<IHttpActionResult> GetProducts()
        {
            var cancellationToken = HttpContext.Current.Request.TimedOutToken;

            try
            {
                IEnumerable<Product> products = await _productRepository.GetAllProductsAsync(cancellationToken);

                List<ProductDto> productDtos = products.Select(Mapper.Map<Product, ProductDto>).ToList();

                return Ok(productDtos);
            }
            catch (Exception exception)
            {
                if (exception is HttpRequestException)
                {
                    return StatusCode(HttpStatusCode.RequestTimeout);
                }

                return InternalServerError(exception);
            }
        }
    }
}
