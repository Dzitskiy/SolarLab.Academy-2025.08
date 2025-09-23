using Articles.Contracts.Files;
using File = Articles.Domain.Entities.File;

namespace Articles.AppServices.Contexts.Files.Repositories
{
    /// <summary>
    /// Репозиторий для работы с файлами.
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Загрузка файла.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Id файла.</returns>
        Task<Guid> UploadAsync(File file, CancellationToken cancellationToken);

        /// <summary>
        /// Получение информации о файле.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о файле.</returns>
        Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель файла.</returns>
        Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken);
    }
}
