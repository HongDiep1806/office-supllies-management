using MediatR;
using Office_supplies_management.DTOs.User;

namespace Office_supplies_management.Features.User.Queries
{
    public class GetDepartmentLeaderQuery : IRequest<UserDto>
    {
        public string Department { get; set; }
    }
}
