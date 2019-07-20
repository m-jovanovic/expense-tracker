using System;
using System.Threading.Tasks;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Domain.Abstractions
{
    /// <summary>
    /// Represents a repository interface.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The aggregate root type.</typeparam>
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        /// <summary>
        /// Gets the aggregate root with the specified identifier if it exists.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        /// <returns>The aggregate root instance with the specified identifier if it exists, otherwise no value.</returns>
        Task<Maybe<TAggregateRoot>> GetByIdAsync(Guid id);
    }
}