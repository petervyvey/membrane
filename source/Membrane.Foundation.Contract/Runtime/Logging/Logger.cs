
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Membrane.Foundation.Runtime.Logging
{
	/// <summary>
	/// Application logger service.
	/// </summary>
	public static class Logger
	{
		#region - Constants & static fields -

		/// <summary>
		/// The default trace level.
		/// </summary>
		public static TraceLevel DEFAULT_LEVEL = TraceLevel.Info;

		/// <summary>
		/// The minimum level for the MessageLogged event to be raised.
		/// </summary>
		public static TraceLevel MinimumTraceLevel = TraceLevel.Error;

		#endregion

		#region - Events -

		/// <summary>
		/// Event raised when a message is logged.
		/// </summary>
		public static event EventHandler<LogMessageEventArgs> MessageLogged;

		#endregion

		#region - Private static fields -
		/// <summary>
		/// Internal instance of an <see cref="ILoggerStrategy"/> implementation.
		/// </summary>
		private static ILoggerStrategy logger = new NullLoggerStrategy();
		
		#endregion
		
		/// <summary>
		/// Write a new log entry to the default category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		public static void Write(object message)
		{
			Logger.logger.Write(message, Logger.DEFAULT_LEVEL);
		}

		/// <summary>
		/// Write a new log entry to the default category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="level">The level of the log entry.</param>
		public static void Write(object message, TraceLevel level)
		{

			Logger.NotifyMessageLoggedSubscribers(message, level);
			Logger.logger.Write(message, level);
		}
 
		/// <summary>
		/// Write a new log entry to a specific collection of categories.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		public static void Write(object message, ICollection<string> categories)
		{
			Logger.Write(message, categories, Logger.DEFAULT_LEVEL);
		}

		/// <summary>
		/// Write a new log entry to a specific collection of categories.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		/// <param name="level">The level of the log entry.</param>
		public static void Write(object message, ICollection<string> categories, TraceLevel level)
		{
			Logger.NotifyMessageLoggedSubscribers(message, level);
			Logger.logger.Write(message, categories, level);
		}

		/// <summary>
		/// Write a new log entry and a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		public static void Write(object message, IDictionary<string, object> properties)
		{
			Logger.Write(message, properties, Logger.DEFAULT_LEVEL);
		}

		/// <summary>
		/// Write a new log entry and a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		/// <param name="level">The level of the log entry.</param>
		public static void Write(object message, IDictionary<string, object> properties, TraceLevel level)
		{
			Logger.NotifyMessageLoggedSubscribers(message, level);
			Logger.logger.Write(message, properties, level);
		}
 
		/// <summary>
		/// Write a new log entry to a specific category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="category"> Category name used to route the log entry to a one or more trace listeners.</param>
		public static void Write(object message, string category)
		{
			Logger.Write(message, category, Logger.DEFAULT_LEVEL);
		}

		/// <summary>
		/// Write a new log entry to a specific category.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="category"> Category name used to route the log entry to a one or more trace listeners.</param>
		/// <param name="level">The level of the log entry.</param>
		public static void Write(object message, string category, TraceLevel level)
		{
			Logger.NotifyMessageLoggedSubscribers(message, level);
			Logger.logger.Write(message, category, level);
		}
    
		/// <summary>
		/// Write a new log entry to a specific collection of categories with a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		public static void Write(object message, ICollection<string> categories, IDictionary<string, object> properties)
		{
			Logger.Write(message, categories, properties, Logger.DEFAULT_LEVEL);
		}

		/// <summary>
		/// Write a new log entry to a specific collection of categories with a dictionary of extended properties.
		/// </summary>
		/// <param name="message">Message body to log. Value from ToString() method from message object.</param>
		/// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
		/// <param name="properties">Dictionary of key/value pairs to log.</param>
		/// <param name="level">The level of the log entry.</param>
		public static void Write(object message, ICollection<string> categories, IDictionary<string, object> properties, TraceLevel level)
		{
			Logger.NotifyMessageLoggedSubscribers(message, level);
			Logger.logger.Write(message, categories, properties, level);
		}

		#region - Private & protected static methods -

		private static void NotifyMessageLoggedSubscribers(object message, TraceLevel level)
		{
			if (level <= Logger.MinimumTraceLevel)
			{
				Logger.OnMessageLogged(new LogMessageEventArgs(message.ToString(), level));
			}
		}

		private static void OnMessageLogged(LogMessageEventArgs e)
		{
			EventHandler<LogMessageEventArgs> handler = Logger.MessageLogged;

			if (null != handler)
			{
				handler(null, e);
			}
		}

		#endregion
	}
}
