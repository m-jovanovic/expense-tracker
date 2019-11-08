using Application.Documents.Abstractions;
using AutoMapper;

namespace Application.Documents.Documents
{
    /// <summary>
    /// Represents the expense document.
    /// </summary>
    public sealed class Expense : IMappable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Expense"/> class.
        /// </summary>
        public Expense()
        {
            Id = string.Empty;
            UserId = string.Empty;
            Name = string.Empty;
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
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

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
        }
    }
}
