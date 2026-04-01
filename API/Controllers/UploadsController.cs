using Application.Mappers;
using Application.Queries.Uploads;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages file uploads via Cloudinary. Supports profile pictures and document uploads.
    /// </summary>
    [Route("api/uploads")]
    [ApiController]
    [Authorize]
    public class UploadsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>Get all uploaded files for a user.</summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>List of uploaded files with Cloudinary URLs.</returns>
        /// <response code="200">Returns list of uploads.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllByUser([FromRoute] string userId)
        {
            var query = new GetAllFilesByUserQuery(userId);
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(result);
        }

        /// <summary>Get a specific file upload for a user.</summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>Upload details including Cloudinary URL and metadata.</returns>
        /// <response code="200">Returns the upload.</response>
        /// <response code="404">Upload not found.</response>
        [HttpGet("get-file/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFileByUser([FromRoute] string userId)
        {
            var query = new GetAllFilesByUserQuery(userId);
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            if (result == null) return NotFound(new { message = "Upload record not found" });
            return Ok(result);
        }

        /// <summary>Upload a file to Cloudinary for a user.</summary>
        /// <param name="file">The file to upload (multipart/form-data).</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>Upload result including Cloudinary URL and public ID.</returns>
        /// <response code="200">File uploaded successfully.</response>
        /// <response code="500">Upload failed.</response>
        [HttpPost("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>Delete a file from Cloudinary.</summary>
        /// <param name="publicId">The Cloudinary public ID of the file.</param>
        /// <returns>Deletion result.</returns>
        /// <response code="200">File deleted successfully.</response>
        /// <response code="500">Deletion failed.</response>
        [HttpDelete("delete-file/{publicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>Replace an existing file with a new upload.</summary>
        /// <param name="file">The new file (multipart/form-data).</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="oldPublicId">The Cloudinary public ID of the file to replace.</param>
        /// <response code="200">File updated successfully.</response>
        /// <response code="404">Original file not found.</response>
        /// <response code="500">Update failed.</response>
        [HttpPatch("update-file/{userId}/{oldPublicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
