using Application.Commands.Users;
using Application.Dtos.Users;
using Application.Mappers;
using Domain.helpers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers
{
    /// <summary>
    /// Handles user authentication and account management.
    /// Includes registration, sign-in, password reset and user listing.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Register a new user account.</summary>
        /// <param name="dto">Registration data: username, email and password.</param>
        /// <returns>201 Created on success. Sends email verification automatically.</returns>
        /// <response code="201">User registered successfully.</response>
        /// <response code="400">Validation error or registration failed.</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            try
            {
                var command = new RegisterUserCommand(dto);
                var result = await _mediator.Send(command, HttpContext.RequestAborted);
                return Created();
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = [.. e.Errors.Select(err => err.ErrorMessage)] });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>Sign in with email and password. Returns JWT access token and refresh token.</summary>
        /// <param name="dto">Sign-in credentials: email and password.</param>
        /// <returns>JWT access token and refresh token on success.</returns>
        /// <response code="200">Returns JWT access token and refresh token.</response>
        /// <response code="400">Invalid credentials or validation error.</response>
        [HttpPost("signin")]
        [EnableRateLimiting("Auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromBody] SignInDto dto)
        {
            try
            {
                var command = new SignInCommand(dto);
                var result = await _mediator.Send(command, HttpContext.RequestAborted);
                return Ok(result);
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = [.. e.Errors.Select(err => err.ErrorMessage)] });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>Get all registered users. Requires authentication.</summary>
        /// <returns>List of all users.</returns>
        /// <response code="200">Returns list of users.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet]
        [Authorize]
        [EnableRateLimiting("Default")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new Application.Queries.Users.GetAllUsersQuery();
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(result);
        }

        /// <summary>Reset password for the authenticated user.</summary>
        /// <param name="dto">Password reset data: current password and new password.</param>
        /// <returns>Updated user info on success.</returns>
        /// <response code="200">Password reset successfully.</response>
        /// <response code="404">User not found.</response>
        /// <response code="400">Validation error.</response>
        [HttpPost("reset-password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDto dto)
        {
            try
            {
                var command = new PasswordResetCommand(dto);
                var result = await _mediator.Send(command, HttpContext.RequestAborted);
                if (result == null) return NotFound(new { message = "User not found" });
                return Ok(result.MapToUserDto());
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = [.. e.Errors.Select(err => err.ErrorMessage)] });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>Send a password reset email to the user.</summary>
        /// <param name="dto">Email address to send the reset link to.</param>
        /// <returns>Confirmation of email sent.</returns>
        /// <response code="200">Reset email sent successfully.</response>
        /// <response code="400">Validation error or email not found.</response>
        [HttpPost("forget-password")]
        [EnableRateLimiting("Auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto dto)
        {
            try
            {
                var command = new ForgetPasswordCommand(dto);
                var result = await _mediator.Send(command, HttpContext.RequestAborted);
                return Ok(result);
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse { Errors = [.. e.Errors.Select(err => err.ErrorMessage)] });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
