using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office_supplies_management.DTOs.User;
using Office_supplies_management.Features.Request.Commands;
using Office_supplies_management.Features.Request.Queries;
using Office_supplies_management.Features.User.Queries;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);
            return Ok(user);
        }

        [HttpGet("department")]
        [Authorize(Policy = "DepartmentQuery")]
        public async Task<IActionResult> GetUsersByDepartment()
        {
            // Log the raw token received
            var authHeader = Request.Headers["Authorization"].ToString();
            Console.WriteLine($"Received Authorization Header: {authHeader}");

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                  User.FindFirstValue(JwtRegisteredClaimNames.Sub);


            if (userIdClaim == null)
            {
                Console.WriteLine("JWT Sub claim is missing.");
                return Unauthorized("User ID claim is missing in token.");
            }

            if (!int.TryParse(userIdClaim, out var userId))
            {
                Console.WriteLine($"Invalid JWT Sub claim format: {userIdClaim}");
                return Unauthorized("Invalid User ID format in token.");
            }

            Console.WriteLine($"Extracted User ID from JWT: {userId}");

            var query = new GetUsersByDepartmentQuery(userId);
            var users = await _mediator.Send(query);
            return Ok(users);
        }
        [HttpGet("unique-departments")]
        public async Task<IActionResult> GetUniqueDepartments()
        {
            var query = new GetUniqueDepartmentsQuery();
            var departments = await _mediator.Send(query);
            if (departments != null && departments.Any())
            {
                return Ok(departments);
            }
            return NotFound();
        }




    }
}
