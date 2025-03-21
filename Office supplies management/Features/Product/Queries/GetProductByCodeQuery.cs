using MediatR;
using Office_supplies_management.DTOs.Product;

namespace Office_supplies_management.Features.Product.Queries
{
    public class GetProductByCodeQuery : IRequest<ProductDto>
    {
        public string Code { get; set; }
    }
}
