using ExpenseTracker.Domain.Aggregates.UserAggregate;
using ExpenseTracker.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configuration
{
    /// <summary>
    /// Represents the <see cref="User"/> entity configuration.
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.CreatedOnUtc)
                .IsRequired();

            builder.OwnsOne(u => u.Email)
                .Property(e => e.Value)
                .HasColumnName(nameof(User.Email))
                .HasMaxLength(255)
                .IsRequired();

            builder.HasMany(u => u.Expenses)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
