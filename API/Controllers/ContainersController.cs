using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Containers;
using Application.Dtos.Containers;
using Application.Queries.Containers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Containers")]
    [Authorize]
    public class ContainersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ContainersFilterDto dto)
        {
            var query = new GetAllContainersQuery(dto);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("TallySession/{TallySessionId:int}")]
        public async Task<IActionResult> GetByTallySessionAsync(int TallySessionId)
        {
            var query = new GetContainersByTallySessionIdQuery(TallySessionId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{ContainerNumber}")]
        public async Task<IActionResult> GetOneAsync(string ContainerNumber)
        {
            var query = new GetContainerQuery(ContainerNumber);
            var result = await _mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateContainerDto containerDto)
        {
            var command = new CreateContainerCommand(containerDto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("{ContainerNumber}")]
        public async Task<IActionResult> UpdateAsync(
            string ContainerNumber,
            [FromBody] UpdateContainerDto dto
        )
        {
            var command = new UpdateContainerCommand(ContainerNumber, dto);
            var result = await _mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }

        [HttpDelete("{ContainerNumber}")]
        public async Task<IActionResult> DeleteAsync(string ContainerNumber)
        {
            var command = new DeleteContainerCommand(ContainerNumber);
            var result = await _mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }
    }
}
