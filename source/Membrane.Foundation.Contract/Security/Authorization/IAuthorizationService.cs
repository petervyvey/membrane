
using System;

namespace Membrane.Foundation.Security.Authorization
{
	/// <summary>
	/// Interface defining the authorization service.
    /// </summary>
	public interface IAuthorizationService
        : IAuthorizationProvider, IDisposable { }
}
