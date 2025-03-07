using MediatR;
using Office_supplies_management.Models;
using Office_supplies_management.DTOs.Product;

namespace Office_supplies_management.Features.Products.Commands
{
    public class AddProductCommand : IRequest<bool>
    {
        public CreateProductDto createRequest;
        public AddProductCommand(CreateProductDto createRequest)
        {
            this.createRequest = createRequest;
        }
    }
}
