using Application.Commands.Tokens;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/tokens")]
    [ApiController]
    public class TokensController(
        IMediator mediator
        ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateToken([FromBody] Application.Dtos.Tokens.RefreshTokenRequest dto)
        {
            var command = new GenerateAccessTokenCommand(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
