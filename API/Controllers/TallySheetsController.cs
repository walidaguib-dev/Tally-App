using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dtos.TallySheets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TallySheetsController(
        IMediator mediator
    ) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _mediator.Send(new Application.Queries.TallySheets.GetTallySheetQuery(id));
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _mediator.Send(new Application.Queries.TallySheets.GetAllTallySheetsQuery());
            return Ok(result);

        }

        [HttpGet("ByShip/{shipId:int}")]
        public async Task<ActionResult> GetByShip(int shipId)
        {
            var result = await _mediator.Send(new Application.Queries.TallySheets.GetAllTallySheetsByShipQuery(shipId));
            if (result is null || result.Count == 0) return NotFound("No tally sheets found for this ship.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTallySheetDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new Application.Commands.TallySheets.CreateTallySheetCommand(dto, userId!);
            await _mediator.Send(command);
            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateTallySheetDto dto)
        {
            var command = new Application.Commands.TallySheets.UpdateTallySheetCommand(id, dto);
            var result = await _mediator.Send(command);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new Application.Commands.TallySheets.DeleteTallySheetCommand(id);
            var result = await _mediator.Send(command);
            if (result != true) return NotFound();
            return NoContent();
        }
    }
}