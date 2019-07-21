using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Application.Specification
{
    /// <summary>
    /// Represents a specification interface that all specifications must implement.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface ISpecification<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Gets or sets the search criteria.
        /// </summary>
        Expression<Func<TEntity, bool>> Criteria { get; }

        /// <summary>
        /// Gets or sets the list of include expressions.
        /// </summary>
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        /// <summary>
        /// Gets or sets the list of include strings.
        /// </summary>
        List<string> IncludeStrings { get; }

        /// <summary>
        /// Gets or sets the order by expression.
        /// </summary>
        Expression<Func<TEntity, object>> OrderByExpression { get; }

        /// <summary>
        /// Gets or sets the order by descending expression.
        /// </summary>
        Expression<Func<TEntity, object>> OrderByDescendingExpression { get; }
    }
}
