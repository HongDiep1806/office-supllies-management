using MediatR;
using Office_supplies_management.Features.Products.Commands;
using Office_supplies_management.Repositories;
using Office_supplies_management.Models;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Products.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductService _productService;
        public UpdateProductCommandHandler(IProductRepository productRepository, IProductService productService)
        {
           _productService = productService;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.Update(request.updateRequest);
        }
    }
}
