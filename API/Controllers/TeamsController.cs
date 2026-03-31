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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Chef")]
    public class TeamsController(IMediator _mediator) : ControllerBase
    {
        private readonly IMediator mediator = _mediator;

        [HttpGet("get-teams")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetTeamsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("team/{Id:int}")]
        public async Task<IActionResult> GetAsync(int Id)
        {
            var query = new GetTeamQuery(Id);
            var result = await mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("create-team")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTeamDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new CreateTeamCommand(dto, userId!);
            var result = await mediator.Send(command);
            return Created();
        }

        [HttpPatch("update-team/{Id:int}")]
        public async Task<IActionResult> UpdateAsync(int Id, [FromBody] UpdateTeamDto dto)
        {
            var command = new UpdateTeamCommand(Id, dto);
            var result = await mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }

        [HttpDelete("delete-team/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteTeamCommand(id);
            var result = await mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }

        [HttpGet("get-members/{teamId:int}")]
        public async Task<IActionResult> GetMembersAsync(int teamId)
        {
            var query = new GetTeamMembersQuery(teamId);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("add-member")]
        public async Task<IActionResult> AddMemberAsync([FromBody] AddMemberDto dto)
        {
            var command = new CreateTeamMemberCommand(dto);
            var result = await mediator.Send(command);
            return Created();
        }

        [HttpDelete("remove-member/{teamId:int}/{userId}")]
        public async Task<IActionResult> RemoveAsync(int teamId, string userId)
        {
            var command = new RemoveTeamMemberCommand(teamId, userId);
            var result = await mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }
    }
}
