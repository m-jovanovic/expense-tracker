using System.Linq;
using ExpenseTracker.Application.QuerySpecification;
using ExpenseTracker.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence
{
    /// <summary>
    /// Represents an <see cref="IQuerySpecification{TEntity}"/> evaluator.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public static class QuerySpecificationEvaluator<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Gets the query for the specified <see cref="IQuerySpecification{TEntity}"/> object.
        /// </summary>
        /// <param name="inputQueryable">The input queryable.</param>
        /// <param name="querySpecification">The query specification.</param>
        /// <returns>An <see cref="IQueryable{T}"/> for the specified entity type.</returns>
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQueryable, IQuerySpecification<TEntity> querySpecification)
        {
            IQueryable<TEntity> outputQueryable = inputQueryable;

            if (querySpecification.Criteria != null)
            {
                outputQueryable = outputQueryable.Where(querySpecification.Criteria);
            }

            outputQueryable = querySpecification
                .IncludeExpressions
                .Aggregate(outputQueryable, (current, include) => current.Include(include));

            outputQueryable = querySpecification
                .IncludeStrings
                .Aggregate(outputQueryable, (current, include) => current.Include(include));

            if (querySpecification.OrderByExpression != null)
            {
                outputQueryable = outputQueryable.OrderBy(querySpecification.OrderByExpression);
            }
            else if (querySpecification.OrderByDescendingExpression != null)
            {
                outputQueryable = outputQueryable.OrderByDescending(querySpecification.OrderByDescendingExpression);
            }

            return outputQueryable;
        }
    }
}
