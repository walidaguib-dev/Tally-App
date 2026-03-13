using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Pauses;
using Application.Dtos.Pauses;
using Application.Queries.Pauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PausesController(
        IMediator mediator
    ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("All/{tallySheetId:int}")]
        public async Task<IActionResult> GetAll([FromRoute] int tallySheetId)
        {
            var query = new GetAllPausesQuery(tallySheetId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetOne([FromRoute] int Id)
        {
            var query = new GetPauseByIdQuery(Id);
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] CreatePauseDto dto)
        {
            var command = new CreatePauseCommand(dto);
            await _mediator.Send(command);
            return Created();
        }

        [HttpPatch("{Id:int}")]
        public async Task<IActionResult> EndTime([FromBody] EndPauseDto dto, [FromRoute] int Id)
        {
            var command = new EndPauseCommand(Id, dto);
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteOne([FromRoute] int Id)
        {
            var command = new DeletePauseCommand(Id);
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateOne(int Id, [FromBody] UpdatePauseDto dto)
        {
            var command = new UpdatePauseCommand(Id, dto);
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : NoContent();
        }

    }
}