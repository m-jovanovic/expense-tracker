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
        private readonly IList<Expense> _expenses;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="firstName">The user first name.</param>
        /// <param name="lastName">The user last name.</param>
        /// <param name="email">The user email.</param>
        public User(Guid id, string firstName, string lastName, Email email) : this()
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
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public Email Email { get; private set; }

        public IReadOnlyList<Expense> Expenses => _expenses.ToList();

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <summary>
        /// Adds the specified expense to the users expenses.
        /// </summary>
        /// <param name="expense">The expense.</param>
        public void AddExpense(Expense expense)
        {
            if (expense == null)
            {
                throw  new DomainException("Expense can not be null.");
            }

            Maybe<Expense> expenseOrNothing = GetExpenseIfExists(expense);

            if (expenseOrNothing.HasValue)
            {
                return;
            }

            _expenses.Add(expense);

            //TODO: Add a domain event. Is it really necessary here?
        }

        /// <summary>
        /// Removes the specified expense from the users expenses.
        /// </summary>
        /// <param name="expense">The expense.</param>
        public void RemoveExpense(Expense expense)
        {
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
        private Maybe<Expense> GetExpenseIfExists(Expense expense) => _expenses.SingleOrDefault(e => !e.Deleted && e.Equals(expense));
    }
}
