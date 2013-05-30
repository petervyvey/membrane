
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
	/// The marker interface for <see cref="Identity"/> repositories.
    /// </summary>
	public interface IIdentityRepository
        : IRepository<Identity, Guid> { }
}
