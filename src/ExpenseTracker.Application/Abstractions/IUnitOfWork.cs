using System.Threading;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Abstractions
{
    /// <summary>
    /// Represents the interface for a unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all of the changes in the unit of work.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of entities that were saved.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
