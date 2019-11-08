using Domain.Expenses;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer.Infrastructure;

namespace Persistence.SqlServer.Configuration
{
    /// <summary>
    /// Represents the <see cref="Expense"/> entity configuration.
    /// </summary>
    public sealed class ExpenseConfiguration : EntityTypeConfiguration<Expense>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable(nameof(Expense));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Date).IsRequired();

            builder.Property(e => e.CreatedOnUtc).IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            builder.OwnsOne(e => e.Money, moneyBuilder =>
            {
                moneyBuilder.WithOwner();

                moneyBuilder.Property(m => m.Amount)
                    .HasColumnName(nameof(Money.Amount))
                    .HasColumnType("decimal(19,4)")
                    .IsRequired();

                moneyBuilder.OwnsOne(m => m.Currency, currencyBuilder =>
                {
                    currencyBuilder.WithOwner();

                    currencyBuilder.Property(c => c.Value)
                        .HasColumnName("CurrencyId")
                        .IsRequired();

                    currencyBuilder.Property(c => c.Name)
                        .HasColumnName("CurrencyName")
                        .HasMaxLength(50)
                        .IsRequired();

                    currencyBuilder.Property(c => c.Symbol)
                        .HasColumnName("CurrencySymbol")
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
