
using System;
using System.Collections.Generic;

namespace Membrane.Domain.Store
{
    /// <summary>
    /// Interface defining the API for the entity context.
    /// </summary>
    public interface IEntityContext
        : IDisposable 
    {
		/// <summary>
		/// The last executed SQL statement within the context.
		/// </summary>
		string LastSqlStatement { get; }

        /// <summary>
        /// Gets the entity identified by the identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TIdentifier">The entity identifier type.</typeparam>
        /// <param name="identifier">The entity identifier.</param>
        /// <returns>The entity matching the given identifier.</returns>
        TEntity Get<TEntity, TIdentifier>(TIdentifier identifier);

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The entity set of the given entity type.</returns>
        IList<TEntity> GetList<TEntity>() where TEntity : class;

        /// <summary>
        /// Adds or updates the given entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity</param>
        void AddOrUpdate<TEntity>(TEntity entity);

        /// <summary>
        /// Merges the entity with the existing repository instance.
        /// </summary>
        /// <param name="entity">The entity to merge.</param>
        void Merge<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity</param>
        void Delete<TEntity>(TEntity entity);

        /// <summary>
        /// Commits the NHibernate transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back the NHibernate transaction.
        /// </summary>
        void Rollback();
    }
}
