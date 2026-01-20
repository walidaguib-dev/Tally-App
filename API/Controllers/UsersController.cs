using Application.Mappers;
using MediatR;
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
        public async Task<IActionResult> RegisterUser([FromBody] Application.Dtos.Users.RegisterUserDto dto)
        {
            var command = new Application.Commands.RegisterUserCommand(dto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(RegisterUser), new { id = result.Id }, result.MapToUserDto());
        }
    }
}
