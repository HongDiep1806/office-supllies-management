using MediatR;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Features.User.Queries;

namespace Office_supplies_management.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUserQuery();
            var users = await _mediator.Send(query);
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto request)
        {
            var command = new AddUserCommand(request);
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var query = new GetUserByEmailQuery(email);
            var user = await _mediator.Send(query);
            return Ok(user);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById (int id)
        {
            var query = new GetUserByIdQuery(id);   
            var user = await _mediator.Send(query); 
            return Ok(user);
        }

    }
}
