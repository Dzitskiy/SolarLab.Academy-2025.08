namespace Articles.Contracts.Files
{
    /// <summary>
    /// Модель информации о файле.
    /// </summary>
    public class FileInfoDto
    {
        /// <summary>
        /// Идентификатор файла.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Размер файла.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Дата создания файла.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
