using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Aggregates.Budgets;
using Domain.Aggregates.Expenses;
using Persistence.QuerySpecifications;

namespace Persistence.Repository
{
    /// <summary>
    /// Represents the budget repository.
    /// </summary>
    internal sealed class BudgetRepository : IBudgetRepository
    {
        private readonly IDbContext _dbContext;

        public BudgetRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Budget>> FindForDateAndCurrencyAsync(DateTime dateTime, Currency currency)
        {
            if (currency.Equals(Currency.None))
            {
                return new List<Budget>();
            }

            return await _dbContext.ListByQuerySpecificationAsync(new BudgetQuerySpecification(dateTime, currency));
        }
    }
}
