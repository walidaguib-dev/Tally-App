using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Observations;
using Application.Dtos.Observations;
using Application.Queries.Observations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ObservationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("all/{tallySheetId:int}")]
        public async Task<IActionResult> GetAll(int tallySheetId)
        {
            var query = new GetAllObservationByTallyIdQuery(tallySheetId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetOne(int Id)
        {
            var query = new GetObservationByIdQuery(Id);
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] CreateObservationDto dto)
        {
            var command = new CreateObservationCommand(dto);
            var result = await _mediator.Send(command);
            return Created();
        }

        [HttpPatch("{Id:int}")]
        public async Task<IActionResult> UpdateOne(int Id, [FromBody] UpdateObservationDto dto)
        {
            var command = new UpdateObservationCommand(Id, dto);
            var result = await _mediator.Send(command);
            return result is null ? NotFound() : Ok("Updated!");
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteOne(int Id)
        {
            var command = new DeleteObservationCommand(Id);
            var result = await _mediator.Send(command);
            return result is null ? NotFound() : NoContent();
        }
    }
}
