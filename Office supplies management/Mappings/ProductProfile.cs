using AutoMapper;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.Models;

namespace Office_supplies_management.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
