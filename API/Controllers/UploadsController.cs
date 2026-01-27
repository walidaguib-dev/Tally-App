using Application.Mappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/uploads")]
    [ApiController]
    [Authorize]
    public class UploadsController(
        IMediator mediator
        ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("upload-file")]
        [Route("{userId}")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromRoute] string userId)
        {
            try
            {
                var command = new Application.Commands.Uploads.UploadFileCommand(file, userId);
                var result = await _mediator.Send(command);
                return Ok(result.ToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
