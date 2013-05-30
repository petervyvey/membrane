
using System.Collections.Concurrent;
using System.Collections.Generic;
using NHibernate;

namespace Membrane.Domain.Store
{
    /// <summary>
    /// The base class for NHibernate entity context implementations.
    /// </summary>
    public abstract class NHibernateEntityContext
        : EntityContext, INHibernateEntityContext
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected NHibernateEntityContext() :
            base()
        {
            this.InitializeObject();
        }

        #endregion

        #region - Destructor -

        /// <summary>
        /// Destructor.
        /// </summary>
        ~NHibernateEntityContext()
        {
            this.Dispose(false);
        }

        #endregion

        #region - Private fields -

        /// <summary>
        /// The LOCK object for thread-safe instantiating the NHibernate session factory.
        /// </summary>
        private object LOCK_SESSION_FACTORY = new object();

        /// <summary>
        /// TRUE if the object is disposed, FALSE otherwise.
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// The set of NHibernate session factories.
        /// </summary>
        protected static readonly ConcurrentDictionary<string, ISessionFactory> sessionFactories = new ConcurrentDictionary<string, ISessionFactory>();

        #endregion

        #region - Properties -

        /// <summary>
        /// The NHibernate session wrapper.
        /// </summary>
        public NHibernateRepositorySession Session { get; private set; }

		/// <summary>
		/// The last executed SQL statement within the context.
		/// </summary>
		public override string LastSqlStatement { get { return NHibernateSql.LastSqlStatement; } }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Gets the entity identified by the identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TIdentifier">The entity identifier type.</typeparam>
        /// <param name="identifier">The entity identifier.</param>
        /// <returns>The entity matching the given identifier.</returns>
        public override TEntity Get<TEntity, TIdentifier>(TIdentifier identifier)
        {
            return this.Session.Get<TEntity, TIdentifier>(identifier);
        }

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The entity set of the given entity type.</returns>
        public override IList<TEntity> GetList<TEntity>()
        {
            return this.Session.GetList<TEntity>();
        }

        /// <summary>
        /// Adds or updates the given entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity</param>
        public override void AddOrUpdate<TEntity>(TEntity entity)
        {
            this.Session.AddOrUpdate<TEntity>(entity);
        }

        /// <summary>
        /// Merges the entity with the existing repository instance.
        /// </summary>
        /// <param name="entity">The entity to merge.</param>
        public override void Merge<TEntity>(TEntity entity)
        {
            this.Session.Merge<TEntity>(entity);
        }

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity</param>
        public override void Delete<TEntity>(TEntity entity)
        {
            this.Session.Delete<TEntity>(entity);
        }

        /// <summary>
        /// Commits the NHibernate transaction.
        /// </summary>
        public override void Commit()
        {
            this.Session.Commit();
        }

        /// <summary>
        /// Rolls back the NHibernate transaction.
        /// </summary>
        public override void Rollback()
        {
            this.Session.Rollback();
        }

        #endregion

        #region - Private & protected methods -

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void InitializeObject()
        {
            var factory = this.CreateSessionFactory();
            this.Session = new NHibernateRepositorySession(factory.OpenSession());
        }

        /// <summary>
        /// Disposes the managed and unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">TRUE if disposing and managed resources should be disposed, FALSE otherwise.</param>
        protected override void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                if (isDisposing)
                {
                    if (this.Session != null)
                    {
                        this.Session.Dispose();
                        this.Session = null;
                    }
                }
            }

            base.Dispose(isDisposing);
        }

        /// <summary>
        /// Factory method to create a NHibernate session factory.
        /// </summary>
        /// <returns>The session factory.</returns>
        protected abstract ISessionFactory CreateSessionFactory();

        #endregion
    }
}
