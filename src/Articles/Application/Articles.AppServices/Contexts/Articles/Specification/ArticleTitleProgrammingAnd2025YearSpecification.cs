using System.Linq.Expressions;
using Articles.AppServices.Specification;
using Articles.Domain.Entities;

namespace Articles.AppServices.Contexts.Articles.Specification;

/// <summary>
/// Спецификация поиска статей по теме "Программирования" за 2025 год и позже.
/// </summary>
public class ArticleTitleProgrammingAnd2025YearSpecification : Specification<Article>
{
    private static readonly DateTime Year2025Date = new(2025, 1, 1);

    /// <inheritdoc />
    public override Expression<Func<Article, bool>> PredicateExpression =>
    a => a.Title.Contains("Программирование") && a.CreatedAt > Year2025Date;
}