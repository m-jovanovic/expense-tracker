using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseTracker.Domain.Aggregates.ExpenseAggregate;
using ExpenseTracker.Domain.Events;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Domain.Aggregates.UserAggregate
{
    /// <summary>
    /// Represents the user entity.
    /// </summary>
    public sealed class User : AggregateRoot, IAuditableEntity
    {
        private readonly List<Expense> _expenses;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="firstName">The user first name.</param>
        /// <param name="lastName">The user last name.</param>
        /// <param name="email">The user email.</param>
        public User(Guid id, string firstName, string lastName, Email email)
            : this()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        private User()
        {
            _expenses = new List<Expense>();
        }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        public Email Email { get; private set; }

        /// <summary>
        /// Gets the expenses. This collection is readonly.
        /// </summary>
        public IReadOnlyList<Expense> Expenses => _expenses.AsReadOnly();

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; private set; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; private set; }

        /// <summary>
        /// Adds the specified expense to the users expenses.
        /// </summary>
        /// <param name="expense">The expense.</param>
        public void AddExpense(Expense expense)
        {
            if (expense == null)
            {
                throw new DomainException("Expense can not be null.");
            }

            Maybe<Expense> expenseOrNothing = GetExpenseIfExists(expense);

            if (expenseOrNothing.HasValue)
            {
                return;
            }

            _expenses.Add(expense);

            AddDomainEvent(new ExpenseAddedEvent(expense.Id));
        }

        /// <summary>
        /// Removes the specified expense from the users expenses.
        /// </summary>
        /// <param name="expense">The expense.</param>
        public void RemoveExpense(Expense expense)
        {
            Check.NotNull(expense, nameof(expense));

            Maybe<Expense> expenseOrNothing = GetExpenseIfExists(expense);

            if (expenseOrNothing.HasNoValue)
            {
                return;
            }

            _expenses.Remove(expense);

            AddDomainEvent(new ExpenseRemovedEvent(expense.Id));
        }

        /// <summary>
        /// Gets the specified expense if it exists.
        /// </summary>
        /// <param name="expense">The expense.</param>
        /// <returns>The expense if it exists, otherwise null.</returns>
        private Maybe<Expense> GetExpenseIfExists(Expense expense) => _expenses.SingleOrDefault(e => e.Equals(expense));
    }
}
