
using System.Runtime.Serialization;

namespace Membrane.Foundation.ExceptionManagement
{
	/// <summary>
	/// Enumeration with the exception numbers. These are always propagated to the presentation layer.
	/// </summary>
	public enum ExceptionNumber
	{
		#region - Workspace -

		/// <summary>
		/// UnexpectedError.
		/// </summary>
		UnexpectedError = 0x00000100,

		/// <summary>
		/// MissingConfigurationFile.
		/// </summary>
		MissingConfigurationFile = 0x00000200,

		#endregion

		#region - Session -

		/// <summary>
		/// EmptyCredentials.
		/// </summary>
		EmptyCredentials = 0x01000100,

		/// <summary>
		/// InvalidCredentials.
		/// </summary>
		InvalidCredentials = 0x01000110,

		/// <summary>
		/// DuplicateSession.
		/// </summary>
		DuplicateSession = 0x01000120,

		/// <summary>
		/// InvalidSession.
		/// </summary>
		InvalidSession = 0x01000130,

		/// <summary>
		/// SessionExpired.
		/// </summary>
		SessionExpired = 0x01000140,

		#endregion

		#region - Security -

		/// <summary>
		/// ContextNotEmpty.
		/// </summary>
		ContextNotEmpty = 0x02000100,

		/// <summary>
		/// ContextEmpty.
		/// </summary>
		ContextEmpty = 0x02000110,

		/// <summary>
		/// InvalidContext.
		/// </summary>
		InvalidContext = 0x02000120

		#endregion
	}
}
