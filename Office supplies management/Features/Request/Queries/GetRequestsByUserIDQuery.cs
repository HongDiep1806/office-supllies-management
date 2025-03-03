using MediatR;
using Office_supplies_management.DTOs.Request;

namespace Office_supplies_management.Features.Request.Queries
{
    public class GetRequestsByUserIDQuery:IRequest<List<RequestDto>>
    {
        public int UserId { get; set; }
        public GetRequestsByUserIDQuery(int userId)
        {
            UserId = userId;
        }
    }
}
