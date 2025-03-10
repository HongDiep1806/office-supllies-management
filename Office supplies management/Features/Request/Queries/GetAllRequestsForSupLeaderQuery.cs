using MediatR;
using Office_supplies_management.DTOs.Request;

namespace Office_supplies_management.Features.Request.Queries
{
    public class GetAllRequestsForSupLeaderQuery: IRequest<List<RequestDto>>
    {
    }
}
