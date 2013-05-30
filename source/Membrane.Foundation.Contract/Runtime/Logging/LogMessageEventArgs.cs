
using System;
using System.Diagnostics;

namespace Membrane.Foundation.Runtime.Logging
{
	/// <summary>
	/// Event arguments used for log message notifications.
	/// </summary>
	public class LogMessageEventArgs 
        : EventArgs
	{
		#region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
		public LogMessageEventArgs()
			: base() { }

		/// <summary>
		/// Constructor with parameters.
		/// </summary>
		/// <param name="message">The logged message.</param>
		public LogMessageEventArgs(string message)
			: this()
		{
			this.Message = message;
		}

		/// <summary>
		/// Constructor with parameters.
		/// </summary>
		/// <param name="message">The logged message.</param>
		/// <param name="level">The category of the log message.</param>
		public LogMessageEventArgs(string message, TraceLevel level)
			: this(message)
		{
			this.Level = level;
		}

		#endregion

		/// <summary>
		/// The logged message.
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// The level of the log message.
		/// </summary>
		public TraceLevel Level { get; set; }

	}
}
