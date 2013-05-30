
namespace Membrane.Foundation.Entity
{
    /// <summary>
	/// Interface defining the API of entity helper.
	/// </summary>
    public interface IEntityHelper
    {
        /// <summary>
        /// TRUE if the entity proxy or proxy collection is initialized, FALSE otherwise.
        /// </summary>
        /// <param name="proxy">The proxy or proxy collection.</param>
        /// <returns>TRUE is the proxy or proxy collection is initialized.</returns>
        bool IsInitialized(object proxy);
    }
}
