using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Aggregates.Expenses;

namespace Domain.Aggregates.Budgets
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> GetByDateAndCurrencyAsync(DateTime dateTime, Currency currency);
    }
}
