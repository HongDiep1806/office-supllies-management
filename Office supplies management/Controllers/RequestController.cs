using MediatR;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Models;
using System.Formats.Asn1;
using System.Security.Cryptography.Xml;

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
            var query = new CountRequestsQuery();
            var number = await _mediator.Send(query);
            return Ok(number);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetRequestByID(int id)
        {
            var query = new GetRequestByIDQuery(id);
            var request = await _mediator.Send(query);
            return Ok(request);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteRequestCommand(id);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRequestDto updateRequestDto)
        {
            var commmand = new UpdateRequestCommand(updateRequestDto);
            var result = await _mediator.Send(commmand);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
    
