using AutoMapper;
using Office_supplies_management.DTOs.ProductRequest;
using Office_supplies_management.Models;

namespace Office_supplies_management.Mappings
{
    public class Product_RequestProfile:Profile
    {
        public Product_RequestProfile() 
        {
            CreateMap<Product_Request, ProductRequestDto>();
            CreateMap<ProductRequestDto, Product_Request>();

        }
    }
}
