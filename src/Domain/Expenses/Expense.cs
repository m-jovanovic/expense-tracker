using System;
using Domain.Core.Primitives;
using Domain.Exceptions;
using Domain.Expenses.Events;
using Domain.Infrastructure;

namespace Domain.Expenses
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
        /// <param name="name">The expense name.</param>
        /// <param name="money">The money of the expense.</param>
        /// <param name="date">The date of the expense.</param>
        /// <exception cref="ArgumentException"> if the expense identifier, the user identifier or
        /// the expense name are empty.</exception>
        /// <exception cref="EmptyMoneyException"> is the specified money instance is empty.</exception>
        public Expense(Guid id, Guid userId, string name, Money money, DateTime date)
        {
            Check.NotEmpty(id, "The identifier is required", nameof(id));
            Check.NotEmpty(userId, "The first name is required", nameof(userId));
            Check.NotEmpty(name, "The expense name is required", nameof(name));
            Check.NotEmpty(money);

            Id = id;
            UserId = userId;
            Name = name;
            Money = money;
            Date = date;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expense"/> class.
        /// </summary>
        private Expense()
        {
            Name = string.Empty;
            Money = Money.Empty;
        }

        /// <summary>
        /// Gets the expense name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the expense' user identifier.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Gets the expense money.
        /// </summary>
        public Money Money { get; private set; }

        /// <summary>
        /// Gets the expense date.
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
        /// Changes the name of the expense.
        /// </summary>
        /// <param name="name">The new name of the expense.</param>
        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The expense name is required.", nameof(name));
            }

            if (string.Equals(Name, name, StringComparison.InvariantCulture))
            {
                return;
            }

            Name = name;

            AddDomainEvent(new ExpenseNameChangedEvent(Id, Name));
        }

        /// <summary>
        /// Changes the money amount of the expense.
        /// </summary>
        /// <param name="amount">The new amount of money.</param>
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

            AddDomainEvent(new ExpenseAmountChangedEvent(Id, new Money(amountDifference, Money.Currency)));
        }

        /// <summary>
        /// Changes the date of the expense.
        /// </summary>
        /// <param name="date">The date to be set for the expense.</param>
        public void ChangeDate(DateTime date)
        {
            if (Date.Date == date.Date)
            {
                return;
            }

            Date = date;

            AddDomainEvent(new ExpenseDateChangedEvent(Id, Date));
        }
    }
}
