using MediatR;
using Office_supplies_management.DTOs.Request;

namespace Office_supplies_management.Features.Request.Queries
{
    public class GetRequestByIDQuery:IRequest<RequestDto>
    {
        public int Id { get; set; }
        public GetRequestByIDQuery(int id)
        {
            Id = id;
        }
    }
}
