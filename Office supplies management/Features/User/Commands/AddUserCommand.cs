using MediatR;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Models;

namespace Office_supplies_management.Features.Request.Commands
{
    public class AddUserCommand : IRequest<UserDto>
    {
        public CreateUserDto createRequest {  get; set; }   
        public AddUserCommand(CreateUserDto createRequest)
        {
            this.createRequest = createRequest;
        }
    }
}
