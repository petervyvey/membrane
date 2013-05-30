
namespace Membrane.Foundation.Entity
{
	/// <summary>
	/// Interface defining the API for identifiable entities.
	/// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
	public interface IIdentifiable<TIdentifier>
	{
		/// <summary>
		/// Provides access to the identifying property of an entity.
		/// </summary>
		TIdentifier ID { get; }
	}
}
