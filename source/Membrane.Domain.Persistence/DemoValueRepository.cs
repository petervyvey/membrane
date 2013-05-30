
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
    /// The repository for <see cref="DemoValue"/> entity.
    /// </summary>
    public class DemoValueRepository
        : Repository<DemoValue, Guid>, IDemoValueRepository 
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected DemoValueRepository()
            : base() { }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="context"></param>
        public DemoValueRepository(IEntityContext context)
            : base(context) { }

        #endregion
    }
}
