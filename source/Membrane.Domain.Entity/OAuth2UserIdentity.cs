
using System;
using Membrane.Foundation.Entity;
using System.Collections.Generic;

namespace Membrane.Domain.Entity
{

    /// <summary>
	/// The <see cref="OAuth2UserScope"/> entity
    /// </summary>
	public partial class OAuth2UserIdentity
        : Entity<Guid>
    {
        #region - Properties -

		/// <summary>
		/// The Identifier.
		/// </summary>
		public virtual OAuth2User User { get; set; }

		/// <summary>
		/// The Secret.
		/// </summary>
		public virtual Identity Identity { get; set; }

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
