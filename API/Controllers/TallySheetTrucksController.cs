using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetTrucks;
using Application.Dtos.TallySheetTrucks;
using Application.Queries.TallySheetTrucks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TallySheetTrucksController(
        IMediator _mediator
    ) : ControllerBase
    {
        private readonly IMediator mediator = _mediator;

        [HttpGet("{tallySheetId:int}")]
        public async Task<IActionResult> GetTrucksByTallySessionId([FromRoute] int tallySheetId)
        {
            var query = new GetTallySheetTrucks(tallySheetId);
            var result = await mediator.Send(query);
            if (result is null || result.Count == 0) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AssignTruck([FromBody] AssignTruckDto dto)
        {
            var command = new AssignTruckCommand(dto);
            var result = await mediator.Send(command);
            return Created();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> EndTruckTime([FromRoute] int id, [FromBody] EndTruckTimeDto dto)
        {
            var command = new EndTruckTimeCommand(id, dto);
            var result = await mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}