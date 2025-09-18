using Articles.AppServices.Contexts.Articles.Builder;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Contexts.Articles.Specification;
using Articles.AppServices.Specification;
using Articles.Contracts.Articles;
using Articles.Contracts.Base;
using Articles.Domain.Entities;
using AutoMapper;

namespace Articles.AppServices.Contexts.Articles.Services;

/// <inheritdoc />
public class ArticleService(
    IArticleRepository articleRepository,
    IArticlePredicateBuilder predicateBuilder,
    IMapper mapper
) : IArticleService
{
    /// <inheritdoc />
    public Task<PaginationCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter,
        CancellationToken cancellationToken)
    {
        //return articleRepository.GetByFilterAsync(filter, cancellationToken);
        Specification<Article> specification = Specification<Article>.True;

        if (filter.Title is not null)
        {
            specification = specification.And(new ArticleTitleSpecification(filter.Title));
        }

        if (filter.UserName is not null)
        {
            specification = specification.And(new ArticleUserNameSpecification(filter.UserName));
        }
        
        return articleRepository.FindAsync(specification, filter.Page, filter.Take, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return articleRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public Task<Guid> CreateAsync(CreateArticleDto article, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<CreateArticleDto, Article>(article);
        return articleRepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto article, CancellationToken cancellationToken)
    {
        return articleRepository.UpdateAsync(id, article, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return articleRepository.DeleteAsync(id, cancellationToken);
    }
}