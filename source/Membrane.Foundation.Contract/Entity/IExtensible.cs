
using System;
using System.Collections.Generic;

namespace Membrane.Foundation.Entity
{
    /// <summary>
	/// Interface defining the extensible API.
	/// </summary>
    public interface IExtensible
	{
        /// <summary>
        /// Container for the additional attributes attached to the instance.
        /// </summary>
        IDictionary<string, object> __extensions { get; set; }
	}
}
