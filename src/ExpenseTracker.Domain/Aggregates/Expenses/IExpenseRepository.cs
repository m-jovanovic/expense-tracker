namespace ExpenseTracker.Domain.Aggregates.Expenses
{
    /// <summary>
    /// Represents the expense repository interface.
    /// </summary>
    public interface IExpenseRepository
    {
        /// <summary>
        /// Inserts the specified expense to the database.
        /// </summary>
        /// <param name="expense">The expense.</param>
        void InsertExpense(Expense expense);
    }
}
