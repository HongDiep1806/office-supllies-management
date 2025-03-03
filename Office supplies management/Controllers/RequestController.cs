using MediatR;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Models;

namespace Office_supplies_management.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RequestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestDto request)
        {
            var command = new AddRequestCommand(request);
            var createdRequest = await _mediator.Send(command);
            return Ok(createdRequest);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestsByUserID(int id)
        {
            var requests = await _mediator.Send(new GetRequestsByUserIDQuery(id));
            return Ok(requests);
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetRequestNumber()
        {
            var query = new GetAllRequestQuery();
            var requests = await _mediator.Send(query);
            return Ok(requests.Count());        
        }
        [HttpGet("getbyid{id}")]
        public async Task<IActionResult> GetRequestByID (int id)
        {
            var query = new GetRequestByIDQuery(id);
            var request = await _mediator.Send(query);    
            return Ok(request);
        }
    }
}
