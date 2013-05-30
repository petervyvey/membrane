
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
	/// The repository for <see cref="OAuth2UserScope"/> entity.
    /// </summary>
    public class OAuth2UserScopeRepository
        : Repository<OAuth2UserScope, Guid>, IOAuth2UserScopeRepository 
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected OAuth2UserScopeRepository()
            : base() { }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="context"></param>
		public OAuth2UserScopeRepository(IEntityContext context)
            : base(context) { }

        #endregion
    }
}
