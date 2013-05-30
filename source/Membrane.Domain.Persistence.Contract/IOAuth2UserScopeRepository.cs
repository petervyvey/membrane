
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
	/// The marker interface for <see cref="OAuth2UserScope"/> repositories.
    /// </summary>
	public interface IOAuth2UserScopeRepository
		: IRepository<OAuth2UserScope, Guid> { }
}
