using MediatR;
using Office_supplies_management.DTOs.User;

namespace Office_supplies_management.Features.Request.Queries
{
    public class GetAllUserQuery:IRequest<List<UserDto>>
    {
    }
}
