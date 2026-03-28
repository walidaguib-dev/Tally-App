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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CarsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("all/{tallySessionId:int}")]
        public async Task<IActionResult> GetAll(int tallySessionId)
        {
            var query = new GetAllCarsByTallySeesionQuery(tallySessionId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetOne(int Id)
        {
            var query = new GetCarQuery(Id);
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] CreateCarDto dto)
        {
            var command = new CreateCarsCommand(dto);
            await _mediator.Send(command);
            return Created();
        }

        [HttpPatch("{Id:int}")]
        public async Task<IActionResult> UpdateOne([FromBody] UpdateCarDto dto, int Id)
        {
            var command = new UpdateCarsCommand(Id, dto);
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var command = new DeleteCarsCommand(Id);
            var result = await _mediator.Send(command);
            return result == null ? NotFound() : NoContent();
        }
    }
}
