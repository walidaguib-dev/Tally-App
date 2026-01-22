using Application.Commands.Users;
using Application.Dtos.Users;
using Application.Mappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(
        IMediator mediator
        ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            var command = new RegisterUserCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(RegisterUser), new { id = result.Id }, result.MapToUserDto());
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto dto)
        {
            var command = new SignInCommand(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new Application.Queries.Users.GetAllUsersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
