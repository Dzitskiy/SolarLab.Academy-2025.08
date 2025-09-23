using Articles.AppServices.Contexts.Files.Services;
using Articles.Contracts.Errors;
using Articles.Contracts.Files;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с файлами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
    public class FileController(IFileService fileService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            var bytes = await GetBytesAsync(file, cancellationToken);
            var fileDto = new FileDto
            {
                Content = bytes,
                ContentType = file.ContentType,
                Name = file.FileName,
            };
            var result = await fileService.UploadAsync(fileDto, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet("{id:guid}/info")]
        [ProducesResponseType(typeof(FileInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfoById(Guid id, CancellationToken cancellationToken)
        {
            var result = await fileService.GetInfoByIdAsync(id, cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
        {
            var result = await fileService.DownloadAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }

            Response.ContentLength = result.Content.Length;
            return File(result.Content, result.ContentType, result.Name);
        }

        private static async Task<byte[]> GetBytesAsync(IFormFile file, CancellationToken cancellationToken)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();
        }
    }
}
