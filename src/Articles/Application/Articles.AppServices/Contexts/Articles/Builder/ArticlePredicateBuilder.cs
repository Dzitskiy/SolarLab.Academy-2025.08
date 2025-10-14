using Articles.Domain.Entities;

namespace Articles.AppServices.Contexts.Articles.Builder;

public class ArticlePredicateBuilder : IArticlePredicateBuilder
{
    private IQueryable<Article> _query;
    
    public IArticlePredicateBuilder WithUsers()
    {
        //_query = _query.Include(a => a.User);

        return this;
    }

    public IArticlePredicateBuilder OrderByTitle()
    {
        _query = _query.OrderBy(a => a.Title);

        return this;
    }

    public IArticlePredicateBuilder Paginate(int pageNumber, int pageSize)
    {
        _query = _query.Take(pageNumber).Skip(pageNumber * pageSize);

        return this;
    }

    public IQueryable<Article> Build()
    {
        return _query;
    }
}