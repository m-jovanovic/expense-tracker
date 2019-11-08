﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Budgets;
using Domain.Expenses;
using Persistence.QuerySpecifications;

namespace Persistence.Repository
{
    /// <summary>
    /// Represents the budget repository.
    /// </summary>
    internal sealed class BudgetRepository : IBudgetRepository
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public BudgetRepository(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<IEnumerable<Budget>> GetByDateAndCurrencyAsync(DateTime dateTime, Currency currency)
        {
            if (currency.Equals(Currency.None))
            {
                return new List<Budget>();
            }

            return await _dbContext.ListByQuerySpecificationAsync(new BudgetQuerySpecification(dateTime, currency));
        }
    }
}
