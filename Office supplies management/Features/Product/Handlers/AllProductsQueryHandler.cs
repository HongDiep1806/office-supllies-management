using MediatR;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.Features.Product.Queries;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Product.Handlers
{
    public class AllProductsQueryHandler : IRequestHandler<AllProductsQuery, List<ProductDto>>
    {
        private readonly IProductService _productService;
        public AllProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<List<ProductDto>> Handle(AllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productService.AllItems(); 
        }
    }
}
