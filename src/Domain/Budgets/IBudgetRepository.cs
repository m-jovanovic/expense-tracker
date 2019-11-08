using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Expenses;

namespace Domain.Budgets
{
    /// <summary>
    /// Represents the budget repository.
    /// </summary>
    public interface IBudgetRepository
    {
        /// <summary>
        /// Gets the budgets that fall within the specified date and time and have the specified currency.
        /// </summary>
        /// <param name="dateTime">The date and time that is within the budget.</param>
        /// <param name="currency">The currency of the budget.</param>
        /// <returns>The enumerable collection of budgets that meet the specified criteria.</returns>
        Task<IEnumerable<Budget>> GetByDateAndCurrencyAsync(DateTime dateTime, Currency currency);
    }
}
