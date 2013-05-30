
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Membrane.Foundation.Runtime.Logging
{
    /// <summary>
	/// Interface for internal logger implementations.
	/// </summary>
	public interface ILoggerStrategy
	{
		/// <summary>
		/// Write a new log entry to the default category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		void Write(object message);

		/// <summary>
		/// Write a new log entry to the default category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="level">The level of the log entry.</param>
		void Write(object message, TraceLevel level);
 
		/// <summary>
		/// Write a new log entry to a specific collection of categories.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		void Write(object message, ICollection<string> categories);

		/// <summary>
		/// Write a new log entry to a specific collection of categories.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		/// <param name="level">The level of the log entry.</param>
		void Write(object message, ICollection<string> categories, TraceLevel level);

		/// <summary>
		/// Write a new log entry and a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		void Write(object message, IDictionary<string, object> properties);

		/// <summary>
		/// Write a new log entry and a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		/// <param name="level">The level of the log entry.</param>
		void Write(object message, IDictionary<string, object> properties, TraceLevel level);
 
		/// <summary>
		/// Write a new log entry to a specific category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="category"> Category name used to route the log entry to a one or more trace listeners.</param>
		void Write(object message, string category);

		/// <summary>
		/// Write a new log entry to a specific category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="category"> Category name used to route the log entry to a one or more trace listeners.</param>
		/// <param name="level">The level of the log entry.</param>
		void Write(object message, string category, TraceLevel level);
    
		/// <summary>
		/// Write a new log entry to a specific collection of categories with a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		void Write(object message, ICollection<string> categories, IDictionary<string, object> properties);

		/// <summary>
		/// Write a new log entry to a specific collection of categories with a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		/// <param name="level">The level of the log entry.</param>
		void Write(object message, ICollection<string> categories, IDictionary<string, object> properties, TraceLevel level);
	}
}
