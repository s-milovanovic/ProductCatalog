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

        //GET /api/products/1
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var cancellationToken = HttpContext.Current.Request.TimedOutToken;

            try
            {
                var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);

                if (product is null)
                {
                    return NotFound();
                }

                var productDto = Mapper.Map<Product, ProductDto>(product);

                return Ok(productDto);
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

        //POST /api/products
        [HttpPost]
        public async Task<IHttpActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            var cancellationToken = HttpContext.Current.Request.TimedOutToken;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    CategoryId = productDto.Category.Id,
                    ManufacturerId = productDto.Category.Id,
                    SupplierId = productDto.Supplier.Id,
                    Price = productDto.Price
                };

                int productId = await _productRepository.InsertProductAsync(product, cancellationToken);

                productDto.Id = productId;

                return Created(new Uri(Request.RequestUri + "/" + productId), productDto);
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    // Log the exception (ex) as needed
                    // Handle different exception types if necessary and return appropriate responses
                    case ArgumentNullException _:
                        return BadRequest("Product data cannot be null.");
                    case HttpRequestException _:
                        return StatusCode(HttpStatusCode.RequestTimeout);
                    default:
                        // For all other exceptions
                        return InternalServerError(exception);
                }
            }
        }

        //PUT /api/products/1
        [HttpPut]
        public async Task<IHttpActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            try
            {
                var cancellationToken = HttpContext.Current.Request.TimedOutToken;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var selectedProduct = await _productRepository.GetProductByIdAsync(id, cancellationToken);

                if (selectedProduct == null)
                {
                    return NotFound();
                }

                selectedProduct.Name = productDto.Name;
                selectedProduct.Description = productDto.Description;
                selectedProduct.CategoryId = productDto.Category.Id;
                selectedProduct.ManufacturerId = productDto.Manufacturer.Id;
                selectedProduct.SupplierId = productDto.Supplier.Id;
                selectedProduct.Price = productDto.Price;

                await _productRepository.UpdateProductAsync(selectedProduct, cancellationToken);

                return StatusCode(HttpStatusCode.NoContent); // 204 No Content
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}
