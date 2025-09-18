using System.Linq.Expressions;
using Articles.AppServices.Specification;
using Articles.Domain.Entities;

namespace Articles.AppServices.Contexts.Articles.Specification;

/// <summary>
/// Спецификация выбора статей по имени пользователя.
/// </summary>
/// <param name="userName">Имя пользователя.</param>
public class ArticleUserNameSpecification(string userName) : Specification<Article>
{
    /// <inheritdoc />
    public override Expression<Func<Article, bool>> PredicateExpression =>
        a => a.User.Name.Contains(userName);
}