using MediatR;
using Office_supplies_management.DTOs.User;

namespace Office_supplies_management.Features.User.Queries
{
    public class GetUsersByDepartmentQuery : IRequest<List<UserDto>>
    {
        public int UserId { get; }

        public GetUsersByDepartmentQuery(int userId)
        {
            UserId = userId;
        }
    }
}
