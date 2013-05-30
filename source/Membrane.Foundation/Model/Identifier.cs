
using System;
using Membrane.Foundation.Pattern.Creational;

namespace Membrane.Foundation.Model
{
    /// <summary>
    /// Identifier operations.
    /// </summary>
    public static class Identifier
    {
        /// <summary>
        /// Creates a random identifier.
        /// </summary>
        /// <remarks>
        /// There is a very low probability that the value of the new id is equal to any other id.
        /// </remarks>
        /// <returns>The ID.</returns>
        public static Guid NewID()
        {
			return Guid.NewGuid();
        }
    }
}

