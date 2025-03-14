using MediatR;
using Office_supplies_management.DTOs.Request;

namespace Office_supplies_management.Queries
{
    public class GetCollectedRequestsQuery : IRequest<List<RequestDto>>
    {
    }
}
