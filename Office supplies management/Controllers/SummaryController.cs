using MediatR;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.Summary;
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
    }
}
