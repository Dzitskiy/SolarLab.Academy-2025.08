using System.Collections.Concurrent;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Specification;
using Articles.Contracts.Articles;
using Articles.Domain.Entities;
using Articles.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Articles.Infrastructure.DataAccess.Contexts.Articles.Repositories;

public class ArticleRepository(ILogger<ArticleRepository> logger, IRepository<Article, ApplicationDbContext> repository) : IArticleRepository
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

    public async Task<ArticleDto> GetByIdAsync(Guid id)
    {
        var article = await repository.GetAll().Where(s => s.Id == id)
            .Include(s => s.User)
            .Select(s => new ArticleDto
            {
                Id = s.Id,
                Title = s.Title,
                CreatedAt = s.CreatedAt,
                Description = s.Description,
                UserName = s.User.Name
            }).FirstOrDefaultAsync();

        return article;
    }

    public async Task<Guid> AddAsync(Article article)
    {        
        await repository.AddAsync(article);
        return article.Id;
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

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }
}