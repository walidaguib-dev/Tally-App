using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands.Teams;
using Application.Commands.Teams.Members;
using Application.Dtos.Teams;
using Application.Dtos.Teams.TeamMembers;
using Application.Queries.Teams;
using Application.Queries.Teams.Members;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages port tally teams and their members.
    /// Teams group tallymen under a supervisor for shift operations.
    /// Requires Chef role for all operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Chef")]
    public class TeamsController(IMediator _mediator) : ControllerBase
    {
        private readonly IMediator mediator = _mediator;

        /// <summary>Get all teams with their members and supervisor info.</summary>
        /// <returns>List of all teams.</returns>
        /// <response code="200">Returns list of teams.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        [HttpGet("get-teams")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetTeamsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>Get a single team by ID.</summary>
        /// <param name="Id">The team ID.</param>
        /// <returns>Team details including members.</returns>
        /// <response code="200">Returns the team.</response>
        /// <response code="404">Team not found.</response>
        [HttpGet("team/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int Id)
        {
            var query = new GetTeamQuery(Id);
            var result = await mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Create a new team. The authenticated Chef becomes the supervisor.</summary>
        /// <param name="dto">Team data including name and description.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Team created successfully.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost("create-team")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTeamDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new CreateTeamCommand(dto, userId!);
            var result = await mediator.Send(command);
            return Created();
        }

        /// <summary>Update team name or description.</summary>
        /// <param name="Id">The team ID.</param>
        /// <param name="dto">Updated team data.</param>
        /// <response code="204">Updated successfully.</response>
        /// <response code="404">Team not found.</response>
        [HttpPatch("update-team/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int Id, [FromBody] UpdateTeamDto dto)
        {
            var command = new UpdateTeamCommand(Id, dto);
            var result = await mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }

        /// <summary>Delete a team.</summary>
        /// <param name="id">The team ID.</param>
        /// <response code="204">Deleted successfully.</response>
        /// <response code="404">Team not found.</response>
        [HttpDelete("delete-team/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteTeamCommand(id);
            var result = await mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }

        /// <summary>Get all members of a team.</summary>
        /// <param name="teamId">The team ID.</param>
        /// <returns>List of team members with roles and join dates.</returns>
        /// <response code="200">Returns list of members.</response>
        [HttpGet("get-members/{teamId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMembersAsync(int teamId)
        {
            var query = new GetTeamMembersQuery(teamId);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>Add a tallyman to a team.</summary>
        /// <param name="dto">Member data: user ID, team ID and role (tallyman/head_tallyman).</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Member added successfully.</response>
        /// <response code="409">User already a member of this team.</response>
        [HttpPost("add-member")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddMemberAsync([FromBody] AddMemberDto dto)
        {
            var command = new CreateTeamMemberCommand(dto);
            var result = await mediator.Send(command);
            return Created();
        }

        /// <summary>Remove a member from a team.</summary>
        /// <param name="teamId">The team ID.</param>
        /// <param name="userId">The user ID to remove.</param>
        /// <response code="204">Member removed successfully.</response>
        /// <response code="404">Member not found in team.</response>
        [HttpDelete("remove-member/{teamId:int}/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAsync(int teamId, string userId)
        {
            var command = new RemoveTeamMemberCommand(teamId, userId);
            var result = await mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }
    }
}
