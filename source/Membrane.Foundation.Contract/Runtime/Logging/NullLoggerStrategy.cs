
#region - Using -

using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace Membrane.Foundation.Runtime.Logging
{
	/// <summary>
	/// ILogger implementation without any logging infrastructure, messages send to this logger are not logged.
	/// </summary>
	public class NullLoggerStrategy 
        : LoggerStrategyBase
	{
		#region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NullLoggerStrategy()
			: base() { }

		#endregion

		/// <summary>
		/// Write a new log entry to a specific collection of categories with a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		/// <param name="level">The level of the log entry.</param>
		public override void Write(object message, ICollection<string> categories, IDictionary<string, object> properties, TraceLevel level) { }

	}
}
