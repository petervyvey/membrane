
using System;

namespace Membrane.Foundation.Model
{
	/// <summary>
	/// Interface defining the auditing API.
	/// </summary>
	public interface IAuditService
		: IDisposable
	{
		/// <summary>
		/// Audits a message to the workspace backend store.
		/// </summary>
		/// <param name="sessionRegistryID">The registered session ID.</param>
		/// <param name="entryType">The type of audit entry.</param>
		/// <param name="message">The audit message.</param>
		void Audit(long sessionRegistryID, AuditEntryType entryType, string message);

		/// <summary>
		/// Audits a message to the workspace back end store.
		/// </summary>
		/// <typeparam name="T">The type of data.</typeparam>
		/// <param name="sessionRegistryID">The registered session ID.</param>
		/// <param name="entryType">The type of audit entry.</param>
		/// <param name="message">The audit message.</param>
		/// <param name="data">The additional audit object.</param>
		void Audit<T>(long sessionRegistryID, AuditEntryType entryType, string message, T data);
	}
}
