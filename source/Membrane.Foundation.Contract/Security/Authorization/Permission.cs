
using System;
using System.Runtime.Serialization;

namespace Membrane.Foundation.Security.Authorization
{
	/// <summary>
	/// The permission associated with a value object.
	/// </summary>
	[Flags]
	public enum Permission
	{
		/// <summary>
		/// No access.
		/// </summary>
		NoAccess = 0,

		/// <summary>
		/// Traverse.
		/// </summary>
		Traverse = 1,

		/// <summary>
		/// Read.
		/// </summary>
		Read = 2 | Traverse,

		/// <summary>
		/// Write.
		/// </summary>
		Write = 4 | Read,

		/// <summary>
		/// Manage.
		/// </summary>
		Manage = 8 | Write,
	}
}
