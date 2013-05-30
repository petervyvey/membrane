
using System;
using NHibernate;
using Membrane.Foundation.Entity;


namespace Membrane.Domain.Store
{
    /// <summary>
    /// Extension methods to handle extra functionality on NHibernate contexts.
    /// </summary>
    public static class NHibernateEntityContextExtension
    {
        #region - Constants & static fields -

        /// <summary>
        /// The NOT OF TYPE NHIBERNATE ENTITY CONTEXT EXCEPTION MESSAGE template.
        /// </summary>
        private const string NOT_OF_TYPE_NHIBERNATE_ENTITY_CONTEXT_EXCEPTION_MESSAGE_TEMPLATE = "The context parameter is not of type {0}.";

        #endregion

        /// <summary>
        /// Gets the current <see cref="ISession"/> instance.
        /// </summary>
        /// <param name="context">The <see cref="NHibernateEntityContext"/>.</param>
        /// <returns>The current <see cref="ISession"/>.</returns>
        public static ISession GetCurrentSession(this IEntityContext context)
        {
            if (!(context is NHibernateEntityContext)) throw new ArgumentException(string.Format(NOT_OF_TYPE_NHIBERNATE_ENTITY_CONTEXT_EXCEPTION_MESSAGE_TEMPLATE, typeof(NHibernateEntityContext).Name), "context");

            NHibernateEntityContext nhibernateEntityContext = (NHibernateEntityContext)context;
            return nhibernateEntityContext.Session.Session;
        }

        /// <summary>
        /// Creates a base query object to execute custom queries.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to query for.</typeparam>
        /// <param name="context">The <see cref="NHibernateEntityContext"/>.</param>
        /// <returns>The NHibernate base query of type QueryOver.</returns>
        public static IQueryOver<TEntity, TEntity> Query<TEntity>(this IEntityContext context)
            where TEntity : class
        {
            if (!(context is NHibernateEntityContext)) throw new ArgumentException(string.Format(NOT_OF_TYPE_NHIBERNATE_ENTITY_CONTEXT_EXCEPTION_MESSAGE_TEMPLATE, typeof(NHibernateEntityContext).Name), "context");

            NHibernateEntityContext nhibernateEntityContext = (NHibernateEntityContext)context;
            IQueryOver<TEntity, TEntity> query = nhibernateEntityContext.Session.Session.QueryOver<TEntity>();

            return query;
        }

        /// <summary>
        /// Creates a query with the given <paramref name="queryString"/>.
        /// </summary>
        /// <param name="context">The <see cref="NHibernateEntityContext"/>.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns>The query.</returns>
        public static IQuery CreateQuery(this IEntityContext context, string queryString)
        {
            if (!(context is NHibernateEntityContext)) throw new ArgumentException(string.Format(NOT_OF_TYPE_NHIBERNATE_ENTITY_CONTEXT_EXCEPTION_MESSAGE_TEMPLATE, typeof(NHibernateEntityContext).Name), "context");

            NHibernateEntityContext nhibernateEntityContext = (NHibernateEntityContext)context;
            IQuery query = nhibernateEntityContext.Session.Session.CreateQuery(queryString);

            return query;
        }

        /// <summary>
        /// Creates a query with the given <paramref name="queryString"/>.
        /// </summary>
        /// <param name="context">The <see cref="NHibernateEntityContext"/>.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns>The query.</returns>
        public static IQuery CreateSQLQuery(this IEntityContext context, string queryString)
        {
            if (!(context is NHibernateEntityContext)) throw new ArgumentException(string.Format(NOT_OF_TYPE_NHIBERNATE_ENTITY_CONTEXT_EXCEPTION_MESSAGE_TEMPLATE, typeof(NHibernateEntityContext).Name), "context");

            NHibernateEntityContext nhibernateEntityContext = (NHibernateEntityContext)context;
            IQuery query = nhibernateEntityContext.Session.Session.CreateSQLQuery(queryString);

            return query;
        }

        /// <summary>
        /// Creates a base query object to execute custom queries.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to query for.</typeparam>
        /// <typeparam name="TIdentifier">The identifier type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <returns>The NHibernate base query of type QueryOver.</returns>
        public static IQueryOver<TEntity, TEntity> Query<TEntity, TIdentifier>(this IRepository<TEntity, TIdentifier> repository)
            where TEntity : class, IEntity<TIdentifier>
        {
            if (!(repository.Context is NHibernateEntityContext)) throw new ArgumentException(string.Format(NOT_OF_TYPE_NHIBERNATE_ENTITY_CONTEXT_EXCEPTION_MESSAGE_TEMPLATE, typeof(NHibernateEntityContext).Name), "repository.Context");

            NHibernateEntityContext nhibernateEntityContext = (NHibernateEntityContext)repository.Context;
            IQueryOver<TEntity, TEntity> query = nhibernateEntityContext.Session.Session.QueryOver<TEntity>();

            return query;
        }
    }
}
