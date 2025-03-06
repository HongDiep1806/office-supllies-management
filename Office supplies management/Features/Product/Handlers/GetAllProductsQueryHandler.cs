using MediatR;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;
using Office_supplies_management.Features.Products.Queries;
using System.Diagnostics;
using Office_supplies_management.Services;
using Office_supplies_management.DTOs.Product;

namespace Office_supplies_management.Features.Products.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductService _productService;
        public GetAllProductsQueryHandler(IProductService productService)
        {
           _productService = productService;
           
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAll();
        }
    }
}