
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
    /// The repository for <see cref="OAuth2CryptoKey"/> entity.
    /// </summary>
    public class OAuth2CryptoKeyRepository
        : Repository<OAuth2CryptoKey, Guid>, IOAuth2CryptoKeyRepository 
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected OAuth2CryptoKeyRepository()
            : base() { }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="context"></param>
		public OAuth2CryptoKeyRepository(IEntityContext context)
            : base(context) { }

        #endregion
    }
}
