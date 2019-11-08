using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Expenses;

namespace Persistence.Repository
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
        public void Insert(Expense expense) => _dbContext.Insert(expense);

        /// <inheritdoc />
        public void Delete(Expense expense) => _dbContext.Delete(expense);

        /// <inheritdoc />
        public async Task<Expense?> GetByIdAsync(Guid id) => await _dbContext.GetByIdAsync<Expense>(id);
    }
}
