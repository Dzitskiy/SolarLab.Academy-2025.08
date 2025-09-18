using System.Linq.Expressions;
using Articles.AppServices.Specification.Extensions;

namespace Articles.AppServices.Specification.Internal
{
    /// <summary>
    /// Спецификация "И".
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    internal class AndSpecification<TEntity> : Specification<TEntity>
    {
        /// <inheritdoc />
        public override Expression<Func<TEntity, bool>> PredicateExpression { get; }
        
        /// <summary>
        /// Инициализирует экземпляр спецификации <see cref="AndSpecification{TEntity}"/>.
        /// </summary>
        public AndSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            PredicateExpression = left.PredicateExpression.And(right.PredicateExpression);
        }
    }
}