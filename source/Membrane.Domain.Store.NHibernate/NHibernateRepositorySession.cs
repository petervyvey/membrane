
using System;
using System.Collections.Generic;
using NHibernate;

namespace Membrane.Domain.Store
{
    /// <summary>
    /// The NHibernate specific repository session.
    /// </summary>
    public class NHibernateRepositorySession
        : IDisposable
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        private NHibernateRepositorySession()
            : base() { }

        internal NHibernateRepositorySession(ISession session)
            : this()
        {
            this.Session = session;
            this.InitializeObject();
        }

        #endregion

        #region - Destructor -

        /// <summary>
        /// Destructor.
        /// </summary>
        ~NHibernateRepositorySession()
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

        #region - Properties -

        /// <summary>
        /// The NHibernate session.
        /// </summary>
        public ISession Session { get; private set; }

        /// <summary>
        /// The Nhibernate transaction.
        /// </summary>
        public ITransaction Transaction { get; private set; }

        #endregion

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
        /// Starts a NHibernate transaction.
        /// </summary>
        public void StartTransaction()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Dispose();
            }

            this.Transaction = this.Session.BeginTransaction();
        }

        /// <summary>
        /// Commits the current NHibernate transaction and start a new transaction..
        /// </summary>
        public void Commit()
        {
            this.Session.Flush();
            this.Transaction.Commit();
            this.StartTransaction();
        }

        /// <summary>
        /// Rolls back the current NHibernate transaction and starts a new transaction..
        /// </summary>
        public void Rollback()
        {
            this.Transaction.Rollback();
            this.StartTransaction();
        }

        /// <summary>
        /// Gets the entity identified by the identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TIdentifier">The entity identifier type.</typeparam>
        /// <param name="identifier">The entity identifier.</param>
        /// <returns>The entity matching the given identifier.</returns>
        public TEntity Get<TEntity, TIdentifier>(TIdentifier identifier)
        {
            TEntity result = this.Session.Get<TEntity>(identifier);

            return result;
        }

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The entity set of the given entity type.</returns>
        public IList<TEntity> GetList<TEntity>()
            where TEntity : class
        {
            return this.Session.QueryOver<TEntity>().List<TEntity>();
        }

        /// <summary>
        /// Creates a query for the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The query for the entity set.</returns>
        public IQueryOver<TEntity> Query<TEntity>()
            where TEntity : class
        {
            return this.Session.QueryOver<TEntity>();
        }

        /// <summary>
        /// Adds or updates the given entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity</param>
        public void AddOrUpdate<TEntity>(TEntity entity)
        {
            this.Session.SaveOrUpdate(entity);
        }

        /// <summary>
        /// Merges the entity with the existing repository instance.
        /// </summary>
        /// <param name="entity">The entity to merge.</param>
        public void Merge<TEntity>(TEntity entity)
            where TEntity : class
        {
            this.Session.Merge(entity);
        }

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity</param>
        public void Delete<TEntity>(TEntity entity)
        {
            this.Session.Delete(entity);
        }

        #region - Private & protected methods -

        /// <summary>
        /// 
        /// </summary>
        private void InitializeObject()
        {
            this.StartTransaction();
        }

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
                if (isDisposing)
                {
                    if (this.Transaction != null)
                    {
                        this.Transaction.Rollback();
                        this.Transaction.Dispose();
                        this.Transaction = null;
                    }

                    if (this.Session != null)
                    {
                        // ATTENTION: Do not use CLOSE() ! 
                        // It breaks when outer TransactionScope instances are active! 
                        // Only use DISPOSE().
                        //this.Session.Close(); 

                        this.Session.Dispose();
                        this.Session = null;
                    }
                }
            }

            this.isDisposed = true;
        }

        #endregion
    }
}
