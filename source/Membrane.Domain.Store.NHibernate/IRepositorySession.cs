
using System;

namespace Membrane.Domain.Store
{
    /// <summary>
    /// Defines the API of repository sessions.
    /// </summary>
    public interface IRepositorySession
        : IDisposable
    {
        /// <summary>
        /// The NHibernate entity context.
        /// </summary>
        INHibernateEntityContext Context { get; }
    }
}
