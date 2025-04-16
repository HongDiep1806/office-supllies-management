using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Models;
using Office_supplies_management.Services;
using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.IdentityModel.Tokens.Jwt;
using Office_supplies_management.Services;
using Office_supplies_management.Queries;
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
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestDto request)
        {
            var command = new AddRequestCommand(request);
            var createdRequest = await _mediator.Send(command);
            return Ok(createdRequest);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestsByUserID(int id)
        {
            // if user role has "Leader" in it, return all requests for user within same department of the leader
            // get user role by id first
            var requests = await _mediator.Send(new GetRequestsByUserIDQuery(id));
            return Ok(requests);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("count")]
        public async Task<IActionResult> GetRequestNumber()
        {
            var query = new CountRequestsQuery();
            var number = await _mediator.Send(query);
            return Ok(number);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetRequestByID(int id)
        {
            var query = new GetRequestByIDQuery(id);
            var request = await _mediator.Send(query);
            return Ok(request);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
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
        [Authorize(Policy = "AllRolesCanAccess")]
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
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet]
        public async Task<IActionResult> GetAllRequest()
        {
            var query = new GetAllRequestQuery();
            var requests = await _mediator.Send(query);
            return Ok(requests);
        }
        [HttpGet("department/{departmentName}")]
        [Authorize(Policy = "DepartmentQuery")]
        public async Task<IActionResult> GetRequestsByDepartment(string departmentName)
        {
            var query = new GetRequestsByDepartmentQuery(departmentName);
            var requests = await _mediator.Send(query);
            return Ok(requests);
        }
        [Authorize(Policy = "DepartmentQuery")]
        [HttpPut("approveByDepLeader/{requestId}")]
        public async Task<IActionResult> ApproveRequestByDepLeader(int requestId, string note)
        {
            var command = new ApproveRequestByDepLeaderCommand(requestId, note);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Can not find request by id");
            }
        }
        [Authorize(Policy = "DepartmentQuery")]
        [HttpPut("notapproveByDepLeader/{requestId}")]
        public async Task<IActionResult> NotApproveRequestByDepLeader(int requestId,string note)
        {
            var command = new NotApproveRequestByDepLeaderCommand(requestId,note);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Can not find request by id");
            }
        }
        [Authorize(Policy = "RequireFinanceEmployee")]
        [HttpPut("approveRequestByFinEmployee/{requestId}")]
        //[Authorize(Policy = "RequireFinanceEmployee")]
        public async Task<IActionResult> ApproveRequestSupLead(int requestId, string note)
        {
            var command = new ApproveRequestFinEmployeeCommand(requestId, note);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Policy = "RequireFinanceEmployee")]
        [HttpPut("notapproveByFinEmployee/{requestId}")]
        public async Task<IActionResult> NotApproveRequestByFinEmployee(int requestId, string note)
        {
            var command = new NotApproveRequestByFinEmployeeCommand(requestId, note);
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Can not find request by id");
            }
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("all-requests")] // Change the authorization policy
        public async Task<IActionResult> GetAllRequestsForSupLeader()
        {
            var query = new GetAllRequestsForFinEmployeeQuery();
            var requests = await _mediator.Send(query);
            return Ok(requests);
        }
        [HttpGet("approved-requests-list")]
        [Authorize(Policy = "RequireSupLeaderRole")]
        public async Task<IActionResult> GetApprovedRequestsByDepLeader()
        {
            var query = new GetApprovedRequestsQuery();
            var approvedRequests = await _mediator.Send(query);
            return Ok(approvedRequests);
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
        //[HttpGet("getCollectedRequests")]
        //public async Task<IActionResult> GetCollectedRequests()
        //{
        //    var query = new GetCollectedRequestsQuery();
        //    var requests = await _mediator.Send(query);
        //    return Ok(requests);
        //}
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("requests-in-approved-summary")]
        public async Task<IActionResult> GetRequestsInApprovedSummary()
        {
            var query = new GetRequestsInApprovedSummaryQuery();
            var requests = await _mediator.Send(query);
            return Ok(requests);
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("requests-in-date-range")]
        public async Task<IActionResult> GetRequestsInDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetRequestsInDateRangeQuery { StartDate = startDate, EndDate = endDate };
            var requests = await _mediator.Send(query);
            return Ok(requests);
        }

        //[HttpGet("approved-requests-list")]
        //[Authorize(Policy = "RequireFinanceEmployee")]
        //public async Task<IActionResult> GetApprovedRequestsByDepLeader()
        //{
        //    var query = new GetApprovedRequestsQuery();
        //    var approvedRequests = await _mediator.Send(query);
        //    return Ok(approvedRequests);
        //}

        //[HttpPost("approve-sup-lead/{requestId}")]
        //[Authorize(Policy = "RequireFinanceEmployee")]
        //public async Task<IActionResult> ApproveRequestSupLead(int requestId)
        //{
        //    var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ??
        //                      User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        //    if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        //    {
        //        return Unauthorized("Invalid User ID in token.");
        //    }

        //    var userRole = User.FindFirstValue("Role"); // Use the correct claim type for role
        //    if (string.IsNullOrEmpty(userRole) || userRole != "Finance Management Employee")
        //    {
        //        return BadRequest("User role is missing or incorrect.");
        //    }

        //    var command = new ApproveRequestSupLeadCommand(requestId, userId, userRole);
        //    var result = await _requestService.ApproveRequestSupLead(command);
        //    if (result)
        //    {
        //        return Ok(new { message = "Request approved by supervisor leader." });
        //    }
        //    return BadRequest(new { message = "Approval failed. Ensure the user has the correct role and the request exists." });
        //}
        //[HttpGet("all-requests")]
        //[Authorize(Policy = "RequireSupLeaderRole")] // Change the authorization policy
        //public async Task<IActionResult> GetAllRequestsForSupLeader()
        //{
        //    var requests = await _requestService.GetAllRequestsForSupLeader();
        //    return Ok(requests);
        //}
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("approved-requests-by-department")]
        public async Task<IActionResult> GetApprovedRequestsByDepartment([FromQuery] string department)
        {
            var query = new GetApprovedRequestsByDepartmentQuery { Department = department };
            var requests = await _mediator.Send(query);
            return Ok(requests);
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("approved-requests-by-date-range-and-department")]
        public async Task<IActionResult> GetApprovedRequestsByDateRangeAndDepartment([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string department = null)
        {
            if (string.IsNullOrEmpty(department))
            {
                var query = new GetRequestsInDateRangeQuery { StartDate = startDate, EndDate = endDate };
                var requests = await _mediator.Send(query);
                return Ok(requests);
            }
            else
            {
                var query = new GetApprovedRequestsByDateRangeAndDepartmentQuery { StartDate = startDate, EndDate = endDate, Department = department };
                var requests = await _mediator.Send(query);
                return Ok(requests);
            }
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpPost("recalculate-total-price")]
        public async Task<IActionResult> RecalculateTotalPrice([FromBody] UpdateTotalPriceCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpGet("requests-by-product/{productId}")]
        public async Task<IActionResult> GetRequestsByProductID(int productId)
        {
            var query = new GetRequestsByProductIDQuery(productId);
            var requests = await _mediator.Send(query);
            return Ok(requests);
        }
        [Authorize(Policy = "AllRolesCanAccess")]
        [HttpPost("recalculate-all-total-price")]
        public async Task<IActionResult> RecalculateAllRequestsTotalPrice()
        {
            var result = await _mediator.Send(new RecalculateAllRequestsTotalPriceCommand());
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        //[Authorize(Policy = "RequireSupLeaderRole")]
        //[HttpPost("adjust-dates-by-adding-7-hours")]
        //public async Task<IActionResult> AdjustDatesByAdding7Hours()
        //{
        //    var result = await _mediator.Send(new AdjustDatesByAdding7HoursCommand());
        //    if (result)
        //    {
        //        return Ok("Dates have been adjusted by adding 7 hours.");
        //    }
        //    return BadRequest("Failed to adjust dates.");
        //}
        [HttpPut("reset-approval-dates")]
        public async Task<IActionResult> ResetApprovalDates()
        {
            var result = await _mediator.Send(new ResetApprovalDatesCommand());
            if (!result)
            {
                return BadRequest("Failed to reset approval dates.");
            }
            return Ok("Approval dates reset successfully for all requests.");
        }



    }
}
