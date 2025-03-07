using MediatR;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Features.AuthenticateUser.Commands;


namespace Office_supplies_management.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var token = await _mediator.Send(new AuthenticateUserCommand(request));
            if (!string.IsNullOrEmpty(token))
            {
                return Ok(new { token });
            }
            return Unauthorized("Invalid credentials");
        }      
    }

    
}
