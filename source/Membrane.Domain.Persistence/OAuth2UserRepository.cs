
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
	/// The repository for <see cref="OAuth2User"/> entity.
    /// </summary>
    public class OAuth2UserRepository
        : Repository<OAuth2User, Guid>, IOAuth2UserRepository 
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected OAuth2UserRepository()
            : base() { }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="context"></param>
		public OAuth2UserRepository(IEntityContext context)
            : base(context) { }

        #endregion
    }
}
