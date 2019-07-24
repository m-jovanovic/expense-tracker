using ExpenseTracker.Domain.Aggregates.ExpenseAggregate;
using ExpenseTracker.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configuration
{
    /// <summary>
    /// Represents the <see cref="Expense"/> entity configuration.
    /// </summary>
    public class ExpenseConfiguration : EntityTypeConfiguration<Expense>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable(nameof(Expense));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.CreatedOnUtc)
                .IsRequired();

            builder.OwnsOne(e => e.Money, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                    .HasColumnName(nameof(Money.Amount))
                    .HasColumnType("decimal(19,4)")
                    .IsRequired();

                moneyBuilder.OwnsOne(m => m.Currency, currencyBuilder =>
                {
                    currencyBuilder.Property(c => c.Value)
                        .HasColumnName($"CurrencyId")
                        .IsRequired();

                    currencyBuilder.Property(c => c.Name)
                        .HasColumnName($"{nameof(Money.Currency)}{nameof(Currency.Name)}")
                        .HasMaxLength(50)
                        .IsRequired();

                    currencyBuilder.Property(c => c.Symbol)
                        .HasColumnName($"{nameof(Money.Currency)}{nameof(Currency.Symbol)}")
                        .HasMaxLength(10)
                        .IsRequired();
                });
            });

            builder.Property(e => e.IsDeleted).HasDefaultValue(false);

            builder.HasQueryFilter(e => !e.IsDeleted);
            
            base.Configure(builder);
        }
    }
}
