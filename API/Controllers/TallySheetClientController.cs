using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetClient;
using Application.Dtos.TallySheetClient;
using Application.Queries.TallySheetClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TallySheetClientController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{tallySheetId:int}")]
        public async Task<IActionResult> GetByTallySheet(int tallySheetId)
        {
            var result = await mediator.Send(new GetAllByTallySheetIdQuery(tallySheetId));
            if (result is null || result.Count == 0)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{tallySheetId:int}/{clientId:int}")]
        public async Task<IActionResult> GetById(int tallySheetId, int clientId)
        {
            var result = await mediator.Send(new GetByIdQuery(tallySheetId, clientId));
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddClientToTallySheet([FromBody] AddClientToTallyDto dto)
        {
            var result = await mediator.Send(new CreateTallySheetClientCommand(dto));
            return Created();
        }

        [HttpPatch("{tallySheetId:int}/{clientId:int}/quantity")]
        public async Task<IActionResult> UpdateQuantity(
            int tallySheetId,
            int clientId,
            [FromBody] UpdateQuantityDto dto
        )
        {
            var command = new UpdateQuantityCommand(tallySheetId, clientId, dto);
            var result = await mediator.Send(command);
            if (result is null)
                return NotFound();
            return Ok(new { message = "Quantity queued for update." });
        }

        [HttpDelete("{tallySheetId:int}/{clientId:int}")]
        public async Task<IActionResult> DeleteMerchandise(int tallySheetId, int clientId)
        {
            var result = await mediator.Send(new DeleteTallySheetClient(tallySheetId, clientId));
            if (result != true)
                return NotFound();
            return NoContent();
        }
    }
}
