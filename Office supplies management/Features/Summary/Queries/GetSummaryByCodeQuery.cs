using MediatR;
using Office_supplies_management.DTOs.Summary;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetSummaryByCodeQuery : IRequest<SummaryDto>
    {
        public string SummaryCode { get; set; }
    }
}
