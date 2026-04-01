using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Cars;
using Application.Dtos.Cars;
using Application.Queries.Cars;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages vehicles (cars and buses) recorded during tally sessions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CarsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Get all vehicles by tally session ID.</summary>
        /// <param name="tallySessionId">The tally sheet session ID.</param>
        /// <returns>List of vehicles recorded in the session.</returns>
        /// <response code="200">Returns list of vehicles.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet("all/{tallySessionId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll(int tallySessionId)
        {
            var query = new GetAllCarsByTallySeesionQuery(tallySessionId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>Get a single vehicle by ID.</summary>
        /// <param name="Id">The vehicle ID.</param>
        /// <returns>Vehicle details.</returns>
        /// <response code="200">Returns the vehicle.</response>
        /// <response code="404">Vehicle not found.</response>
        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOne(int Id)
        {
            var query = new GetCarQuery(Id);
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>Record a new vehicle in a tally session.</summary>
        /// <param name="dto">Vehicle creation data including VIN, brand, type and status.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Vehicle recorded successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateOne([FromBody] CreateCarDto dto)
        {
            var command = new CreateCarsCommand(dto);
            await _mediator.Send(command);
            return Created();
        }

        /// <summary>Update an existing vehicle record.</summary>
        /// <param name="dto">Updated vehicle data.</param>
        /// <param name="Id">The vehicle ID.</param>
        /// <response code="204">Updated successfully.</response>
        /// <response code="404">Vehicle not found.</response>
        [HttpPatch("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOne([FromBody] UpdateCarDto dto, int Id)
        {
            var command = new UpdateCarsCommand(Id, dto);
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : NoContent();
        }

        /// <summary>Delete a vehicle record.</summary>
        /// <param name="Id">The vehicle ID.</param>
        /// <response code="204">Deleted successfully.</response>
        /// <response code="404">Vehicle not found.</response>
        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int Id)
        {
            var command = new DeleteCarsCommand(Id);
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : NoContent();
        }
    }
}
