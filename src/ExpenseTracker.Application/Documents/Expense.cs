using AutoMapper;
using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Documents
{
    /// <summary>
    /// Represents the expense document.
    /// </summary>
    public sealed class Expense : IMappable
    {
        public Expense()
        {
            Id = string.Empty;
            UserId = string.Empty;
            CurrencySymbol = string.Empty;
            Date = string.Empty;
        }

        /// <summary>
        /// Gets or sets the expense identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency symbol.
        /// </summary>
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets the formatted expense.
        /// </summary>
        public string FormattedExpense => $"{CurrencySymbol} {Amount:N2}";

        /// <inheritdoc />
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Aggregates.Expenses.Expense, Expense>()
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Money.Amount))
                .ForMember(d => d.CurrencySymbol, o => o.MapFrom(s => s.Money.Currency.Symbol))
                .ForMember(d => d.FormattedExpense, o => o.Ignore());
        }
    }
}
