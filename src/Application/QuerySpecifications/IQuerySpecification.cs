using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Primitives;

namespace Application.QuerySpecifications
{
    /// <summary>
    /// Represents a specification interface that all specifications must implement.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IQuerySpecification<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Gets the search criteria.
        /// </summary>
        Expression<Func<TEntity, bool>> Criteria { get; }

        /// <summary>
        /// Gets the list of include expressions.
        /// </summary>
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        /// <summary>
        /// Gets the list of include strings.
        /// </summary>
        List<string> IncludeStrings { get; }

        /// <summary>
        /// Gets the order by expression.
        /// </summary>
        Expression<Func<TEntity, object>>? OrderByExpression { get; }

        /// <summary>
        /// Gets the order by descending expression.
        /// </summary>
        Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; }
    }
}
