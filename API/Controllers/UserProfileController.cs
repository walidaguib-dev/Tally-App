using Application.Commands.Users.Profiles;
using Application.Dtos.Users.Profiles;
using Application.Queries.Users.Profiles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users/profile")]
    [ApiController]
    [Authorize]
    public class UserProfileController(
        IMediator mediator
        ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserProfile([FromRoute] string userId)
        {
            var query = new GetUserProfileQuery(userId);
            var profileDto = await _mediator.Send(query);
            if (profileDto == null)
                return NotFound($"Profile for user {userId} not found.");

            return Ok(profileDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] CreateUserProfileDto dto)
        {
            var command = new CreateUserProfileCommand(dto);
            var createdProfile = await _mediator.Send(command);
            return Created();
        }

        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUserProfile([FromRoute] string userId, [FromBody] UpdateUserProfileDto dto)
        {
            var command = new UpdateUserProfileCommand(dto,userId);
            var updatedProfile = await _mediator.Send(command);
            if(updatedProfile == null)
                return NotFound($"Failed to update profile for user {userId}.");
            return Ok("profile updated!");
        }
    }
}
