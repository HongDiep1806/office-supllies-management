using MediatR;
using Office_supplies_management.Features.Products.Commands;
using Office_supplies_management.Repositories;
using Office_supplies_management.Services;

namespace Office_supplies_management.Features.Products.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductService _productService;
        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.Delete(request.id);
        }
    }
}
