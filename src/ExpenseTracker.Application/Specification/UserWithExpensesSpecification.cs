using System;
using ExpenseTracker.Domain.Aggregates.UserAggregate;

namespace ExpenseTracker.Application.Specification
{
    public sealed class UserWithExpensesSpecification : SpecificationBase<User>
    {
        public UserWithExpensesSpecification(Guid id)
            : base(u => u.Id == id)
        {
            AddInclude(u => u.Expenses);
        }
    }
}