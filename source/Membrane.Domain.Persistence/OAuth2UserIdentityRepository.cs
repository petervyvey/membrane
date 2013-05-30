
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
	/// The repository for <see cref="OAuth2UserIdentity"/> entity.
    /// </summary>
    public class OAuth2UserIdentityRepository
        : Repository<OAuth2UserIdentity, Guid>, IOAuth2UserIdentityRepository 
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected OAuth2UserIdentityRepository()
            : base() { }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="context"></param>
		public OAuth2UserIdentityRepository(IEntityContext context)
            : base(context) { }

        #endregion
    }
}
