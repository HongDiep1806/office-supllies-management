using MediatR;
using Office_supplies_management.DTOs.Summary;

namespace Office_supplies_management.Features.Summary.Commands
{
    public class AddSummaryCommand : IRequest<SummaryDto>
    {
        public CreateSummaryDto request { get; set; }
        public AddSummaryCommand(CreateSummaryDto request)
        {
            this.request = request;
        }
    }
}
