
using System;
using Membrane.Foundation.Entity;
using System.Collections.Generic;

namespace Membrane.Domain.Entity
{

    /// <summary>
    /// The DemoValue entity
    /// </summary>
    public partial class DemoValue
        : Entity<Guid>
    {
        #region - Properties -

        /// <summary>
        /// The Code.
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// The Description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// The Value.
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// The CreationTimestamp.
        /// </summary>
        public virtual DateTime CreationTimestamp { get; set; }

        /// <summary>
        /// The ValidFrom.
        /// </summary>
        public virtual DateTime ValidFrom { get; set; }

        /// <summary>
        /// The ValidUntil.
        /// </summary>
        public virtual DateTime ValidUntil { get; set; }

        #endregion

        #region - Public methods -

        #endregion
    }

}
