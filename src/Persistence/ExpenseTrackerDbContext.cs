using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.QuerySpecifications;
using Domain.Events;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Infrastructure;

namespace Persistence
{
    /// <summary>
    /// Represents the application database context.
    /// </summary>
    public sealed class ExpenseTrackerDbContext : DbContext, IDbContext, IUnitOfWork
    {
        // TODO: Remove pragma when [AllowNull] attribute starts working.
        #nullable disable
        private readonly IDateTime _dateTime;
        private readonly IMediator _mediator;
        #nullable enable

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

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseTrackerDbContext"/> class.
        /// </summary>
        /// <param name="options">The database context options.</param>
        internal ExpenseTrackerDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime utcNow = _dateTime.UtcNow();

            UpdateAuditableEntities(utcNow);

            UpdateSoftDeletableEntities(utcNow);

            await PublishDomainEvents(cancellationToken);

            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TEntity?> GetByIdAsync<TEntity>(Guid id)
            where TEntity : Entity
        {
            if (id == Guid.Empty)
            {
                return default;
            }

            return await Set<TEntity>().FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<TEntity?> GetByQuerySpecificationAsync<TEntity>(IQuerySpecification<TEntity> querySpecification)
            where TEntity : Entity =>
            await ApplyQuerySpecification(querySpecification).SingleOrDefaultAsync();

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ListAsync<TEntity>()
            where TEntity : Entity =>
            await Table<TEntity>().ToListAsync();

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> ListByQuerySpecificationAsync<TEntity>(IQuerySpecification<TEntity> querySpecification)
            where TEntity : Entity =>
            await ApplyQuerySpecification(querySpecification).ToListAsync();

        /// <inheritdoc />
        public async Task<int> CountAsync<TEntity>()
            where TEntity : Entity =>
            await Table<TEntity>().CountAsync();

        /// <inheritdoc />
        public async Task<int> CountByQuerySpecificationAsync<TEntity>(IQuerySpecification<TEntity> querySpecification)
            where TEntity : Entity =>
            await ApplyQuerySpecification(querySpecification).CountAsync();

        /// <inheritdoc />
        public void Insert<TEntity>(TEntity entity)
            where TEntity : Entity =>
            Set<TEntity>().Add(entity);

        /// <inheritdoc />
        public void Insert<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity =>
            Set<TEntity>().AddRange(entities);

        /// <inheritdoc />
        public new void Update<TEntity>(TEntity entity)
            where TEntity : Entity =>
            Set<TEntity>().Update(entity);

        /// <inheritdoc />
        public void Update<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity =>
            Set<TEntity>().UpdateRange(entities);

        /// <inheritdoc />
        public void Delete<TEntity>(TEntity entity)
            where TEntity : Entity =>
            Set<TEntity>().Remove(entity);

        /// <inheritdoc />
        public void Delete<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity =>
            Set<TEntity>().RemoveRange(entities);

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
            Assembly? assembly = Assembly.GetAssembly(GetType());

            if (assembly is null)
            {
                throw new InvalidOperationException($"No assembly was found for {GetType().Name}.");
            }

            IEnumerable<IEntityTypeConfiguration> entityTypeConfigurations = from t in assembly.GetTypes()
                where (t.BaseType?.IsGenericType ?? false) &&
                      t.BaseType?.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                select Activator.CreateInstance(t) as IEntityTypeConfiguration;

            foreach (IEntityTypeConfiguration entityTypeConfiguration in entityTypeConfigurations)
            {
                entityTypeConfiguration.ApplyConfiguration(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Applies the provided specification.
        /// </summary>
        /// <param name="querySpecification">The query specification.</param>
        /// <returns>An <see cref="IQueryable{T}"/> for the specified entity type.</returns>
        private IQueryable<TEntity> ApplyQuerySpecification<TEntity>(IQuerySpecification<TEntity> querySpecification)
            where TEntity : Entity =>
            QuerySpecificationEvaluator<TEntity>.GetQuery(Table<TEntity>(), querySpecification);

        /// <summary>
        /// Sets the property value of the given entity to the specified value.
        /// </summary>
        /// <param name="entityEntry">The entity entry.</param>
        /// <param name="property">The property name.</param>
        /// <param name="value">The value.</param>
        private static void SetPropertyValue(EntityEntry entityEntry, string property, object value)
            => entityEntry.Property(property).CurrentValue = value;

        /// <summary>
        /// Updates the specified entity entry's referenced entries in the deleted state to the modified state.
        /// This method is recursive.
        /// </summary>
        /// <param name="entityEntry">The entity entry.</param>
        private static void UpdateDeletedEntityEntryReferencesToModified(EntityEntry entityEntry)
        {
            if (!entityEntry.References.Any())
            {
                return;
            }

            foreach (ReferenceEntry referenceEntry in entityEntry.References.Where(r => r.TargetEntry.State == EntityState.Deleted))
            {
                referenceEntry.TargetEntry.State = EntityState.Modified;

                UpdateDeletedEntityEntryReferencesToModified(referenceEntry.TargetEntry);
            }
        }

        /// <summary>
        /// Updates all the entities that implement the <see cref="IAuditableEntity"/> interface.
        /// </summary>
        private void UpdateAuditableEntities(DateTime utcNow)
        {
            foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        SetPropertyValue(entityEntry, nameof(IAuditableEntity.CreatedOnUtc), utcNow);
                        break;
                    case EntityState.Modified:
                        SetPropertyValue(entityEntry, nameof(IAuditableEntity.ModifiedOnUtc), utcNow);
                        break;
                }
            }
        }

        /// <summary>
        /// Updates all the entities that implement the <see cref="ISoftDeletableEntity"/> interface.
        /// </summary>
        private void UpdateSoftDeletableEntities(DateTime utcNow)
        {
            foreach (EntityEntry<ISoftDeletableEntity> entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>().Where(e => e.State == EntityState.Deleted))
            {
                entityEntry.State = EntityState.Modified;

                UpdateDeletedEntityEntryReferencesToModified(entityEntry);

                SetPropertyValue(entityEntry, nameof(ISoftDeletableEntity.IsDeleted), true);

                SetPropertyValue(entityEntry, nameof(ISoftDeletableEntity.DeletedOnUtc), utcNow);
            }
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
