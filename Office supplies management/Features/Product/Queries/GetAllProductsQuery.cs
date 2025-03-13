using MediatR;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Models;
using System.Collections.Generic;

namespace Office_supplies_management.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}