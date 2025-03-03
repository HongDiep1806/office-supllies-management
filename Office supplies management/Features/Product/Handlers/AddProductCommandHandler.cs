using MediatR;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.Features.Products.Commands;
using Office_supplies_management.Models;
using Office_supplies_management.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Office_supplies_management.Features.Products.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, bool>
    {
        private readonly IProductService _productService;

        public AddProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.Create(request.createRequest);
        }
    }
}
