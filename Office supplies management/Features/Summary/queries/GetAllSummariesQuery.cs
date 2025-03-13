using MediatR;
using Office_supplies_management.DTOs.Summary;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetAllSummariesQuery : IRequest<List<SummaryDto>>
    {
        public GetAllSummariesQuery()
        {
            // Constructor logic if needed
        }
    }
}
