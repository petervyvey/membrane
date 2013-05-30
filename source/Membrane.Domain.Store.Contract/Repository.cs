

using System;
using System.Collections.Generic;
using Membrane.Foundation.Entity;


namespace Membrane.Domain.Store
{
    /// <summary>
    /// Base class for repositories.
    /// </summary>
    /// <typeparam name="TEntity">The entity type handled by the repository implementation.</typeparam>
    /// <typeparam name="TIdentifier">The entity identifier type.</typeparam>
    public abstract class Repository<TEntity, TIdentifier>
        : IRepository<TEntity, TIdentifier> where TEntity : class, IEntity<TIdentifier>
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected Repository()
            : base() { }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="context">The <see cref="IEntityContext"/> instance holding the entity store parameters.</param>
        protected Repository(IEntityContext context)
            : this()
        {
            this.Context = context;
        }

        #endregion

        #region - Private fields -

        /// <summary>
        /// TRUE if the object is disposed, FALSE otherwise.
        /// </summary>
        private bool isDisposed = false;

        #endregion

        #region - Properties -

        /// <summary>
        /// The <see cref="IEntityContext"/>.
        /// </summary>
        public IEntityContext Context { get; private set; }

        #endregion

        #region - Public methods -

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

        /// <summary>
        /// Gets the entity identified by the identifier.
        /// </summary>
        /// <param name="identifier">The entity identifier.</param>
        /// <returns>The entity matching the given identifier.</returns>
        public TEntity Get(TIdentifier identifier)
        {
            return this.Context.Get<TEntity, TIdentifier>(identifier);
        }

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <returns>The entity set of the given entity type.</returns>
        public IList<TEntity> GetList()
        {
            return this.Context.GetList<TEntity>();
        }

        /// <summary>
        /// Adds or updates the given entity.
        /// </summary>
        /// <param name="entity">The entity</param>
        public void AddOrUpdate(TEntity entity)
        {
            this.Context.AddOrUpdate(entity);
        }

        /// <summary>
        /// Merges the entity with the existing repository instance.
        /// </summary>
        /// <param name="entity">The entity to merge.</param>
        public void Merge(TEntity entity)
        {
            this.Context.Merge(entity);
        }

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity">The entity</param>
        public void Delete(TEntity entity)
        {
            this.Context.Delete(entity);
        }

        #endregion

        #region - Private & protected methods -

        /// <summary>
        /// Disposes managed and unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">Flag indicating how this protected method was called. 
        /// TRUE means via Dispose(), FALSE means via the destructor.
        /// Only in case of a call through the Dispose() method should managed resources be freed.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                this.Context = null;
            }

            this.isDisposed = true;
        }

        #endregion
    }
}
