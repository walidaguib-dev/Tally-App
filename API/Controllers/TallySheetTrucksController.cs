using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetTrucks;
using Application.Dtos.TallySheetTrucks;
using Application.Queries.TallySheetTrucks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages truck assignments and sessions within tally sheets.
    /// Tracks start/end times for each truck working a tally session.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TallySheetTrucksController(IMediator _mediator) : ControllerBase
    {
        private readonly IMediator mediator = _mediator;

        /// <summary>Get all truck assignments for a tally sheet session.</summary>
        /// <param name="tallySheetId">The tally sheet ID.</param>
        /// <returns>List of truck sessions including start/end times.</returns>
        /// <response code="200">Returns list of truck sessions.</response>
        /// <response code="404">No trucks found for this tally sheet.</response>
        [HttpGet("{tallySheetId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTrucksByTallySessionId([FromRoute] int tallySheetId)
        {
            var query = new GetTallySheetTrucks(tallySheetId);
            var result = await mediator.Send(query);
            if (result is null || result.Count == 0) return NotFound();
            return Ok(result);
        }

        /// <summary>Assign a truck to a tally sheet session and record start time.</summary>
        /// <param name="dto">Assignment data: tally sheet ID, truck ID and start time.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Truck assigned successfully.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AssignTruck([FromBody] AssignTruckDto dto)
        {
            var command = new AssignTruckCommand(dto);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return Created();
        }

        /// <summary>Record the end time for a truck session.</summary>
        /// <param name="TallySheetId">The tally sheet ID.</param>
        /// <param name="TruckId">The truck ID.</param>
        /// <param name="dto">End time data.</param>
        /// <response code="200">End time recorded successfully.</response>
        /// <response code="404">Truck session not found.</response>
        [HttpPatch("EndTime/{TallySheetId:int}/{TruckId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EndTruckTime([FromRoute] int TallySheetId, [FromRoute] int TruckId, [FromBody] EndTruckTimeDto dto)
        {
            var command = new EndTruckTimeCommand(TallySheetId, TruckId, dto);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>Remove a truck assignment from a tally sheet.</summary>
        /// <param name="TallySheetId">The tally sheet ID.</param>
        /// <param name="TruckId">The truck ID.</param>
        /// <response code="200">Removed successfully.</response>
        /// <response code="404">Assignment not found.</response>
        [HttpDelete("Delete/{TallySheetId:int}/{TruckId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAssignedTruck(int TallySheetId, [FromRoute] int TruckId)
        {
            var command = new DeleteAssignedTruckCommand(TallySheetId, TruckId);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return Ok(result);
        }
    }
}
