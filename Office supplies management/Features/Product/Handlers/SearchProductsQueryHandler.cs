using MediatR;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.Features.Product.Queries;

namespace Office_supplies_management.Features.Product.Handlers
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, List<ProductDto>>
    {
        private readonly IProductService _productService;

        public SearchProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<ProductDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productService.SearchProductsAsync(request.Name, request.Code, request.MinPrice, request.MaxPrice);
        }
    }
}
