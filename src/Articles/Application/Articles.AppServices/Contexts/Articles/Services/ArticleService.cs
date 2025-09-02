using Articles.AppServices.Contexts.Articles.Builder;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.Contracts.Articles;
using Articles.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Articles.AppServices.Contexts.Articles.Services;

public class ArticleService(IArticleRepository articleRepository, IArticlePredicateBuilder predicateBuilder) : IArticleService
{
    public Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter)
    {
        // пример применения строителя.
        var query = predicateBuilder.WithUsers().OrderByTitle().Build();
        
        
        return articleRepository.GetByFilterAsync(filter);
    }

    public Task<ArticleDto> GetByIdAsync(Guid id)
    {
        return articleRepository.GetByIdAsync(id);
    }

    public Task<ArticleDto> CreateAsync(CreateArticleDto article)
    {
        return articleRepository.CreateAsync(article);
    }

    public Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto article)
    {
        return articleRepository.UpdateAsync(id, article);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return articleRepository.DeleteAsync(id);
    }
}