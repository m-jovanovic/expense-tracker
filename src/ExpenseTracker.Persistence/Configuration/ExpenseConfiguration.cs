using ExpenseTracker.Domain.Aggregates.ExpenseAggregate;
using ExpenseTracker.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configuration
{
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
                        .HasColumnName($"{nameof(Money.Currency)}_{nameof(Currency.Value)}")
                        .IsRequired();

                    currencyBuilder.Property(c => c.Name)
                        .HasColumnName($"{nameof(Money.Currency)}_{nameof(Currency.Name)}")
                        .HasMaxLength(200)
                        .IsRequired();

                    currencyBuilder.Property(c => c.Symbol)
                        .HasColumnName($"{nameof(Money.Currency)}_{nameof(Currency.Symbol)}")
                        .HasMaxLength(20)
                        .IsRequired();
                });
            });
            
            base.Configure(builder);
        }
    }
}
