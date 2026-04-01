using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetClient;
using Application.Dtos.TallySheetClient;
using Application.Queries.TallySheetClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages client assignments to tally sheets including real-time quantity tracking.
    /// Quantity updates use write-behind caching — changes are stored in Redis and synced to the database every 2 minutes via Hangfire.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TallySheetClientController(IMediator mediator) : ControllerBase
    {
        /// <summary>Get all client assignments for a tally sheet.</summary>
        /// <param name="tallySheetId">The tally sheet ID.</param>
        /// <returns>List of client assignments with quantities and units.</returns>
        /// <response code="200">Returns list of client assignments.</response>
        /// <response code="404">No clients found for this tally sheet.</response>
        [HttpGet("{tallySheetId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTallySheet(int tallySheetId)
        {
            var result = await mediator.Send(new GetAllByTallySheetIdQuery(tallySheetId));
            if (result is null || result.Count == 0) return NotFound();
            return Ok(result);
        }

        /// <summary>Get a specific client assignment by tally sheet and client ID.</summary>
        /// <param name="tallySheetId">The tally sheet ID.</param>
        /// <param name="clientId">The client ID.</param>
        /// <returns>Client assignment details.</returns>
        /// <response code="200">Returns the client assignment.</response>
        /// <response code="404">Assignment not found.</response>
        [HttpGet("{tallySheetId:int}/{clientId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int tallySheetId, int clientId)
        {
            var result = await mediator.Send(new GetByIdQuery(tallySheetId, clientId));
            if (result is null) return NotFound();
            return Ok(result);
        }

        /// <summary>Assign a client to a tally sheet. Returns 409 if already assigned.</summary>
        /// <param name="dto">Assignment data including tally sheet ID, client ID, quantity and unit.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Client assigned successfully.</response>
        /// <response code="409">Client already assigned to this tally sheet.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddClientToTallySheet([FromBody] AddClientToTallyDto dto)
        {
            var result = await mediator.Send(new CreateTallySheetClientCommand(dto));
            return Created();
        }

        /// <summary>
        /// Update merchandise quantity for a client in a tally session.
        /// Uses write-behind caching — quantity is stored in Redis immediately and synced to the database every 2 minutes.
        /// </summary>
        /// <param name="tallySheetId">The tally sheet ID.</param>
        /// <param name="clientId">The client ID.</param>
        /// <param name="dto">New quantity value.</param>
        /// <response code="200">Quantity queued for update.</response>
        /// <response code="404">Client assignment not found.</response>
        [HttpPatch("{tallySheetId:int}/{clientId:int}/quantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateQuantity(int tallySheetId, int clientId, [FromBody] UpdateQuantityDto dto)
        {
            var command = new UpdateQuantityCommand(tallySheetId, clientId, dto);
            var result = await mediator.Send(command);
            if (result is null) return NotFound();
            return Ok(new { message = "Quantity queued for update." });
        }

        /// <summary>Remove a client from a tally sheet.</summary>
        /// <param name="tallySheetId">The tally sheet ID.</param>
        /// <param name="clientId">The client ID.</param>
        /// <response code="204">Removed successfully.</response>
        /// <response code="404">Assignment not found.</response>
        [HttpDelete("{tallySheetId:int}/{clientId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMerchandise(int tallySheetId, int clientId)
        {
            var result = await mediator.Send(new DeleteTallySheetClient(tallySheetId, clientId));
            if (result != true) return NotFound();
            return NoContent();
        }
    }
}
