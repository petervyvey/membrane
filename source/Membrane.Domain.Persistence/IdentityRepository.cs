
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
	/// The repository for <see cref="Identity"/> entity.
    /// </summary>
    public class IdentityRepository
        : Repository<Identity, Guid>, IIdentityRepository 
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected IdentityRepository()
            : base() { }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="context"></param>
		public IdentityRepository(IEntityContext context)
            : base(context) { }

        #endregion
    }
}
