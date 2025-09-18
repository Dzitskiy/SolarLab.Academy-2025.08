using System.Linq.Expressions;
using Articles.AppServices.Specification;
using Articles.Domain.Entities;

namespace Articles.AppServices.Contexts.Articles.Specification;

/// <summary>
/// Спецификация выбора статей по названию.
/// </summary>
/// <param name="title">Название.</param>
public class ArticleTitleSpecification(string title) : Specification<Article>
{
    /// <inheritdoc />
    public override Expression<Func<Article, bool>> PredicateExpression => 
        a => a.Title.Contains(title);
}