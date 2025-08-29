using Articles.AppServices.Contexts.Articles.Repository;
using Articles.Contracts.Articles;
using Microsoft.AspNetCore.Http;

namespace Articles.AppServices.Contexts.Articles.Services;

public class ArticleService(IArticleRepository articleRepository) : IArticleService
{
    public Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter)
    {
        throw new NotImplementedException();
    }

    public Task<ArticleDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ArticleDto> CreateAsync(CreateArticleDto article)
    {
        throw new NotImplementedException();
    }

    public Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto article)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}