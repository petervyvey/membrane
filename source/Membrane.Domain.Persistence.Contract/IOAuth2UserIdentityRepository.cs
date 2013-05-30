
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
	/// The marker interface for <see cref="OAuth2UserIdentity"/> repositories.
    /// </summary>
	public interface IOAuth2UserIdentityRepository
		: IRepository<OAuth2UserIdentity, Guid> { }
}
