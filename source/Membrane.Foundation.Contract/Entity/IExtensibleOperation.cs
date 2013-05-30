
using System;
using System.Collections.Generic;

namespace Membrane.Foundation.Entity
{
    /// <summary>
	/// Interface defining the extensible API.
	/// </summary>
    public interface IExtensibleOperation
	{
        /// <summary>
        /// Adds an extension to the entity.
        /// </summary>
        /// <param name="name">The extension name.</param>
        /// <param name="value">The extension value.</param>
        void __addExtension(string name, object value);
	}
}
