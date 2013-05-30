
using System.Collections.Generic;

namespace Membrane.Foundation.Entity
{
	/// <summary>
	/// Marker interface defining the API for entities.
	/// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public interface IEntity<TIdentifier>
        : IIdentifiable<TIdentifier>, IExtensible, IExtensibleOperation
    {
        /// <summary>
        /// Holds the value pointers to related members.
        /// </summary>
        References __references { get; set; }

        /// <summary>
        /// Adds a reference to the Members collection.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="reference">The reference value of the </param>
        void __addMemberReference(string name, string reference);

        /// <summary>
        /// Adds a reference to the Collections collection.
        /// </summary>
        /// <param name="name">The name of the collection.</param>
        /// <param name="references">The list of reference values.</param>
        void __addCollectionReference(string name, IList<string> references);
    }
}
