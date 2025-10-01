using Articles.AppServices.Contexts.Files.Repositories;
using Articles.Contracts.Files;
using AutoMapper;
using Microsoft.Extensions.Logging;
using File = Articles.Domain.Entities.File;

namespace Articles.AppServices.Contexts.Files.Services
{
    /// <inheritdoc cref="IFileService"/>
    public class FileService(
        ILogger<FileService> logger,
        IFileRepository repository,
        IMapper mapper
    ) : IFileService
    {
        /// <inheritdoc/>
        public Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return repository.DownloadAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return repository.GetInfoByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Guid> UploadAsync(FileDto file, CancellationToken cancellationToken)
        {
            logger.LogInformation("Загружен файл \"{FileName}\", размер: {FileSize}кб", file.Name, file.Content.Length / 1024);
            var entity = mapper.Map<FileDto, File>(file);
            return repository.UploadAsync(entity, cancellationToken);
        }
    }
}
