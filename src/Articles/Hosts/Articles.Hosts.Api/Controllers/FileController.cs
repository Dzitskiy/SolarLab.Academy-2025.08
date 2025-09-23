using Articles.AppServices.Contexts.Articles.Services;
using Articles.AppServices.Contexts.Files.Services;
using Articles.Contracts.Errors;
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
    }
}
