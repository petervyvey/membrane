
using NHibernate;
using Membrane.Foundation.Entity;

namespace Membrane.Domain.Store
{
    /// <summary>
    /// NHibernate specific implementation of <see cref="EntityHelper"/>.
    /// </summary>
    public sealed class NHibernateEntityHelper
        : EntityHelper
    {
        /// <summary>
        /// TRUE if the entity proxy or proxy collection is initialized, FALSE otherwise.
        /// </summary>
        /// <param name="proxy">The proxy or proxy collection.</param>
        /// <returns>TRUE is the proxy or proxy collection is initialized.</returns>
        public override bool IsInitialized(object proxy)
        {
            return NHibernateUtil.IsInitialized(proxy);
        }
    }
}
