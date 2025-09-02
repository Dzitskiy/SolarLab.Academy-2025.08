using Articles.Domain.Entities;

namespace Articles.AppServices.Contexts.Articles.Builder;

public interface IArticlePredicateBuilder
{
    IArticlePredicateBuilder WithUsers();
    IArticlePredicateBuilder OrderByTitle();
    IArticlePredicateBuilder Paginate(int pageNumber, int pageSize);

    IQueryable<Article> Build();
}