using Application.Commands.Tokens;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Handles JWT token operations including refresh token rotation.
    /// </summary>
    [Route("api/tokens")]
    [ApiController]
    public class TokensController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Generate a new access token using a valid refresh token.</summary>
        /// <param name="dto">Refresh token request containing the current refresh token.</param>
        /// <returns>New JWT access token and refresh token pair.</returns>
        /// <response code="200">Returns new access token and refresh token.</response>
        /// <response code="401">Refresh token is invalid, expired or revoked.</response>
        [HttpPost("generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GenerateToken([FromBody] Application.Dtos.Tokens.RefreshTokenRequest dto)
        {
            var command = new GenerateAccessTokenCommand(dto);
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            return Ok(result);
        }
    }
}
