using Articles.Contracts.Base;

namespace Articles.Contracts.Articles;

/// <summary>
/// Модель поиска статей по фильтру.
/// </summary>
public class ArticleFilterDto : IPagination
{
    /// <summary>
    /// Наименование статьи.
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Имя автора.
    /// </summary>
    public string? UserName { get; set; }

    /// <inheritdoc />
    public int Page { get; set; }

    /// <inheritdoc />
    public int Take { get; set; }
}