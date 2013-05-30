

using NHibernate;

namespace Membrane.Domain.Store
{
    /// <summary>
    /// Defines the API of NHibernate entity context.
    /// </summary>
    public interface INHibernateEntityContext
        : IEntityContext
    {
        /// <summary>
        /// The NHibernate respository session.
        /// </summary>
        NHibernateRepositorySession Session { get; }
    }
}
