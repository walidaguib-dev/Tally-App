using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetTrucks;
using Application.Dtos.TallySheetTrucks;
using Application.Queries.TallySheetTrucks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return Created();
        }

        [HttpPatch("EndTime/{TallySheetId:int}/{TruckId:int}")]
        public async Task<IActionResult> EndTruckTime([FromRoute] int TallySheetId, [FromRoute] int TruckId, [FromBody] EndTruckTimeDto dto)
        {
            var command = new EndTruckTimeCommand(TallySheetId, TruckId, dto);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("Delete/{TallySheetId:int}/{TruckId:int}")]
        public async Task<IActionResult> DeleteAssignedTruck(int TallySheetId, [FromRoute] int TruckId)
        {
            var command = new DeleteAssignedTruckCommand(TallySheetId, TruckId);
            var result = await mediator.Send(command, HttpContext.RequestAborted);
            return Ok(result);
        }
    }
}