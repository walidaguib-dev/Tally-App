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
    [Route("api/users")]
    [ApiController]
    public class UsersController(
        IMediator mediator
        ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            try
            {
                var command = new RegisterUserCommand(dto);
                var result = await _mediator.Send(command, HttpContext.RequestAborted);
                return Created();
            } catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = [.. e.Errors.Select(err => err.ErrorMessage)]
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("signin")]
        [EnableRateLimiting("Auth")]
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
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = [.. e.Errors.Select(err => err.ErrorMessage)]
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("Default")]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new Application.Queries.Users.GetAllUsersQuery();
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDto dto)
        {
            try
            {
                var command = new PasswordResetCommand(dto);
                var result = await _mediator.Send(command, HttpContext.RequestAborted);
                if (result == null)
                {
                    return NotFound(new { message = "User not found" });
                }
                return Ok(result.MapToUserDto());
            }
            catch (ValidationException e)
            {
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = [.. e.Errors.Select(err => err.ErrorMessage)]
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }

        [HttpPost("forget-password")]
        [EnableRateLimiting("Auth")]
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
                return BadRequest(new ValidationErrorResponse
                {
                    Errors = [.. e.Errors.Select(err => err.ErrorMessage)]
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
