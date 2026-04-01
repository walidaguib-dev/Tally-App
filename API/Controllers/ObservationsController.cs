using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Observations;
using Application.Dtos.Observations;
using Application.Queries.Observations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages operational observations tied to tally sessions, clients, or trucks.
    /// Observations record real-time port incidents: damage, weather, congestion, quantity mismatches.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ObservationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Get all observations for a tally sheet session.</summary>
        /// <param name="tallySheetId">The tally sheet ID.</param>
        /// <returns>List of observations for the session.</returns>
        /// <response code="200">Returns list of observations.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet("all/{tallySheetId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll(int tallySheetId)
        {
            var query = new GetAllObservationByTallyIdQuery(tallySheetId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>Get a single observation by ID.</summary>
        /// <param name="Id">The observation ID.</param>
        /// <returns>Observation details.</returns>
        /// <response code="200">Returns the observation.</response>
        /// <response code="404">Observation not found.</response>
        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOne(int Id)
        {
            var query = new GetObservationByIdQuery(Id);
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>Record a new observation. Can be tied to a tally sheet, client, or truck — not both client and truck simultaneously.</summary>
        /// <param name="dto">Observation data. TallySheetId is required. ClientId and TruckId are optional but mutually exclusive.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Observation recorded successfully.</response>
        /// <response code="400">Invalid request — cannot target both client and truck.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateOne([FromBody] CreateObservationDto dto)
        {
            var command = new CreateObservationCommand(dto);
            var result = await _mediator.Send(command);
            return Created();
        }

        /// <summary>Update an existing observation.</summary>
        /// <param name="Id">The observation ID.</param>
        /// <param name="dto">Updated observation data.</param>
        /// <response code="200">Updated successfully.</response>
        /// <response code="404">Observation not found.</response>
        [HttpPatch("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOne(int Id, [FromBody] UpdateObservationDto dto)
        {
            var command = new UpdateObservationCommand(Id, dto);
            var result = await _mediator.Send(command);
            return result is null ? NotFound() : Ok("Updated!");
        }

        /// <summary>Delete an observation.</summary>
        /// <param name="Id">The observation ID.</param>
        /// <response code="204">Deleted successfully.</response>
        /// <response code="404">Observation not found.</response>
        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOne(int Id)
        {
            var command = new DeleteObservationCommand(Id);
            var result = await _mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }
    }
}
