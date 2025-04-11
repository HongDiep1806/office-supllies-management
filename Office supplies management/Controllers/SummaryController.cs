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
        [Authorize(Policy = "RequireFinanceEmployee")]
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
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSummariesQuery();
            var summaries = await _mediator.Send(query);
            return Ok(summaries);
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
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
        [Authorize(Policy = "RequireSupLeaderRole")]
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
        [Authorize(Policy = "AllRolesCanAccess")]
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
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("report")]
        //[Authorize(Policy = "RequireFinanceEmployee")]
        public async Task<IActionResult> GetDepartmentUsageReport([FromQuery] string department, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetDepartmentUsageReportQuery { Department = department, StartDate = startDate, EndDate = endDate };
            var report = await _mediator.Send(query);
            return Ok(report);
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
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
        [Authorize(Policy = "RequireSupLeaderRole")]
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
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("department-costs")]
        public async Task<IActionResult> GetDepartmentCosts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetDepartmentCostsQuery { StartDate = startDate, EndDate = endDate };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("approved-summaries-with-requests")]
        public async Task<IActionResult> GetApprovedSummariesWithRequests()
        {
            var query = new GetApprovedSummariesWithRequestsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("summaries-with-requests-date-range")]
        public async Task<IActionResult> GetSummariesWithRequestsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetSummariesWithRequestsByDateRangeQuery(startDate, endDate);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var query = new GetNumberOfTotalSummariesQuery();
            var result = await _mediator.Send(query); 
            return Ok(result+1);
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpPut("update-approval")]
        public async Task<IActionResult> UpdateSummaryApproval([FromBody] UpdateSummaryApprovalCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Summary approval status updated successfully.");
            }
            return BadRequest("Failed to update summary approval status.");
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("get-by-code")]
        public async Task<IActionResult> GetSummaryByCode([FromQuery] string summaryCode)
        {
            var query = new GetSummaryByCodeQuery { SummaryCode = summaryCode };
            var summary = await _mediator.Send(query);
            if (summary == null)
            {
                return NotFound("Summary not found.");
            }
            return Ok(summary);
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpPost("recalculate-total-price")]
        public async Task<IActionResult> RecalculateAllSummariesTotalPrice()
        {
            var result = await _mediator.Send(new RecalculateAllSummariesTotalPriceCommand());
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpPost("set-update-date-to-created-date")]
        public async Task<IActionResult> SetUpdateDateToCreatedDate()
        {
            var result = await _mediator.Send(new SetUpdateDateToCreatedDateCommand());
            if (result)
            {
                return Ok("UpdateDate has been set to CreatedDate for all summaries.");
            }
            return BadRequest("Failed to update UpdateDate.");
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("product-count")]
        public async Task<IActionResult> GetProductCountForApprovedSummaries([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetProductCountForApprovedSummariesQuery(startDate, endDate);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("export-product-report")]
        public async Task<IActionResult> ExportProductReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GenerateProductReportExcelQuery(startDate, endDate);
            var excelFile = await _mediator.Send(query);

            // Generate the dynamic file name
            var fileName = $"report-{startDate:yyyy-MM-dd}-{endDate:yyyy-MM-dd}.xlsx";

            // Return the file with the dynamic name
            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [Authorize(Policy = "RequireSupLeaderRole")]
        [HttpGet("export-summary-detail")]
        public async Task<IActionResult> ExportSummaryDetail([FromQuery] int summaryId)
        {
            var query = new GenerateSummaryDetailExcelQuery(summaryId);
            var excelFile = await _mediator.Send(query);

            var fileName = $"summary-detail-{summaryId}.xlsx";
            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}
