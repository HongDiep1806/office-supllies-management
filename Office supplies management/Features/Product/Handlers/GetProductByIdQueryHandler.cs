using MediatR;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.Features.Products.Queries;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductRequestDto>
    {
        private readonly IProductService _productService;
        public GetProductByIdQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<ProductRequestDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetById(request.Id);

        }
    }
}
