
using Membrane.Foundation.Pattern.Creational;

namespace Membrane.Foundation.Entity
{
    /// <summary>
    /// Base implementation of <see cref="IEntityHelper"/>.
	/// </summary>
    public abstract class EntityHelper
        : IEntityHelper
    {
        #region - Constants & static fields -

        /// <summary>
        /// The current instance of the <see cref="IEntityHelper"/>.
        /// </summary>
        public static readonly IEntityHelper Current = DependencyInjection.Get<IEntityHelper>();

        #endregion

        /// <summary>
        /// TRUE if the entity proxy or proxy collection is initialized, FALSE otherwise.
        /// </summary>
        /// <param name="proxy">The proxy or proxy collection.</param>
        /// <returns>TRUE is the proxy or proxy collection is initialized.</returns>
        public abstract bool IsInitialized(object proxy);
    }
}
