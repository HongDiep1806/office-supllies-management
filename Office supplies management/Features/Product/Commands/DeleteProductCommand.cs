using MediatR;
using Office_supplies_management.Models;

namespace Office_supplies_management.Features.Products.Commands
{
    public class DeleteProductCommand :IRequest<bool>
    {
        public int id {  get; set; }
        public DeleteProductCommand(int id)
        {
            this.id = id;
        }
    }
}
