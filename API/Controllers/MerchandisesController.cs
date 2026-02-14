using Application.Commands.Merchandises;
using Application.Dtos.Merchandises;
using Application.Queries.Merchandises;
using Domain.helpers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/merchandises")]
    [ApiController]
    public class MerchandisesController(
        IMediator mediator
        ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllMerchandisesQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOne([FromRoute] int id) {
            var query = new GetMerchandiseQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        //[Authorize(Roles = "Chef")]
        public async Task<IActionResult> CreateOne([FromBody] CreateMerchandiseDto dto) {
            try
            {
                var command = new CreateMerchandiseCommand(dto);
                var response = await _mediator.Send(command);
                return Created();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> UpdateOne([FromRoute] int id,[FromBody] UpdateMerchandiseDto dto)
        {
            try
            {
                var command = new UpdateMerchandiseCommand(id,dto);
                var response = await _mediator.Send(command);
                return Ok("updated!");
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> DeleteOne([FromRoute] int id)
        {
            try
            {
                var command = new DeleteMerchandiseCommand(id);
                var response = await _mediator.Send(command);
                return Ok("deleted!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Data);
            }

        }
    }
}
