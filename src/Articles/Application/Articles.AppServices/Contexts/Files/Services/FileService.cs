using Articles.AppServices.Contexts.Files.Repositories;
using AutoMapper;

namespace Articles.AppServices.Contexts.Files.Services
{
    /// <inheritdoc cref="IFileService"/>
    public class FileService(IFileRepository repository, IMapper mapper) : IFileService
    {

    }
}
