using System;
using AutoMapper;
using ExpenseTracker.Application.Abstractions;

namespace ExpenseTracker.Application.Documents
{
    public sealed class Expense : IMapFrom<Domain.Aggregates.Expenses.Expense>
    {
        public Expense()
        {
            Id = string.Empty;
            UserId = string.Empty;
            CurrencySymbol = string.Empty;
            Date = string.Empty;
        }

        public string Id { get; set; }

        public string UserId { get; set; }

        public decimal Amount { get; set; }

        public string CurrencySymbol { get; set; }

        public string Date { get; set; }

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
