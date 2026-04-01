using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dtos.TallySheets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Core tally sheet management — the primary entity of the port tally workflow.
    /// Each tally sheet represents one shift session for a vessel at a specific zone.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TallySheetsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Get a single tally sheet by ID.</summary>
        /// <param name="id">The tally sheet ID.</param>
        /// <returns>Tally sheet details.</returns>
        /// <response code="200">Returns the tally sheet.</response>
        /// <response code="404">Tally sheet not found.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _mediator.Send(new Application.Queries.TallySheets.GetTallySheetQuery(id));
            if (result is null) return NotFound();
            return Ok(result);
        }

        /// <summary>Get all tally sheets with filtering and pagination.</summary>
        /// <param name="queryDto">Filter parameters: shipName, shift (morning/afternoon/night), zone, date, pageNumber, pageSize.</param>
        /// <returns>Paginated list of tally sheets.</returns>
        /// <response code="200">Returns paginated tally sheets.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll([FromQuery] TallySheetsQueryDto queryDto)
        {
            var result = await _mediator.Send(new Application.Queries.TallySheets.GetAllTallySheetsQuery(queryDto));
            return Ok(result);
        }

        /// <summary>Get all tally sheets for a specific ship.</summary>
        /// <param name="shipId">The ship ID.</param>
        /// <returns>List of tally sheets for the ship.</returns>
        /// <response code="200">Returns tally sheets for the ship.</response>
        /// <response code="404">No tally sheets found for this ship.</response>
        [HttpGet("ByShip/{shipId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByShip(int shipId)
        {
            var result = await _mediator.Send(new Application.Queries.TallySheets.GetAllTallySheetsByShipQuery(shipId));
            if (result is null || result.Count == 0) return NotFound("No tally sheets found for this ship.");
            return Ok(result);
        }

        /// <summary>Create a new tally sheet. The authenticated user is recorded as the tallyman.</summary>
        /// <param name="dto">Tally sheet data: date, shift (morning/afternoon/night), zone, ship ID, teams count.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Tally sheet created successfully.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Create([FromBody] CreateTallySheetDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new Application.Commands.TallySheets.CreateTallySheetCommand(dto, userId!);
            await _mediator.Send(command);
            return Created();
        }

        /// <summary>Update an existing tally sheet.</summary>
        /// <param name="id">The tally sheet ID.</param>
        /// <param name="dto">Updated tally sheet data.</param>
        /// <response code="200">Updated successfully.</response>
        /// <response code="404">Tally sheet not found.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateTallySheetDto dto)
        {
            var command = new Application.Commands.TallySheets.UpdateTallySheetCommand(id, dto);
            var result = await _mediator.Send(command);
            if (result is null) return NotFound();
            return Ok(result);
        }

        /// <summary>Delete a tally sheet.</summary>
        /// <param name="id">The tally sheet ID.</param>
        /// <response code="204">Deleted successfully.</response>
        /// <response code="404">Tally sheet not found.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new Application.Commands.TallySheets.DeleteTallySheetCommand(id);
            var result = await _mediator.Send(command);
            if (result != true) return NotFound();
            return NoContent();
        }
    }
}
