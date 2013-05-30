
using System;

namespace Membrane.Foundation.Security.Authentication
{
    /// <summary>
    /// Interface defining the authentication methods.
    /// </summary>
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="credential">The user credentials.</param>
        /// <returns>The user's <see cref="Identity"/>.</returns>
        Identity Authenticate(Credential credential);

		/// <summary>
		/// Tries to authenticate a user with the given credentials.
		/// </summary>
		/// <param name="credential">The user credentials.</param>
		/// <param name="identity">The user identity.</param>
		/// <returns>TRUE if the user can be authenticated with the credentials, FALSE otherwise.</returns>
		bool TryAuthenticate(Credential credential, out Identity identity);
    }
}
