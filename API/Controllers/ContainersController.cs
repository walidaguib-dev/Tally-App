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
    /// <summary>
    /// Manages shipping containers with ISO 6346 standard validation.
    /// </summary>
    [ApiController]
    [Route("api/Containers")]
    [Authorize]
    public class ContainersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Get all containers with pagination and filtering.</summary>
        /// <param name="dto">Filter and pagination parameters.</param>
        /// <returns>Paginated list of containers.</returns>
        /// <response code="200">Returns paginated containers.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] ContainersFilterDto dto)
        {
            var query = new GetAllContainersQuery(dto);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>Get all containers by tally session ID.</summary>
        /// <param name="TallySessionId">The tally sheet session ID.</param>
        /// <returns>List of containers in the session.</returns>
        /// <response code="200">Returns list of containers.</response>
        /// <response code="404">No containers found for session.</response>
        [HttpGet("TallySession/{TallySessionId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTallySessionAsync(int TallySessionId)
        {
            var query = new GetContainersByTallySessionIdQuery(TallySessionId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>Get a container by its ISO 6346 container number.</summary>
        /// <param name="ContainerNumber">ISO 6346 container number (e.g. MSCU1234567).</param>
        /// <returns>Container details.</returns>
        /// <response code="200">Returns the container.</response>
        /// <response code="404">Container not found.</response>
        [HttpGet("{ContainerNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneAsync(string ContainerNumber)
        {
            var query = new GetContainerQuery(ContainerNumber);
            var result = await _mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Register a new container. Container number must follow ISO 6346 format.</summary>
        /// <param name="containerDto">Container data. ContainerNumber must be 11 chars: 3 owner letters + U/J/Z + 6 digits + check digit.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Container registered successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="422">Validation failed — check container number format.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateContainerDto containerDto)
        {
            var command = new CreateContainerCommand(containerDto);
            var result = await _mediator.Send(command);
            return Created();
        }

        /// <summary>Update container status or details.</summary>
        /// <param name="ContainerNumber">ISO 6346 container number.</param>
        /// <param name="dto">Updated container data.</param>
        /// <response code="204">Updated successfully.</response>
        /// <response code="404">Container not found.</response>
        [HttpPatch("{ContainerNumber}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(string ContainerNumber, [FromBody] UpdateContainerDto dto)
        {
            var command = new UpdateContainerCommand(ContainerNumber, dto);
            var result = await _mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }

        /// <summary>Delete a container record.</summary>
        /// <param name="ContainerNumber">ISO 6346 container number.</param>
        /// <response code="204">Deleted successfully.</response>
        /// <response code="404">Container not found.</response>
        [HttpDelete("{ContainerNumber}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(string ContainerNumber)
        {
            var command = new DeleteContainerCommand(ContainerNumber);
            var result = await _mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }
    }
}
