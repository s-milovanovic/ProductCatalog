using AutoMapper;
using ProductCatalog.Api.Model;
using ProductCatalog.Models;

namespace ProductCatalog
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<ProductDto, Product>();
        }
    }
}