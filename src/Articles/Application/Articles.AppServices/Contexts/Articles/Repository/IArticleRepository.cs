using Articles.AppServices.Specification;
using Articles.Contracts.Articles;
using Articles.Contracts.Base;
using Articles.Domain.Entities;

namespace Articles.AppServices.Contexts.Articles.Repository;

/// <summary>
/// Репозиторий для работы со статьями.
/// </summary>
public interface IArticleRepository
{
    /// <summary>
    /// Получить статьи по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекция моделей статей.</returns>
    Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter, CancellationToken cancellationToken);

    /// <summary>
    /// Найти статьи по спецификации.
    /// </summary>
    /// <param name="predicate">Спецификация.</param>
    /// <param name="page"></param>
    /// <param name="take"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекция статей.</returns>
    Task<PaginationCollection<ArticleDto>> FindAsync(Specification<Article> predicate,
        int page,
        int take,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить модель статьи по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель статьи.</returns>
    Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Создаёт статью по модели.
    /// </summary>
    /// <param name="article">Модель статьи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор созданной статьи.</returns>
    Task<Guid> AddAsync(Article article, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновляет статью по модели.
    /// </summary>
    /// <param name="id">Идентификатор существующей статьи.</param>
    /// <param name="request">Модель обновления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель обновлённой статьи.</returns>
    Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет статью.
    /// </summary>
    /// <param name="id">Идентификатор статьи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}