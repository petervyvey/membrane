
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
	/// The repository for <see cref="OAuth2Scope"/> entity.
    /// </summary>
    public class OAuth2ScopeRepository
        : Repository<OAuth2Scope, Guid>, IOAuth2ScopeRepository 
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected OAuth2ScopeRepository()
            : base() { }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="context"></param>
		public OAuth2ScopeRepository(IEntityContext context)
            : base(context) { }

        #endregion
    }
}
