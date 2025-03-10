using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Models;
using System.Formats.Asn1;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.IdentityModel.Tokens.Jwt;
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
            // if user role has "Leader" in it, return all requests for user within same department of the leader
            // get user role by id first
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
            var command = new UpdateRequestCommand(updateRequestDto);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("department/{departmentName}")]
        [Authorize(Policy = "DepartmentQuery")]
        public async Task<IActionResult> GetRequestsByDepartment(string departmentName)
        {
            var query = new GetRequestsByDepartmentQuery(departmentName);
            var requests = await _mediator.Send(query);
            return Ok(requests);
        }
        [HttpPost("approve-request/{requestId}")]
        [Authorize(Policy = "DepartmentQuery")]
        public async Task<IActionResult> ApproveRequestDepLeader(int requestId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                              User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid User ID in token.");
            }

            var command = new ApproveRequestDepLeaderCommand(requestId, userId);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Request approved successfully.");
            }
            return BadRequest("Failed to approve request.");
        }
    }
}
