
using System;

namespace Membrane.Foundation.Security.Authentication
{
	/// <summary>
    /// Interface defining the authentication service.
    /// </summary>
    public interface IAuthenticationService
        : IAuthenticationProvider, IDisposable { }
}
