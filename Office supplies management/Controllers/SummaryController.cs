using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Features.Summary.Commands;
using Office_supplies_management.Features.Summary.Queries;

namespace Office_supplies_management.Controllers
{

    [Route("/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SummaryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        //[Authorize(Policy = "RequireFinanceEmployee")]
        public async Task<IActionResult> Create([FromBody] CreateSummaryDto createSummaryDto)
        {
            var command = new AddSummaryCommand(createSummaryDto);
            var newSummary = await _mediator.Send(command);
            if (newSummary != null)
            {
                return Ok(newSummary);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSummariesQuery();
            var summaries = await _mediator.Send(query);
            return Ok(summaries);
        }

        [HttpPut("update")]
        //[Authorize(Policy = "RequireSupLeaderRole")]
        public async Task<IActionResult> UpdateSummary([FromBody] UpdateSummaryCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is bool success && success)
            {
                return Ok();
            }
            return BadRequest();
        }

        //[HttpPut("update-request-status")]
        //[Authorize(Policy = "RequireSupLeaderRole")]
        //public async Task<IActionResult> UpdateRequestStatus([FromBody] UpdateRequestStatusCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    if (result)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

        //[HttpGet]
        ////[Authorize(Policy = "RequireSupLeaderRole")]
        //public async Task<IActionResult> GetAllSummaries()
        //{
        //    var query = new GetAllSummariesQuery();
        //    var summaries = await _mediator.Send(query);
        //    if (summaries != null && summaries.Any())
        //    {
        //        return Ok(summaries);
        //    }
        //    return NotFound();
        //}

        [HttpGet("user/{userId}")]
        //[Authorize(Policy = "RequireFinanceEmployee")]
        //[Authorize(Policy = "RequireSupLeaderRole")]
        public async Task<IActionResult> GetSummariesByUserId(int userId)
        {
            var query = new GetSummariesByUserIdQuery(userId);
            var summaries = await _mediator.Send(query);
            if (summaries != null && summaries.Any())
            {
                return Ok(summaries);
            }
            return NotFound();
        }

        [HttpGet("{summaryId}")]
        //[Authorize(Policy = "RequireFinanceEmployee")]
        //[Authorize(Policy = "RequireSupLeaderRole")]
        public async Task<IActionResult> GetSummaryById(int summaryId)
        {
            var query = new GetSummaryByIdQuery(summaryId);
            var summary = await _mediator.Send(query);
            if (summary != null)
            {
                return Ok(summary);
            }
            return NotFound();
        }

        [HttpGet("report")]
        //[Authorize(Policy = "RequireFinanceEmployee")]
        public async Task<IActionResult> GetDepartmentUsageReport([FromQuery] string department, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetDepartmentUsageReportQuery { Department = department, StartDate = startDate, EndDate = endDate };
            var report = await _mediator.Send(query);
            if (report != 0)
            {
                return Ok(report);
            }
            return NotFound();
        }

        [HttpGet("summariesByDateRange")]
        //[Authorize(Policy = "RequireFinanceEmployee")]
        public async Task<IActionResult> GetSummariesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetSummariesByDateRangeQuery { StartDate = startDate, EndDate = endDate };
            var summaries = await _mediator.Send(query);
            if (summaries != null && summaries.Any())
            {
                return Ok(summaries);
            }
            return NotFound();
        }

        [HttpGet("{summaryId}/requests")]
        public async Task<ActionResult<List<RequestDto>>> GetRequestsBySummaryId(int summaryId)
        {
            var query = new GetRequestsBySummaryIdQuery(summaryId);
            var requests = await _mediator.Send(query);
            if (requests == null || requests.Count == 0)
            {
                return NotFound();
            }
            return Ok(requests);
        }
        [HttpGet("department-costs")]
        public async Task<IActionResult> GetDepartmentCosts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetDepartmentCostsQuery { StartDate = startDate, EndDate = endDate };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("approved-summaries-with-requests")]
        public async Task<IActionResult> GetApprovedSummariesWithRequests()
        {
            var query = new GetApprovedSummariesWithRequestsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        

        [HttpGet("summaries-with-requests")]
        public async Task<IActionResult> GetSummariesWithRequestsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetSummariesWithRequestsByDateRangeQuery(startDate, endDate);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


    }
}
