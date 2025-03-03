using MediatR;
using Office_supplies_management.DTOs.Product;

namespace Office_supplies_management.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public UpdateProductDto updateRequest { get; set; }
        public UpdateProductCommand(UpdateProductDto updateRequest)
        {
            this.updateRequest = updateRequest;
        }
    }
}
