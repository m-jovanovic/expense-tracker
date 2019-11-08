using System;
using Application.QuerySpecifications;
using Domain.Budgets;
using Domain.Expenses;

namespace Persistence.SqlServer.QuerySpecifications
{
    /// <summary>
    /// Represents the query specification for the <see cref="Budget"/> entity.
    /// </summary>
    public sealed class BudgetQuerySpecification : QuerySpecificationBase<Budget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetQuerySpecification"/> class.
        /// </summary>
        /// <param name="dateTime">The date and time that falls within the budgets duration.</param>
        /// <param name="currency">The currency of the budget.</param>
        public BudgetQuerySpecification(DateTime dateTime, Currency currency)
            : base(b => b.Amount.Currency.Value == currency.Value && b.StartsOnUtc >= dateTime && b.EndsOnUtc <= dateTime)
        {
            ApplyOrderBy(b => b.StartsOnUtc);
        }
    }
}
