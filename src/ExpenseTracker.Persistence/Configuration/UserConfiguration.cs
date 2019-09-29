using ExpenseTracker.Domain.Aggregates.Users;
using ExpenseTracker.Persistence.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Configuration
{
    /// <summary>
    /// Represents the <see cref="User"/> entity configuration.
    /// </summary>
    public sealed class UserConfiguration : EntityTypeConfiguration<User>
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

            builder.OwnsOne(u => u.Email, emailBuilder =>
            {
                emailBuilder.WithOwner();

                emailBuilder.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(255)
                    .IsRequired();
            });

            base.Configure(builder);
        }
    }
}
