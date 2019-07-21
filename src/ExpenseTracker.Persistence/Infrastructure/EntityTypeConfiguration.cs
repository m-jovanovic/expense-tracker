using ExpenseTracker.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Persistence.Infrastructure
{
    /// <summary>
    /// Represents an entity type configuration.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        /// <inheritdoc />
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
        }

        /// <inheritdoc />
        public void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }
    }
}
