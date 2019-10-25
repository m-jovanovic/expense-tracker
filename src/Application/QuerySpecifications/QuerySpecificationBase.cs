using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Core.Primitives;

namespace Application.QuerySpecifications
{
    /// <summary>
    /// Represents the base specification class that all specifications derive from.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public abstract class QuerySpecificationBase<TEntity> : IQuerySpecification<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuerySpecificationBase{TEntity}"/> class.
        /// </summary>
        /// <param name="criteria">The filter criteria.</param>
        protected QuerySpecificationBase(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
            IncludeExpressions = new List<Expression<Func<TEntity, object>>>();
            IncludeStrings = new List<string>();
        }

        /// <inheritdoc />
        public Expression<Func<TEntity, bool>> Criteria { get; }

        /// <inheritdoc />
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        /// <inheritdoc />
        public List<string> IncludeStrings { get; }

        /// <inheritdoc />
        public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }

        /// <inheritdoc />
        public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

        /// <summary>
        /// Adds the specified include expression to the list of include expressions.
        /// </summary>
        /// <param name="includeExpression">The include expression.</param>
        protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

        /// <summary>
        /// Adds the specified include string to the list of include strings.
        /// </summary>
        /// <param name="includeString">The include string.</param>
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        /// <summary>
        /// Applies the specified order by expression.
        /// </summary>
        /// <param name="orderByExpression">The order by expression.</param>
        protected virtual void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderByExpression = orderByExpression;

            OrderByDescendingExpression = null;
        }

        /// <summary>
        /// Applies the specified order by descending expression.
        /// </summary>
        /// <param name="orderByDescendingExpression">The order by descending expression.</param>
        protected virtual void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
            OrderByDescendingExpression = orderByDescendingExpression;

            OrderByExpression = null;
        }
    }
}
