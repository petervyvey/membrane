
using System;

namespace Membrane.Foundation.Security.Authorization
{
	/// <summary>
    /// Interface defining the authorization methods.
    /// </summary>
	public interface IAuthorizationProvider
	{
		/// <summary>
		/// Gets the permission on a resource for a user.
		/// </summary>
		/// <param name="userID">The user ID.</param>
		/// <param name="resourcePath">The path of the resource.</param>
		/// <returns>The permission.</returns>
		Permission GetPermission(string userID, string resourcePath);
	}
}
