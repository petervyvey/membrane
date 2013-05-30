
using System;
using System.Collections.Generic;
using Membrane.Foundation.Entity;

namespace Membrane.Domain.Store
{
    /// <summary>
    /// Interface defining the API of repository implementations.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TIdentifier">The entity identifier type.</typeparam>
    public interface IRepository<TEntity, TIdentifier>
        : IDisposable
        where TEntity : class, IEntity<TIdentifier>
    {
        /// <summary>
        /// The <see cref="IEntityContext"/>.
        /// </summary>
        IEntityContext Context { get; }

        /// <summary>
        /// Gets the entity identified by the identifier.
        /// </summary>
        /// <param name="identifier">The entity identifier.</param>
        /// <returns>The entity matching the given identifier.</returns>
        TEntity Get(TIdentifier identifier);

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <returns>The entity set of the given entity type.</returns>
        IList<TEntity> GetList();

        /// <summary>
        /// Adds or updates the given entity.
        /// </summary>
        /// <param name="entity">The entity</param>
        void AddOrUpdate(TEntity entity);

        /// <summary>
        /// Merges the entity with the existing repository instance.
        /// </summary>
        /// <param name="entity">The entity to merge.</param>
        void Merge(TEntity entity);

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity">The entity</param>
        void Delete(TEntity entity);
    }
}
