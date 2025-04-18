﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.Product;
using Office_supplies_management.Features.Product.Queries;
using Office_supplies_management.Features.Products.Commands;
using Office_supplies_management.Features.Products.Queries;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

namespace Office_supplies_management.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly HttpClient _httpClient;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("allproductsincludedeleted")]
        public async Task<IActionResult> AllProducts()
        {
            return Ok(await _mediator.Send(new AllProductsQuery()));
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string? name, [FromQuery] string? code, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var query = new SearchProductsQuery { Name = name, Code = code, MinPrice = minPrice, MaxPrice = maxPrice };
            var products = await _mediator.Send(query);
            return Ok(products);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("get-by-code")]
        public async Task<IActionResult> GetProductByCode([FromQuery] string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Code is required.");
            }

            var query = new GetProductByCodeQuery { Code = code };
            var product = await _mediator.Send(query);

            if (product == null)
            {
                return NotFound($"Product with code {code} not found.");
            }

            return Ok(product);
        }
        [Authorize(Policy = "RequireFinanceEmployee")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto request)
        {
            var command = new UpdateProductCommand(request);
            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("Errors occurred while updating the product.");
            }

            return Ok();
        }
        [Authorize(Policy = "RequireFinanceEmployee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("Errors occurred while deleting the product.");
            }

            return Ok();
        }

        [Authorize(Policy = "RequireFinanceEmployee")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto request)
        {
            var command = new AddProductCommand(request);
            var createdProduct = await _mediator.Send(command);
            return Ok(createdProduct);
        }
        
    }
}
