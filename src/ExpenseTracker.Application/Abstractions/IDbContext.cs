using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Application.Specification;
using ExpenseTracker.Domain.Primitives;

namespace ExpenseTracker.Application.Abstractions
{
    /// <summary>
    /// Represents the database context interface. Provides methods for querying entities and updating the entities of the context.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Gets an entity with the specified identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>The entity if found, or null if no entity is found.</returns>
        Task<TEntity?> GetByIdAsync<TEntity>(Guid id)
            where TEntity : Entity;

        /// <summary>
        /// Gets an entity based on the provided specification.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <returns>An entity if found, otherwise false.</returns>
        Task<TEntity?> GetBySpecificationAsync<TEntity>(ISpecification<TEntity> specification)
            where TEntity : Entity;

        /// <summary>
        /// Gets an enumerable collection of all the entities in the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>An enumerable collection of all the entities in the database.</returns>
        Task<IEnumerable<TEntity>> ListAsync<TEntity>()
            where TEntity : Entity;

        /// <summary>
        /// Gets an enumerable collection of entities based on the provided specification.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <returns>An enumerable collection of entities.</returns>
        Task<IEnumerable<TEntity>> ListBySpecificationAsync<TEntity>(ISpecification<TEntity> specification)
            where TEntity : Entity;

        /// <summary>
        /// Gets the total number of entities.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The total number of entities.</returns>
        Task<int> CountAsync<TEntity>()
            where TEntity : Entity;

        /// <summary>
        /// Gets the total number of entities based on the provided specification while skipping paging.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <returns>The total number of entities.</returns>
        Task<int> CountBySpecificationAsync<TEntity>(ISpecification<TEntity> specification)
            where TEntity : Entity;

        /// <summary>
        /// Inserts the specified entity to the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity.</param>
        void Insert<TEntity>(TEntity entity)
            where TEntity : Entity;

        /// <summary>
        /// Inserts the specified collection of entities to the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The collection of entities to be inserted.</param>
        void Insert<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity;

        /// <summary>
        /// Updates the specified entity in the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to be updated.</param>
        void Update<TEntity>(TEntity entity)
            where TEntity : Entity;

        /// <summary>
        /// Updates the specified collection of entities in the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The collection of entities to be updated.</param>
        void Update<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity;

        /// <summary>
        /// Deletes the specified entity from the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete<TEntity>(TEntity entity)
            where TEntity : Entity;

        /// <summary>
        /// Deletes the specified collection of entities from the database.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The collection of entities to be deleted.</param>
        void Delete<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity;

        /// <summary>
        /// Gets a queryable collection of entities.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The queryable collection if entities.</returns>
        IQueryable<TEntity> Table<TEntity>()
            where TEntity : Entity;

        /// <summary>
        /// Gets a queryable collection of entities that aren't tracked by the database context.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <remarks>
        /// Using this scenario is practical for read-only operations.
        /// If you need to alter the entities, then don't use this.
        /// </remarks>
        /// <returns>The queryable collection if entities that aren't tracked by the database context.</returns>
        IQueryable<TEntity> TableAsNoTracking<TEntity>()
            where TEntity : Entity;
    }
}
