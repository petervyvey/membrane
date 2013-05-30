
using System;
using System.Collections.Generic;

namespace Membrane.Domain.Store
{
    /// <summary>
    /// The base class of persistence context implementations.
    /// </summary>
    public abstract class EntityContext
        : IEntityContext
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected EntityContext()
            : base() { }

        #endregion

        #region - Destructor -

        /// <summary>
        /// Destructor.
        /// </summary>
        ~EntityContext()
        {
            this.Dispose(false);
        }

        #endregion

        #region - Private fields -

        /// <summary>
        /// TRUE if the object is disposed, FALSE otherwise.
        /// </summary>
        private bool isDisposed = false;

        #endregion

		/// <summary>
		/// The last executed SQL statement within the context.
		/// </summary>
		public abstract string LastSqlStatement { get; }

        /// <summary>
        /// Gets the entity identified by the identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TIdentifier">The entity identifier type.</typeparam>
        /// <param name="identifier">The entity identifier.</param>
        /// <returns>The entity matching the given identifier.</returns>
        public abstract TEntity Get<TEntity, TIdentifier>(TIdentifier identifier);

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The entity set of the given entity type.</returns>
        public abstract IList<TEntity> GetList<TEntity>() where TEntity : class;

        /// <summary>
        /// Adds or updates the given entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity</param>
        public abstract void AddOrUpdate<TEntity>(TEntity entity);

        /// <summary>
        /// Merges the entity with the existing repository instance.
        /// </summary>
        /// <param name="entity">The entity to merge.</param>
        public abstract void Merge<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity</param>
        public abstract void Delete<TEntity>(TEntity entity);

        /// <summary>
        /// Commits the NHibernate transaction.
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// Rolls back the NHibernate transaction.
        /// </summary>
        public abstract void Rollback();

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (!this.isDisposed)
            {
                this.Dispose(true);
            }
            GC.SuppressFinalize(this);
        }

        #region - Private & protected methods -

        /// <summary>
        /// Disposes managed and unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">Flag indicating how this protected method was called. 
        /// TRUE means via Dispose(), FALSE means via the destructor.
        /// Only in case of a call through the Dispose() method should managed resources be freed.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (!this.isDisposed) { }

            this.isDisposed = true;
        }

        #endregion
    }
}
