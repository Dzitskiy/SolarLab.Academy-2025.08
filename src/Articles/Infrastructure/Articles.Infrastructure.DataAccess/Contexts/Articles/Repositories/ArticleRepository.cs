using System.Collections.Concurrent;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Specification;
using Articles.Contracts.Articles;
using Articles.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Articles.Infrastructure.DataAccess.Contexts.Articles.Repositories;

public class ArticleRepository(ILogger<ArticleRepository> logger) : IArticleRepository
{
    private readonly ConcurrentDictionary<Guid, ArticleDto> _articles = new();

    public Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter)
    {
        var articles = _articles.Values.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(filter.Title))
        {
            articles = articles.Where(a => a.Title.Contains(filter.Title, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.UserName))
        {
            articles = articles.Where(a => a.UserName.Contains(filter.UserName, StringComparison.OrdinalIgnoreCase));
        }

        var result = articles.ToList().AsReadOnly();
        return Task.FromResult<IReadOnlyCollection<ArticleDto>>(result);
    }

    public Task<IReadOnlyCollection<ArticleDto>> FindAsync(Specification<ArticleDto> predicate)
    {
        var articles = _articles.Values.ToList();
        var result = articles.Where(predicate.ToExpression().Compile()).ToList().AsReadOnly();
        return Task.FromResult<IReadOnlyCollection<ArticleDto>>(result);
    }

    public Task<ArticleDto> GetByIdAsync(Guid id)
    {
        _articles.TryGetValue(id, out var article);
        if (article is null)
        {
            logger.LogError("Запись с идентификатором: {id}", id);
            return Task.FromResult(new ArticleDto());
        }
        return Task.FromResult(article);
    }

    public Task<ArticleDto> CreateAsync(CreateArticleDto article)
    {
        var articleDto = new ArticleDto
        {
            Id = Guid.NewGuid(),
            Title = article.Title,
            Description = article.Description,
            CreatedAt = article.CreatedAt,
            UserName = article.UserName
        };

        _articles.TryAdd(articleDto.Id, articleDto);
        return Task.FromResult(articleDto);
    }

    public Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto article)
    {
        if (!_articles.TryGetValue(id, out var existingArticle))
        {
            return Task.FromResult(new ArticleDto());
        }

        var updatedArticle = new ArticleDto
        {
            Id = id,
            Title = article.Title,
            Description = article.Description,
            CreatedAt = article.CreatedAt,
            UserName = article.UserName
        };

        _articles.TryUpdate(id, updatedArticle, existingArticle);
        return Task.FromResult(updatedArticle);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var result = _articles.TryRemove(id, out _);
        return Task.FromResult(result);
    }
}