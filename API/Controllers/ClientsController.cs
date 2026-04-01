using Application.Commands.Clients;
using Application.Dtos.Clients;
using Application.Queries.Clients;
using Domain.helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages port clients and their cargo assignments.
    /// </summary>
    [Route("api/clients")]
    [ApiController]
    public class ClientsController(IMediator _mediator) : ControllerBase
    {
        private readonly IMediator mediator = _mediator;

        /// <summary>Get all clients with pagination and filtering.</summary>
        /// <param name="queryDto">Pagination and filter parameters (name, pageNumber, pageSize, sortBy).</param>
        /// <returns>Paginated list of clients.</returns>
        /// <response code="200">Returns paginated clients.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] ClientsQueryDto queryDto)
        {
            var query = new GetAllClientsQuery(queryDto);
            var result = await mediator.Send(query, HttpContext.RequestAborted);
            return Ok(result);
        }

        /// <summary>Get a single client by ID.</summary>
        /// <param name="id">The client ID.</param>
        /// <returns>Client details including merchandise and bill of lading.</returns>
        /// <response code="200">Returns the client.</response>
        /// <response code="404">Client not found.</response>
        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var query = new GetClientQuery(id);
            var result = await mediator.Send(query, HttpContext.RequestAborted);
            if (result is null) return NotFound();
            return Ok(result);
        }

        /// <summary>Create a new client. Requires Chef role.</summary>
        /// <param name="dto">Client data including name, contact info, bill of lading and merchandise.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Client created successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateOne([FromBody] CreateClientDto dto)
        {
            var command = new CreateClientCommand(dto);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            if (result is null) return BadRequest("Unable to create client.");
            return Created();
        }

        /// <summary>Update an existing client. Requires Chef role.</summary>
        /// <param name="id">The client ID.</param>
        /// <param name="dto">Updated client data.</param>
        /// <response code="204">Updated successfully.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateOne([FromRoute] int id, [FromBody] UpdateClientDto dto)
        {
            var command = new UpdateClientCommand(id, dto);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return result is null ? NotFound() : NoContent();
        }

        /// <summary>Delete a client by ID. Requires Chef role.</summary>
        /// <param name="id">The client ID.</param>
        /// <response code="204">Deleted successfully.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteOne([FromRoute] int id)
        {
            var command = new DeleteClientCommand(id);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return result is null ? NotFound() : NoContent();
        }
    }
}
