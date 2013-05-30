
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Membrane.Foundation.Runtime.Logging
{
	/// <summary>
	/// Base class implementation of ILoggerConnector.
	/// </summary>
	public abstract class LoggerStrategyBase 
        : ILoggerStrategy
	{
		#region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
		protected LoggerStrategyBase()
			: base() { }

		#endregion

		/// <summary>
		/// Write a new log entry to the default category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		public virtual void Write(object message)
		{
			this.Write(message, Logger.DEFAULT_LEVEL);
		}

		/// <summary>
		/// Write a new log entry to the default category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="level">The level of the log entry.</param>
		public virtual void Write(object message, TraceLevel level)
		{
			this.Write(message, new Collection<string>(), level);
		}

		/// <summary>
		/// Write a new log entry to a specific collection of categories.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		public virtual void Write(object message, ICollection<string> categories)
		{
			this.Write(message, categories, Logger.DEFAULT_LEVEL);
		}

		/// <summary>
		/// Write a new log entry to a specific collection of categories.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		/// <param name="level">The level of the log entry.</param>
		public virtual void Write(object message, ICollection<string> categories, TraceLevel level)
		{
			this.Write(message, categories, new Dictionary<string, object>(), level);
		}

		/// <summary>
		/// Write a new log entry and a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		public virtual void Write(object message, IDictionary<string, object> properties)
		{
			this.Write(message, properties, Logger.DEFAULT_LEVEL);
		}

		/// <summary>
		/// Write a new log entry and a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		/// <param name="level">The level of the log entry.</param>
		public virtual void Write(object message, IDictionary<string, object> properties, TraceLevel level)
		{
			this.Write(message, new Collection<string>(), properties, level);
		}

		/// <summary>
		/// Write a new log entry to a specific category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="category"> Category name used to route the log entry to a one or more trace listeners.</param>
		public virtual void Write(object message, string category)
		{
			this.Write(message, category, Logger.DEFAULT_LEVEL);
		}

		/// <summary>
		/// Write a new log entry to a specific category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="category"> Category name used to route the log entry to a one or more trace listeners.</param>
		/// <param name="level">The level of the log entry.</param>
		public virtual void Write(object message, string category, TraceLevel level)
		{
			this.Write(message, new Collection<string> { category }, new Dictionary<string, object>(), level);
		}

		/// <summary>
		/// Write a new log entry to a specific collection of categories with a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		public virtual void Write(object message, ICollection<string> categories, IDictionary<string, object> properties)
		{
			this.Write(message, categories, properties, Logger.DEFAULT_LEVEL);
		}

		/// <summary>
		/// Write a new log entry to a specific collection of categories with a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		/// <param name="level">The level of the log entry.</param>
		public abstract void Write(object message, ICollection<string> categories, IDictionary<string, object> properties, TraceLevel level);

	}
}
