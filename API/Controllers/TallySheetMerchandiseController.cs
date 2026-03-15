using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetMerchandise;
using Application.Dtos.TallySheetMerchandise;
using Application.Queries.TallySheetMerchandises;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TallySheetMerchandiseController(
    IMediator mediator
) : ControllerBase
    {
        [HttpGet("{tallySheetId:int}")]
        public async Task<IActionResult> GetByTallySheet(int tallySheetId)
        {
            var result = await mediator.Send(new GetAllByTallySheetIdQuery(tallySheetId));
            if (result is null || result.Count == 0) return NotFound();
            return Ok(result);
        }

        [HttpGet("{tallySheetId:int}/{merchandiseId:int}")]
        public async Task<IActionResult> GetById(int tallySheetId, int merchandiseId)
        {
            var result = await mediator.Send(new GetByIdQuery(tallySheetId, merchandiseId));
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddMerchandise([FromBody] AddMerchandiseToTallyDto dto)
        {
            var result = await mediator.Send(new CreateTallySheetMerchandiseCommand(dto));
            return CreatedAtAction(nameof(GetById), new { tallySheetId = result.TallySheetId, merchandiseId = result.MerchandiseId }, result);
        }

        [HttpPatch("{tallySheetId:int}/{merchandiseId:int}/quantity")]
        public async Task<IActionResult> UpdateQuantity(int tallySheetId, int merchandiseId, [FromBody] UpdateQuantityDto dto)
        {
            var command = new UpdateQuantityCommand(tallySheetId, merchandiseId, dto);
            var result = await mediator.Send(command);
            if (result is null) return NotFound();
            return Ok(new { message = "Quantity queued for update." });
        }

        [HttpDelete("{tallySheetId:int}/{merchandiseId:int}")]
        public async Task<IActionResult> DeleteMerchandise(int tallySheetId, int merchandiseId)
        {
            var result = await mediator.Send(new DeleteTallySheetMerchandise(tallySheetId, merchandiseId));
            if (result != true) return NotFound();
            return NoContent();
        }
    }
}