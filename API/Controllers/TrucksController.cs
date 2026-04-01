using Application.Commands.Trucks;
using Application.Dtos.Trucks;
using Application.Queries.Trucks;
using Domain.helpers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages port truck registry including plate numbers and load capacities.
    /// </summary>
    [Route("api/trucks")]
    [ApiController]
    public class TrucksController(IMediator _mediator) : ControllerBase
    {
        private readonly IMediator mediator = _mediator;

        /// <summary>Get all trucks with pagination and filtering.</summary>
        /// <param name="dto">Filter parameters (plate number, pageNumber, pageSize).</param>
        /// <returns>Paginated list of trucks.</returns>
        /// <response code="200">Returns paginated trucks.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] TrucksQueryDto dto)
        {
            var query = new GetAllTrucksQuery(dto);
            var result = await mediator.Send(query, HttpContext.RequestAborted);
            return Ok(result);
        }

        /// <summary>Get a single truck by ID.</summary>
        /// <param name="id">The truck ID.</param>
        /// <returns>Truck details including plate number and capacity.</returns>
        /// <response code="200">Returns the truck.</response>
        /// <response code="404">Truck not found.</response>
        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var query = new GetTruckQuery(id);
            var result = await mediator.Send(query, HttpContext.RequestAborted);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Register a new truck. Requires Chef role.</summary>
        /// <param name="dto">Truck data: plate number and capacity (tons).</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Truck registered successfully.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateOne([FromBody] CreateTruckDto dto)
        {
            try
            {
                var command = new CreateTruckCommand(dto);
                var result = await mediator.Send(command, HttpContext.RequestAborted);
                if (result is null) return BadRequest("something went wrong!");
                return Created();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
        }

        /// <summary>Update truck details. Requires Chef role.</summary>
        /// <param name="dto">Updated truck data.</param>
        /// <param name="id">The truck ID.</param>
        /// <response code="200">Updated successfully.</response>
        /// <response code="404">Truck not found.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateOne([FromBody] UpdateTruckDto dto, [FromRoute] int id)
        {
            var command = new UpdateTruckCommand(id, dto);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Delete a truck from the registry. Requires Chef role.</summary>
        /// <param name="id">The truck ID.</param>
        /// <response code="200">Deleted successfully.</response>
        /// <response code="404">Truck not found.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteOne([FromRoute] int id)
        {
            var command = new DeleteTruckCommand(id);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
