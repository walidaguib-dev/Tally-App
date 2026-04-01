using Application.Dtos.Ships;
using Application.Mappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages vessel registry including IMO numbers and tally sheet assignments.
    /// </summary>
    [Route("api/ships")]
    [ApiController]
    public class ShipsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Get all ships with pagination and filtering.</summary>
        /// <param name="queryDto">Filter parameters (name, IMO number, pageNumber, pageSize).</param>
        /// <returns>Paginated list of ships.</returns>
        /// <response code="200">Returns paginated ships.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllShips([FromQuery] ShipsQueryDto queryDto)
        {
            var query = new Application.Queries.Ships.GetAllShipsQuery(queryDto);
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(result);
        }

        /// <summary>Get a ship by ID.</summary>
        /// <param name="id">The ship ID.</param>
        /// <returns>Ship details including IMO number.</returns>
        /// <response code="200">Returns the ship.</response>
        /// <response code="404">Ship not found.</response>
        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShipById(int id)
        {
            var query = new Application.Queries.Ships.GetShipQuery(id);
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>Register a new ship. Requires Chef role.</summary>
        /// <param name="dto">Ship data including name and IMO number.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Ship registered successfully.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateShip([FromBody] CreateShipDto dto)
        {
            var command = new Application.Commands.ships.CreateShipCommand(dto);
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            return Created();
        }

        /// <summary>Update ship details. Requires Chef role.</summary>
        /// <param name="id">The ship ID.</param>
        /// <param name="dto">Updated ship data.</param>
        /// <response code="200">Updated successfully.</response>
        /// <response code="404">Ship not found.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        [HttpPatch("update/{id:int}")]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateShip(int id, [FromBody] UpdateShipDto dto)
        {
            var command = new Application.Commands.ships.UpdateShipCommand(id, dto);
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok(result.MapToJson());
        }

        /// <summary>Delete a ship. Requires Chef role.</summary>
        /// <param name="id">The ship ID.</param>
        /// <response code="200">Deleted successfully.</response>
        /// <response code="404">Ship not found.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new Application.Commands.ships.DeleteShipCommand(id);
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok($"Ship {result.Name} is deleted!");
        }
    }
}
