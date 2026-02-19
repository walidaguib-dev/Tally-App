using Application.Mappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/ships")]
    [ApiController]

    public class ShipsController(
        IMediator mediator
        ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllShips()
        {
            var query = new Application.Queries.Ships.GetAllShipsQuery();
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetShipById(int id)
        {
            var query = new Application.Queries.Ships.GetShipQuery(id);
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> CreateShip([FromBody] Application.Dtos.Ships.CreateShipDto dto)
        {
            var command = new Application.Commands.ships.CreateShipCommand(dto);
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            return Created();
        }

        [HttpPatch("update/{id:int}")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> UpdateShip(int id, [FromBody] Application.Dtos.Ships.UpdateShipDto dto)
        {
            var command = new Application.Commands.ships.UpdateShipCommand(id, dto);
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result.MapToJson());
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new Application.Commands.ships.DeleteShipCommand(id);
            var result = await _mediator.Send(command, HttpContext.RequestAborted);
            if (result == null)
            {
                return NotFound();
            }
            return Ok($"Ship {result.Name} is deleted!");
        }
    }
}
