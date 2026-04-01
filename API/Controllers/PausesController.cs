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
    /// <summary>
    /// Manages operational pauses during tally sessions.
    /// Pauses can be tied to a truck session or the overall tally sheet (e.g. weather, crane failure).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PausesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Get all pauses for a tally sheet session.</summary>
        /// <param name="tallySheetId">The tally sheet ID.</param>
        /// <returns>List of pauses recorded in the session.</returns>
        /// <response code="200">Returns list of pauses.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet("All/{tallySheetId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromRoute] int tallySheetId)
        {
            var query = new GetAllPausesQuery(tallySheetId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>Get a single pause by ID.</summary>
        /// <param name="Id">The pause ID.</param>
        /// <returns>Pause details including reason, start/end time and duration.</returns>
        /// <response code="200">Returns the pause.</response>
        /// <response code="404">Pause not found.</response>
        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOne([FromRoute] int Id)
        {
            var query = new GetPauseByIdQuery(Id);
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>Record a new pause. EndTime is null until the pause ends.</summary>
        /// <param name="dto">Pause data including reason, start time and optional truck/tally sheet reference.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Pause recorded successfully.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateOne([FromBody] CreatePauseDto dto)
        {
            var command = new CreatePauseCommand(dto);
            await _mediator.Send(command);
            return Created();
        }

        /// <summary>End an active pause by recording the end time.</summary>
        /// <param name="Id">The pause ID.</param>
        /// <param name="dto">End time data.</param>
        /// <response code="200">Pause ended successfully.</response>
        /// <response code="404">Pause not found.</response>
        [HttpPatch("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EndTime([FromBody] EndPauseDto dto, [FromRoute] int Id)
        {
            var command = new EndPauseCommand(Id, dto);
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>Delete a pause record.</summary>
        /// <param name="Id">The pause ID.</param>
        /// <response code="200">Deleted successfully.</response>
        /// <response code="404">Pause not found.</response>
        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOne([FromRoute] int Id)
        {
            var command = new DeletePauseCommand(Id);
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>Update pause details (reason, start time, notes).</summary>
        /// <param name="Id">The pause ID.</param>
        /// <param name="dto">Updated pause data.</param>
        /// <response code="204">Updated successfully.</response>
        /// <response code="404">Pause not found.</response>
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOne(int Id, [FromBody] UpdatePauseDto dto)
        {
            var command = new UpdatePauseCommand(Id, dto);
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : NoContent();
        }
    }
}
