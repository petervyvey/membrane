
using System;
using Membrane.Foundation.Entity;
using System.Collections.Generic;

namespace Membrane.Domain.Entity
{

    /// <summary>
	/// The OAuthClientApplication entity
    /// </summary>
	public partial class OAuth2ClientApplication
        : Entity<Guid>
    {
        #region - Properties -

		/// <summary>
		/// The Description.
		/// </summary>
		public virtual string Description { get; set; }

		/// <summary>
		/// The Identifier.
		/// </summary>
		public virtual string Identifier { get; set; }

		/// <summary>
		/// The Secret.
		/// </summary>
		public virtual string Secret { get; set; }

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
