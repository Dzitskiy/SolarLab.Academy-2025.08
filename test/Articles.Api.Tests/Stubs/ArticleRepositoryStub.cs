using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Specification;
using Articles.Contracts.Articles;
using Articles.Contracts.Base;
using Articles.Domain.Entities;
using Articles.AppServices.Exceptions;

namespace Articles.Api.Tests.Stubs;

/// <summary>
/// Заглушка репозитория статей <see cref="IArticleRepository"/>.
/// </summary>
public class ArticleRepositoryStub : IArticleRepository
{
    public const string TestId = "347b6335-f0bc-492c-8bf7-f8e534cca123";
    public const string TestTitle = "blablabla";
    public const string TestDescription = "some_descrition";

    public Task<IReadOnlyCollection<ArticleDto>> GetByFilterAsync(ArticleFilterDto filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationCollection<ArticleDto>> FindAsync(Specification<Article> predicate, int page, int take, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id.ToString() != TestId)
        {
            throw new NotFoundException(id.ToString());
        }

        return new ArticleDto
        {
            Id = id,
            Title = TestTitle,
            Description = TestDescription,
            CreatedAt = DateTime.Now,
            UserName = "Ivan"
        };
    }

    public Task<Guid> AddAsync(Article article, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ArticleDto> UpdateAsync(Guid id, CreateArticleDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}