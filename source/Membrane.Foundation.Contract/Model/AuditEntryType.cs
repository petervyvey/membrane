
using System;

namespace Membrane.Foundation.Model
{
	/// <summary>
    /// The supported audit entry types.
    /// </summary>
	public enum AuditEntryType
	{
		/// <summary>
		/// Unspecified.
		/// </summary>
		Unspecified = 0,

		/// <summary>
		/// Security action.
		/// </summary>
		Security = 1,

		/// <summary>
		/// User action.
		/// </summary>
		User = 2
	}
}
