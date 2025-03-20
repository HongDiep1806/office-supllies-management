using MediatR;
using Office_supplies_management.DTOs.Product;

namespace Office_supplies_management.Features.Product.Queries
{
    public class SearchProductsQuery : IRequest<List<ProductDto>>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
