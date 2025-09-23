using Articles.AppServices.Contexts.Files.Repositories;
using Articles.Contracts.Files;
using AutoMapper;
using File = Articles.Domain.Entities.File;

namespace Articles.AppServices.Contexts.Files.Services
{
    /// <inheritdoc cref="IFileService"/>
    public class FileService(IFileRepository repository, IMapper mapper) : IFileService
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
            var entity = mapper.Map<FileDto, File>(file);
            return repository.UploadAsync(entity, cancellationToken);
        }
    }
}
