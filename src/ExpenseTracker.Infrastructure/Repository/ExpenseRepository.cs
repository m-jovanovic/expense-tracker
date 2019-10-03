﻿using System;
using System.Threading.Tasks;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Aggregates.Expenses;

namespace ExpenseTracker.Infrastructure.Repository
{
    /// <summary>
    /// Represents the expense repository.
    /// </summary>
    internal sealed class ExpenseRepository : IExpenseRepository
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ExpenseRepository(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public void InsertExpense(Expense expense) => _dbContext.Insert(expense);

        /// <inheritdoc />
        public void DeleteExpense(Expense expense) => _dbContext.Delete(expense);

        /// <inheritdoc />
        public async Task<Expense?> GetExpenseByIdAsync(Guid id) => await _dbContext.GetByIdAsync<Expense>(id);
    }
}