
using System;
using Membrane.Foundation.Entity;
using System.Collections.Generic;

namespace Membrane.Domain.Entity
{

    /// <summary>
	/// The OAuthUser entity
    /// </summary>
	public partial class OAuth2User
        : Entity<Guid>
    {
        #region - Properties -

		/// <summary>
		/// The Identifier.
		/// </summary>
		public virtual string Identifier { get; set; }

		/// <summary>
		/// The Secret.
		/// </summary>
		public virtual string Password { get; set; }

		/// <summary>
		/// The Secret.
		/// </summary>
		public virtual bool IsAdministrator { get; set; }

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

		/// <summary>
		/// Set of <see cref="OAuth2UserScope" /> entities.
		/// </summary>
		public virtual IList<OAuth2UserScope> UserScopes { get; set; }

		/// <summary>
		/// Set of <see cref="OAuth2UserIdentity" /> entities.
		/// </summary>
		public virtual IList<OAuth2UserIdentity> UserIdentities { get; set; }

        #endregion

        #region - Public methods -

        #endregion
    }

}
