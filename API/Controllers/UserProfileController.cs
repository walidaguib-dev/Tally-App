using Application.Commands.Users.Profiles;
using Application.Dtos.Users.Profiles;
using Application.Queries.Users.Profiles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages user profile information including name, bio and avatar.
    /// </summary>
    [Route("api/users/profile")]
    [ApiController]
    [Authorize]
    public class UserProfileController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Get a user profile by user ID.</summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>User profile including first name, last name, bio and avatar URL.</returns>
        /// <response code="200">Returns the user profile.</response>
        /// <response code="404">Profile not found.</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserProfile([FromRoute] string userId)
        {
            var query = new GetUserProfileQuery(userId);
            var profileDto = await _mediator.Send(query, HttpContext.RequestAborted);
            if (profileDto == null) return NotFound($"Profile for user {userId} not found.");
            return Ok(profileDto);
        }

        /// <summary>Create a profile for a user.</summary>
        /// <param name="dto">Profile data: first name, last name and optional bio.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Profile created successfully.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateProfile([FromBody] CreateUserProfileDto dto)
        {
            var command = new CreateUserProfileCommand(dto);
            var createdProfile = await _mediator.Send(command, HttpContext.RequestAborted);
            return Created();
        }

        /// <summary>Update a user profile.</summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="dto">Updated profile data.</param>
        /// <response code="200">Updated successfully.</response>
        /// <response code="404">Profile not found.</response>
        [HttpPatch("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserProfile([FromRoute] string userId, [FromBody] UpdateUserProfileDto dto)
        {
            var command = new UpdateUserProfileCommand(dto, userId);
            var updatedProfile = await _mediator.Send(command, HttpContext.RequestAborted);
            if (updatedProfile == null) return NotFound($"Failed to update profile for user {userId}.");
            return Ok("profile updated!");
        }
    }
}
