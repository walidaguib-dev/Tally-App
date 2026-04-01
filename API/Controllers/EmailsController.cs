using Application.Commands.Emails;
using Application.Dtos.Mail;
using Application.Mappers;
using Domain.Contracts;
using Domain.helpers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Handles email operations including verification and password reset.
    /// </summary>
    [Route("api/emails")]
    [ApiController]
    public class EmailsController(IMediator mediator, IEmail emailsService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IEmail _emailsService = emailsService;

        /// <summary>Send a verification or notification email.</summary>
        /// <param name="dto">Email details including recipient and purpose.</param>
        /// <returns>Email send result.</returns>
        /// <response code="200">Email sent successfully.</response>
        /// <response code="401">Unauthorized or user not found.</response>
        /// <response code="400">Validation error or send failure.</response>
        [HttpPost("send-email")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailDto dto)
        {
            try
            {
                var response = await _mediator.Send(new SendEmailCommand(dto), HttpContext.RequestAborted);
                if (response is null) return Unauthorized();
                return Ok(response.MapToEmailJson());
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

        /// <summary>Confirm a user email address via token link.</summary>
        /// <param name="userId">The user ID from the confirmation link.</param>
        /// <param name="token">The confirmation token from the email link.</param>
        /// <returns>Redirects to the app on success.</returns>
        /// <response code="302">Email confirmed — redirecting.</response>
        /// <response code="400">Invalid token or user not found.</response>
        [HttpGet("confirm-email")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            try
            {
                var result = await _emailsService.ConfirmEmailAsync(userId, token);
                if (result == null) return BadRequest("user not found!");
                return Redirect("https://fb.com");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
