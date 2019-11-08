using Microsoft.EntityFrameworkCore;

namespace Persistence.SqlServer.Infrastructure
{
    /// <summary>
    /// Represents an interface for applying an entity type configuration.
    /// </summary>
    public interface IEntityTypeConfiguration
    {
        /// <summary>
        /// Applies the entity type configuration using the specified <see cref="ModelBuilder"/>.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to apply the configuration.</param>
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
