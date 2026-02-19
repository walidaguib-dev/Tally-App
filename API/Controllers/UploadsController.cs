using Application.Mappers;
using Application.Queries.Uploads;
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



        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetAllByUser([FromRoute] string userId) {
            var query = new GetAllFilesByUserQuery(userId);
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpGet("get-file/{userId}")]
        public async Task<IActionResult> GetFileByUser([FromRoute] string userId)
        {
            var query = new GetAllFilesByUserQuery(userId);
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            if (result == null) return NotFound(new { message = "Upload record not found" });
            return Ok(result);
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromRoute] string userId)
        {
            try
            {
                var command = new Application.Commands.Uploads.UploadFileCommand(file, userId);
                var result = await _mediator.Send(command, HttpContext.RequestAborted);
                return Ok(result.ToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("delete-file/{publicId}")]
        public async Task<IActionResult> DeleteFile([FromRoute] string publicId)
        {
            try
            {
                var command = new Application.Commands.Uploads.DeleteFileCommand(publicId);
                var result = await _mediator.Send(command, HttpContext.RequestAborted);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPatch("update-file/{userId}/{oldPublicId}")]
        public async Task<IActionResult> UpdateFile([FromForm] IFormFile file, [FromRoute] string userId, [FromRoute] string oldPublicId)
        {
            try
            {
                var command = new Application.Commands.Uploads.UpdateFileCommand(userId, file, oldPublicId);
                var result = await _mediator.Send(command, HttpContext.RequestAborted);
                if (result == null) return NotFound(new { message = "Upload record not found" });
                return Ok("file updated!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
