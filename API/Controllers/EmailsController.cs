using Application.Commands.Emails;
using Application.Dtos.Mail;
using Application.Mappers;
using Domain.Contracts;
using Domain.helpers;
using FluentValidation;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailsController(
        IMediator mediator,
        IEmail emailsService
        ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IEmail _emailsService = emailsService;

        [HttpPost("send-email")]
        [Authorize]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailDto dto)
        {
            try
            {
                var response = await _mediator.Send(new SendEmailCommand(dto), HttpContext.RequestAborted);
                if (response is null)
                {
                    return Unauthorized();
                }
                return Ok(response.MapToEmailJson());
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = e.Errors.Select(e => e.ErrorMessage) });
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            try
            {
                var result = await _emailsService.ConfirmEmailAsync(userId, token);
                if (result == null)
                {
                    return BadRequest("user not found!");
                }
                return Redirect("https://fb.com");
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }


    }
}
