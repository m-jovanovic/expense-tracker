using System.Linq;
using ExpenseTracker.Application.Specification;
using ExpenseTracker.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence
{
    /// <summary>
    /// Represents an <see cref="ISpecification{TEntity}"/> evaluator.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static class SpecificationEvaluator<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Gets the query for the specified <see cref="ISpecification{T}"/> object.
        /// </summary>
        /// <param name="inputQueryable">The input queryable.</param>
        /// <param name="specification">The specification.</param>
        /// <returns>An <see cref="IQueryable{T}"/> for the specified entity type.</returns>
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQueryable, ISpecification<TEntity> specification)
        {
            IQueryable<TEntity> outputQueryable = inputQueryable;

            if (specification.Criteria != null)
            {
                outputQueryable = outputQueryable.Where(specification.Criteria);
            }

            outputQueryable = specification
                .IncludeExpressions
                .Aggregate(outputQueryable, (current, include) => current.Include(include));

            outputQueryable = specification
                .IncludeStrings
                .Aggregate(outputQueryable, (current, include) => current.Include(include));

            if (specification.OrderByExpression != null)
            {
                outputQueryable = outputQueryable.OrderBy(specification.OrderByExpression);
            }
            else if (specification.OrderByDescendingExpression != null)
            {
                outputQueryable = outputQueryable.OrderByDescending(specification.OrderByDescendingExpression);
            }

            return outputQueryable;
        }
    }
}
