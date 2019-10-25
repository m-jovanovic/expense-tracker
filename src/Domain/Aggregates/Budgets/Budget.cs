using System;
using Domain.Aggregates.Expenses;
using Domain.Core.Primitives;
using Domain.Events;
using Domain.Exceptions;

namespace Domain.Aggregates.Budgets
{
    /// <summary>
    /// Represents the budget entity.
    /// </summary>
    public sealed class Budget : AggregateRoot, IAuditableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Budget"/> class.
        /// </summary>
        /// <param name="currency">The currency instance.</param>
        /// <param name="amount">The budget amount.</param>
        /// <param name="startsOnUtc">The date and time the budget starts on in UTC format.</param>
        /// <param name="endsOnUtc">The date and time the budget ends on in UTC format.</param>
        /// <exception cref="ArgumentException"> if currency is empty.</exception>
        /// <exception cref="EndDatePrecedesStartDateException"> if end date precedes start date.</exception>
        /// <exception cref="NegativeAmountException"> if the amount is negative.</exception>
        public Budget(Currency currency, decimal amount, DateTime startsOnUtc, DateTime endsOnUtc)
        {
            if (currency.Equals(Currency.None))
            {
                throw new ArgumentException("The currency is required.", nameof(currency));
            }

            if (endsOnUtc < startsOnUtc)
            {
                throw new EndDatePrecedesStartDateException(startsOnUtc, endsOnUtc);
            }

            if (amount < decimal.Zero)
            {
                throw new NegativeAmountException(amount);
            }

            Currency = currency;
            Amount = amount;
            StartsOnUtc = startsOnUtc;
            EndsOnUtc = endsOnUtc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Budget"/> class.
        /// </summary>
        private Budget()
        {
            Currency = Currency.None;
        }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        public Currency Currency { get; private set; }

        /// <summary>
        /// Gets the budget amount.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Gets the spent amount of the budget.
        /// </summary>
        public decimal Spent { get; private set; }

        /// <summary>
        /// Gets the remaining amount of the budget.
        /// </summary>
        public decimal Remaining => Amount - Spent;

        /// <summary>
        /// Gets the date and time the budget starts on in UTC format.
        /// </summary>
        public DateTime StartsOnUtc { get; }

        /// <summary>
        /// Gets the date and time the budget ends on in UTC format.
        /// </summary>
        public DateTime EndsOnUtc { get; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; private set; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; private set; }

        /// <summary>
        /// Creates the budget for the current month using the specified parameters.
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <param name="amount">The budget amount.</param>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The budget for the current month with the specified amount and currency.</returns>
        public static Budget CreateForCurrentMonth(Currency currency, decimal amount, DateTime utcNow)
        {
            var startsOn = new DateTime(utcNow.Year, utcNow.Month, 1);

            DateTime endsOn = startsOn.AddMonths(1).AddDays(-1);

            return new Budget(currency, amount, startsOn, endsOn);
        }

        /// <summary>
        /// Withdraws the specified amount of money from the budget.
        /// </summary>
        /// <param name="money">The money to withdraw.</param>
        public void Withdraw(Money money)
        {
            if (!money.Currency.Equals(Currency))
            {
                return;
            }

            Spent += money.Amount;

            AddDomainEvent(new BudgetUpdated(Id));
        }

        /// <summary>
        /// Deposits the specified amount to the budget.
        /// </summary>
        /// <param name="amount">The amount to deposit.</param>
        public void Deposit(decimal amount)
        {
            Spent -= amount;

            AddDomainEvent(new BudgetUpdated(Id));
        }
    }
}
