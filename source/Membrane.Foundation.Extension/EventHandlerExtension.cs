
using System;
using System.Collections.Generic;

namespace Membrane.Foundation.Extension
{
	/// <summary>
	/// Extension methods to handle extra functionality on event handlers
	/// </summary>
	public static class EventHandlerExtension
	{
		/// <summary>
		/// Raises the event.
		/// </summary>
		/// <param name="handler">The event handler.</param>
		/// <param name="sender">The sender of the event.</param>
		public static void Raise(this EventHandler handler, object sender)
		{
			EventHandler local = handler;
			if (local != null)
			{
				local(sender, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Raises the event with the specified type of event arguments.
		/// </summary>
		/// <typeparam name="T">The type of the event arguments.</typeparam>
		/// <param name="handler">The event handler.</param>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="args">The event arguments.</param>
		public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
			where T : EventArgs
		{
			EventHandler<T> local = handler;
			if (local != null)
			{
				local(sender, args);
			}
		}
	}
}
