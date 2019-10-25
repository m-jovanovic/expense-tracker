using System;
using Domain.Core.Primitives;
using Domain.Events;
using Domain.Exceptions;

namespace Domain.Aggregates.Expenses
{
    /// <summary>
    /// Represents the expense entity.
    /// </summary>
    public class Expense : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
    {
        private DateTime _date;

        /// <summary>
        /// Initializes a new instance of the <see cref="Expense"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="money">The money of the expense.</param>
        /// <param name="date">The date of the expense.</param>
        /// <exception cref="ArgumentException"> if the identifier or the user identifier is empty.</exception>
        public Expense(Guid id, Guid userId, Money money, DateTime date)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("The identifier is required.", nameof(id));
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentException("The user identifier is required.", nameof(userId));
            }

            Id = id;
            UserId = userId;
            Money = money;
            Date = date;
        }

        private Expense()
        {
            Money = Money.Empty;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Gets the money of the expense.
        /// </summary>
        public Money Money { get; private set; }

        /// <summary>
        /// Gets the date of the expense.
        /// </summary>
        public DateTime Date
        {
            get => _date;
            private set => _date = value.Date;
        }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; private set; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; private set; }

        /// <inheritdoc />
        public bool IsDeleted { get; private set; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; private set; }

        /// <summary>
        /// Changes the amount of the expense.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <exception cref="NegativeAmountException"> if the specified amount is negative.</exception>
        public void ChangeAmount(decimal amount)
        {
            if (amount < decimal.Zero)
            {
                throw new NegativeAmountException(amount);
            }

            if (amount == Money.Amount)
            {
                return;
            }

            decimal amountDifference = Money.Amount - amount;

            Money = Money.ChangeAmount(amount);

            AddDomainEvent(new ExpenseAmountChangedEvent(Id, amountDifference));
        }
    }
}
