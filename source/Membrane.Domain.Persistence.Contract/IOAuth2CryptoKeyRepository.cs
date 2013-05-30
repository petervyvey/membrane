
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
    /// The marker interface for <see cref="OAuth2CryptoKey"/> repositories.
    /// </summary>
    public interface IOAuth2CryptoKeyRepository
        : IRepository<OAuth2CryptoKey, Guid> { }
}
