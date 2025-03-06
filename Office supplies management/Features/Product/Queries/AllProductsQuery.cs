using MediatR;
using Office_supplies_management.DTOs.Product;

namespace Office_supplies_management.Features.Product.Queries
{
    public class AllProductsQuery:IRequest<List<ProductDto>>
    {
    }
}
