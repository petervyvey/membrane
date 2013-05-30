
using System.Collections.Generic;

namespace Membrane.Foundation.Entity
{
    /// <summary>
    /// Containes the collection of pointers to related entities/data values.
    /// </summary>
    public interface IReferenceContainerOperation
    {
        /// <summary>
        /// Adds a reference to the Members collection.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="reference">The reference value of the </param>
        void AddMember(string name, string reference);

        /// <summary>
        /// Adds a reference to the Collections collection.
        /// </summary>
        /// <param name="name">The name of the collection.</param>
        /// <param name="collection">The collection of reference values.</param>
        void AddCollection(string name, IList<string> collection);
    }
}
