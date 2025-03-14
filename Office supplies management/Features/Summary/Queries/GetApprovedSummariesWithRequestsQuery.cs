using MediatR;
using Office_supplies_management.DTOs.Request;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetApprovedSummariesWithRequestsQuery : IRequest<Dictionary<int, List<RequestDto>>>
    {
    }
}
