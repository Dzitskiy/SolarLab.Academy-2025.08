using Articles.Contracts.Files;

namespace Articles.AppServices.Contexts.Files.Services
{
    /// <summary>
    /// Сервис для работы с файлами.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Загрузка файла.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Id файла.</returns>
        Task<Guid> UploadAsync(FileDto file, CancellationToken cancellationToken);

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
