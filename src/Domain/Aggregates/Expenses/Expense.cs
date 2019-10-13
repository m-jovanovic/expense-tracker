using System;
using Domain.Exceptions;
using Domain.Primitives;

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
        public Expense(Guid id, Guid userId, Money money, DateTime date)
        {
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
        public void ChangeAmount(decimal amount)
        {
            if (amount < decimal.Zero)
            {
                throw new DomainException("Amount can not be less than zero.");
            }

            if (amount == Money.Amount)
            {
                return;
            }

            Money = Money.ChangeAmount(amount);
        }

        /// <summary>
        /// Changes the currency of the expense.
        /// </summary>
        /// <param name="currency">The currency.</param>
        public void ChangeCurrency(Currency currency)
        {
            if (currency.Equals(Currency.None))
            {
                throw new DomainException("Currency can not be empty.");
            }

            if (currency.Equals(Money.Currency))
            {
                return;
            }

            Money = Money.ChangeCurrency(currency);
        }
    }
}
