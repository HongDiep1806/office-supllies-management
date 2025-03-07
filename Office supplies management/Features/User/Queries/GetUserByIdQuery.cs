using MediatR;
using Office_supplies_management.DTOs.User;

namespace Office_supplies_management.Features.User.Queries
{
    public class GetUserByIdQuery: IRequest<UserDto>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
