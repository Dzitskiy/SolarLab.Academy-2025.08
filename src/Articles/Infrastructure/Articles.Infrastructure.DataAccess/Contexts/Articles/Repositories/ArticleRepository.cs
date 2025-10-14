using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Specification;
using Articles.Contracts.Articles;
using Articles.Domain.Entities;
using Articles.Infrastructure.DataAccess.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Articles.AppServices.Exceptions;
using Articles.Contracts.Base;

namespace Articles.Infrastructure.DataAccess.Contexts.Articles.Repositories;

/// <inheritdoc />
public class ArticleRepository(
    ILogger<ArticleRepository> logger,
    IRepository<Article, ApplicationDbContext> repository,
    IMapper mapper
) : IArticleRepository
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(
        ArticleFilterDto filter,
        CancellationToken cancellationToken
    )
    {
        var articles = repository.GetAll();

        if (!string.IsNullOrWhiteSpace(filter.Title))
        {
            articles = articles.Where(a => a.Title.Contains(filter.Title));
        }

        if (!string.IsNullOrWhiteSpace(filter.UserName))
        {
            articles = articles.Where(a => a.User.Name.Contains(filter.UserName));
        }

        return await articles
            .ProjectTo<ArticleDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<PaginationCollection<ArticleDto>> FindAsync(Specification<Article> predicate,
        int page,
        int take,
        CancellationToken cancellationToken)
    {
        var query = repository.GetAll().Where(predicate);
        
        var total = await query.CountAsync(cancellationToken);

        var result = await query
            .OrderBy(a => a.Id)
            .Skip(take * (page - 1))
            .Take(take)
            .ProjectTo<ArticleDto>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return new PaginationCollection<ArticleDto>
        {
            Items = result.AsReadOnly(),
            Total = total,
            AvailablePages = (int)double.Round((total / (double)take), MidpointRounding.ToPositiveInfinity) - page,
        };

        /*return await repository
            .GetAll()
            .Where(predicate)
            .OrderBy(a => a.Id)
            .Skip(page)
            .Take(take)
            .ProjectTo<ArticleDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);*/
    }

    /// <inheritdoc />
    public async Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await repository.GetAll().Where(s => s.Id == id)
            .Include(s => s.User)
            .ProjectTo<ArticleDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            throw new NotFoundException(id.ToString());
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<Guid> AddAsync(Article article, CancellationToken cancellationToken)
    {        
        await repository.AddAsync(article, cancellationToken);
        return article.Id;
    }

    /// <inheritdoc />
    public async Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto request, CancellationToken cancellationToken)
    {
        var article = await repository.GetByIdAsync(id, cancellationToken);

        if (article is null)
        {
            return new ArticleDto();
        }

        var updatedArticle = mapper.Map(request, article);

        await repository.UpdateAsync(updatedArticle, cancellationToken);
        return await GetByIdAsync(id, cancellationToken) 
               ?? throw new NotFoundException(id.ToString());
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(id, cancellationToken);
    }
}