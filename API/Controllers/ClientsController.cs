using Application.Commands.Clients;
using Application.Dtos.Clients;
using Application.Queries.Clients;
using Domain.helpers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/clients")]
    [ApiController]

    public class ClientsController(
        IMediator _mediator
        ) : ControllerBase
    {
        private readonly IMediator mediator = _mediator;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllClientsQuery();
            var result = await mediator.Send(query, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOne([FromRoute] int id) {
            var query = new GetClientQuery(id);
            var result = await mediator.Send(query, HttpContext.RequestAborted);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> CreateOne([FromBody] CreateClientDto dto) {
           
                var command = new CreateClientCommand(dto);
                var result = await mediator.Send(command, HttpContext.RequestAborted);
                if (result is null) return BadRequest("Unable to create client.");
            return Created();

        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> UpdateOne([FromRoute] int id , [FromBody] UpdateClientDto dto) {
             var command = new UpdateClientCommand(id,dto);
                var result = await mediator.Send(command, HttpContext.RequestAborted);
            return result is null ? NotFound() : NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> DeleteOne([FromRoute] int id) {
            var command = new DeleteClientCommand(id);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return result is null ? NotFound() : NoContent();
        }

    }
}
