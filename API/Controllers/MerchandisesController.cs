using Application.Commands.Merchandises;
using Application.Dtos.Merchandises;
using Application.Queries.Merchandises;
using Domain.helpers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages port merchandise types and cargo categories.
    /// </summary>
    [Route("api/merchandises")]
    [ApiController]
    public class MerchandisesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Get all merchandises with pagination and filtering.</summary>
        /// <param name="dto">Filter parameters (name, type, pageNumber, pageSize).</param>
        /// <returns>Paginated list of merchandises.</returns>
        /// <response code="200">Returns paginated merchandises.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] MerchandisesQueryDto dto)
        {
            var query = new GetAllMerchandisesQuery(dto);
            var response = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(response);
        }

        /// <summary>Get a single merchandise by ID.</summary>
        /// <param name="id">The merchandise ID.</param>
        /// <returns>Merchandise details.</returns>
        /// <response code="200">Returns the merchandise.</response>
        /// <response code="404">Merchandise not found.</response>
        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var query = new GetMerchandiseQuery(id);
            var response = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(response);
        }

        /// <summary>Create a new merchandise type.</summary>
        /// <param name="dto">Merchandise data including name and type.</param>
        /// <returns>201 Created on success.</returns>
        /// <response code="201">Merchandise created successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="422">Validation failed.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateOne([FromBody] CreateMerchandiseDto dto)
        {
            try
            {
                var command = new CreateMerchandiseCommand(dto);
                var response = await _mediator.Send(command, HttpContext.RequestAborted);
                return Created();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>Update an existing merchandise. Requires Chef role.</summary>
        /// <param name="id">The merchandise ID.</param>
        /// <param name="dto">Updated merchandise data.</param>
        /// <response code="200">Updated successfully.</response>
        /// <response code="404">Merchandise not found.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateOne([FromRoute] int id, [FromBody] UpdateMerchandiseDto dto)
        {
            try
            {
                var command = new UpdateMerchandiseCommand(id, dto);
                var response = await _mediator.Send(command, HttpContext.RequestAborted);
                return Ok("updated!");
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>Delete a merchandise type. Requires Chef role.</summary>
        /// <param name="id">The merchandise ID.</param>
        /// <response code="200">Deleted successfully.</response>
        /// <response code="403">Forbidden — Chef role required.</response>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Chef")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteOne([FromRoute] int id)
        {
            try
            {
                var command = new DeleteMerchandiseCommand(id);
                var response = await _mediator.Send(command, HttpContext.RequestAborted);
                return Ok("deleted!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Data);
            }
        }
    }
}
