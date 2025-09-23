using Articles.AppServices.Contexts.Files.Repositories;
using Articles.Contracts.Files;
using Articles.Infrastructure.DataAccess.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using File = Articles.Domain.Entities.File;

namespace Articles.Infrastructure.DataAccess.Contexts.Files.Repositories
{
    /// <inheritdoc cref="IFileRepository"/>
    public class FileRepository(IRepository<File, ApplicationDbContext> repository, IMapper mapper) : IFileRepository
    {
        /// <inheritdoc/>
        public Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            return repository.GetAll().Where(s => s.Id == id).ProjectTo<FileDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<FileInfoDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Guid> UploadAsync(Domain.Entities.File file, CancellationToken cancellationToken)
        {
            await repository.AddAsync(file, cancellationToken);
            return file.Id;
        }
    }
}
