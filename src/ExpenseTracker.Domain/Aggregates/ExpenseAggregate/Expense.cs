using System;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Domain.Aggregates.ExpenseAggregate
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
        /// <param name="money">The money of the expense.</param>
        /// <param name="date">The date of the expense.</param>
        public Expense(Guid id, Money money, DateTime date)
        {
            Id = id;
            Money = money;
            Date = date;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Gets or sets the money of the expense.
        /// </summary>
        public Money Money { get; private set; }

        /// <summary>
        /// Gets or sets the date of the expense.
        /// </summary>
        public DateTime Date
        {
            get => _date;
            private set => _date = value.Date;
        }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }
    }
}
