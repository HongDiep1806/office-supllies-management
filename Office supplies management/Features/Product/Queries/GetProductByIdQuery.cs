using MediatR;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.Models;
using Office_supplies_management.Repositories;

namespace Office_supplies_management.Features.Products.Queries
{
    public class GetProductByIdQuery: IRequest<ProductDto>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
