
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
    /// The marker interface for <see cref="OAuth2ClientApplication"/> repositories.
    /// </summary>
	public interface IOAuth2ClientApplicationRepository
        : IRepository<OAuth2ClientApplication, Guid> { }
}
