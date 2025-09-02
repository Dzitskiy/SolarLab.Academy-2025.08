using Articles.AppServices.Specification;
using Articles.Contracts.Articles;
using Articles.Domain.Entities;

namespace Articles.AppServices.Contexts.Articles.Repository;

public interface IArticleRepository
{
    Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter);
    Task<IReadOnlyCollection<ArticleDto>> FindAsync(Specification<ArticleDto> predicate);
    Task<ArticleDto> GetByIdAsync(Guid id);
    Task<ArticleDto> CreateAsync(CreateArticleDto article);
    Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto article);
    Task<bool> DeleteAsync(Guid id);
}