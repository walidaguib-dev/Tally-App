using Application.Commands.Trucks;
using Application.Dtos.Trucks;
using Application.Queries.Trucks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/trucks")]
    [ApiController]
    public class TrucksController(
        IMediator _mediator
        ) : ControllerBase
    {
        private readonly IMediator mediator = _mediator;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllTrucksQuery();
            var result = await mediator.Send(query, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOne([FromRoute] int id) {
            var query = new GetTruckQuery(id);
            var result = await mediator.Send(query, HttpContext.RequestAborted);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> CreateOne([FromBody] CreateTruckDto dto) {
            var command = new CreateTruckCommand(dto);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            if (result is null) return BadRequest("something went wrong!");
            return Created();
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> UpdateOne([FromBody] UpdatetTruckDto dto , [FromRoute] int id) {
            var command = new UpdateTruckCommand(id, dto);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return result is null ? NotFound() : Ok(result);

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> DeleteOne([FromRoute] int id) {
            var command = new DeleteTruckCommand(id);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
