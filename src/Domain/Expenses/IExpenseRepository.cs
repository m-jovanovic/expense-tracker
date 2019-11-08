using System;
using System.Threading.Tasks;

namespace Domain.Expenses
{
    /// <summary>
    /// Represents the expense repository interface.
    /// </summary>
    public interface IExpenseRepository
    {
        /// <summary>
        /// Inserts the specified expense to the database.
        /// </summary>
        /// <param name="expense">The expense to insert.</param>
        void Insert(Expense expense);

        /// <summary>
        /// Deletes the specified expense from the database.
        /// </summary>
        /// <param name="expense">The expense to delete.</param>
        void Delete(Expense expense);

        /// <summary>
        /// Gets the expense with the specified identifier if exists, otherwise null.
        /// </summary>
        /// <param name="id">The expense identifier.</param>
        /// <returns>The expense with the specified identifier if it exists, otherwise null.</returns>
        Task<Expense?> GetByIdAsync(Guid id);
    }
}
