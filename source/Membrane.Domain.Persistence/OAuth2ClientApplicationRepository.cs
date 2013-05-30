
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
	/// The repository for <see cref="OAuth2ClientApplication"/> entity.
    /// </summary>
    public class OAuth2ClientApplicationRepository
		: Repository<OAuth2ClientApplication, Guid>, IOAuth2ClientApplicationRepository 
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected OAuth2ClientApplicationRepository()
            : base() { }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="context"></param>
		public OAuth2ClientApplicationRepository(IEntityContext context)
            : base(context) { }

        #endregion
    }
}
