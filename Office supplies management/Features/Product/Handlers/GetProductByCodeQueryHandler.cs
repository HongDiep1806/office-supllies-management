using MediatR;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.Features.Product.Queries;

namespace Office_supplies_management.Features.Product.Handlers
{
    public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, ProductDto>
    {
        private readonly IProductService _productService;

        public GetProductByCodeQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductDto> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetProductByCodeAsync(request.Code);
        }
    }
}
