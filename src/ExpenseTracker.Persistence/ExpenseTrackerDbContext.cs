using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Specification;
using ExpenseTracker.Domain.Events;
using ExpenseTracker.Domain.Primitives;
using ExpenseTracker.Persistence.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExpenseTracker.Persistence
{
    /// <summary>
    /// Represents the application database context.
    /// </summary>
    public sealed class ExpenseTrackerDbContext : DbContext, IDbContext, IUnitOfWork
    {
        private readonly IDateTime _dateTime;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseTrackerDbContext"/> class.
        /// </summary>
        /// <param name="options">The database context options.</param>
        /// <param name="dateTime">The date time instance.</param>
        /// <param name="mediator">The mediator instance.</param>
        public ExpenseTrackerDbContext(DbContextOptions options, IDateTime dateTime, IMediator mediator)
            : base(options)
        {
            _dateTime = dateTime;
            _mediator = mediator;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities();

            UpdateSoftDeletableEntities();

            await PublishDomainEvents(cancellationToken);

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetByIdAsync<TEntity>(Guid id)
            where TEntity : Entity
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return await Set<TEntity>().FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetBySpecificationAsync<TEntity>(ISpecification<TEntity> specification)
            where TEntity : Entity
        {
            return await ApplySpecification(specification).SingleOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ListAsync<TEntity>()
            where TEntity : Entity
        {
            return await Table<TEntity>().ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ListBySpecificationAsync<TEntity>(ISpecification<TEntity> specification)
            where TEntity : Entity
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<int> CountAsync<TEntity>()
            where TEntity : Entity
        {
            return await Table<TEntity>().CountAsync();
        }

        /// <inheritdoc />
        public async Task<int> CountBySpecificationAsync<TEntity>(ISpecification<TEntity> specification)
            where TEntity : Entity
        {
            return await ApplySpecification(specification).CountAsync();
        }

        /// <inheritdoc />
        public void Insert<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            Set<TEntity>().Add(entity);
        }

        /// <inheritdoc />
        public void Insert<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity
        {
            Set<TEntity>().AddRange(entities);
        }

        /// <inheritdoc />
        public new void Update<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            Set<TEntity>().Update(entity);
        }

        /// <inheritdoc />
        public void Update<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity
        {
            Set<TEntity>().UpdateRange(entities);
        }

        /// <inheritdoc />
        public void Delete<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            Set<TEntity>().Remove(entity);
        }

        /// <inheritdoc />
        public void Delete<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity
        {
            Set<TEntity>().RemoveRange(entities);
        }

        /// <inheritdoc />
        public IQueryable<TEntity> Table<TEntity>()
            where TEntity : Entity
            => Set<TEntity>();

        /// <inheritdoc />
        public IQueryable<TEntity> TableAsNoTracking<TEntity>()
            where TEntity : Entity
            => Set<TEntity>().AsNoTracking();

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<IEntityTypeConfiguration> entityTypeConfigurations = from t in Assembly.GetAssembly(GetType()).GetTypes()
                where (t.BaseType?.IsGenericType ?? false) &&
                      t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                select (IEntityTypeConfiguration)Activator.CreateInstance(t);

            foreach (IEntityTypeConfiguration entityTypeConfiguration in entityTypeConfigurations)
            {
                entityTypeConfiguration.ApplyConfiguration(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Applies the provided specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns>An <see cref="IQueryable{T}"/> for the specified entity type.</returns>
        private IQueryable<TEntity> ApplySpecification<TEntity>(ISpecification<TEntity> specification)
            where TEntity : Entity
        {
            return SpecificationEvaluator<TEntity>.GetQuery(Table<TEntity>(), specification);
        }

        /// <summary>
        /// Sets the property value of the given entity to the specified value.
        /// </summary>
        /// <param name="entityEntry">The entity entry.</param>
        /// <param name="property">The property name.</param>
        /// <param name="value">The value.</param>
        private static void SetPropertyValue(EntityEntry entityEntry, string property, object value)
        {
            entityEntry.Property(property).CurrentValue = value;
        }

        /// <summary>
        /// Updates all the entities that implement the <see cref="IAuditableEntity"/> interface.
        /// </summary>
        private void UpdateAuditableEntities()
        {
            List<EntityEntry<IAuditableEntity>> auditableEntities = ChangeTracker
                .Entries<IAuditableEntity>()
                .ToList();

            DateTime utcNow = _dateTime.UtcNow();

            auditableEntities.ForEach(entityEntry =>
            {
                if (entityEntry.State == EntityState.Added)
                {
                    SetPropertyValue(entityEntry, nameof(IAuditableEntity.CreatedOnUtc), utcNow);
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    SetPropertyValue(entityEntry, nameof(IAuditableEntity.ModifiedOnUtc), utcNow);
                }
            });
        }

        /// <summary>
        /// Updates all the entities that implement the <see cref="ISoftDeletableEntity"/> interface.
        /// </summary>
        private void UpdateSoftDeletableEntities()
        {
            List<EntityEntry<ISoftDeletableEntity>> softDeletableEntities = ChangeTracker
                .Entries<ISoftDeletableEntity>()
                .ToList();

            DateTime utcNow = _dateTime.UtcNow();

            softDeletableEntities.ForEach(entityEntry =>
            {
                if (entityEntry.State != EntityState.Deleted)
                {
                    return;
                }

                entityEntry.State = EntityState.Modified;

                SetPropertyValue(entityEntry, nameof(ISoftDeletableEntity.IsDeleted), true);

                SetPropertyValue(entityEntry, nameof(ISoftDeletableEntity.DeletedOnUtc), utcNow);
            });
        }

        /// <summary>
        /// Publishes and then clears all the domain events that exist within the current transaction.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task PublishDomainEvents(CancellationToken cancellationToken)
        {
            List<EntityEntry<AggregateRoot>> aggregateRoots = ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            List<IDomainEvent> domainEvents = aggregateRoots
                .SelectMany(entityEntry => entityEntry.Entity.DomainEvents)
                .ToList();

            aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());

            IEnumerable<Task> tasks = domainEvents
                .Select(async domainEvent => await _mediator.Publish(domainEvent, cancellationToken));

            await Task.WhenAll(tasks);
        }
    }
}
