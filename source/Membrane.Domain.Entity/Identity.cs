
using System;
using Membrane.Foundation.Entity;
using System.Collections.Generic;

namespace Membrane.Domain.Entity
{

    /// <summary>
	/// The OAuthUser entity
    /// </summary>
	public partial class Identity
        : Entity<Guid>
    {
        #region - Properties -

		/// <summary>
		/// The Identifier.
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// The Secret.
		/// </summary>
		public virtual string FirstName { get; set; }

		/// <summary>
		/// The Locale.
		/// </summary>
		public virtual string Locale { get; set; }

		/// <summary>
		/// The CreationTimestamp.
		/// </summary>
		public virtual DateTime CreationTimestamp { get; set; }

		/// <summary>
		/// Set of <see cref="OAuth2UserIdentity" /> entities.
		/// </summary>
		public virtual IList<OAuth2UserIdentity> UserIdentities { get; set; }

        #endregion

        #region - Public methods -

        #endregion
    }

}
