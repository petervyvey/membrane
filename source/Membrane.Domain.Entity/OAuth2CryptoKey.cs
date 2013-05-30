
using System;
using Membrane.Foundation.Entity;
using System.Collections.Generic;

namespace Membrane.Domain.Entity
{

    /// <summary>
	/// The OAuthCryptoKey entity
    /// </summary>
    public partial class OAuth2CryptoKey
        : Entity<Guid>
    {
        #region - Properties -

        /// <summary>
		/// The Bucket.
        /// </summary>
        public virtual string Bucket { get; set; }

        /// <summary>
		/// The Handle.
        /// </summary>
        public virtual string Handle { get; set; }

        /// <summary>
		/// The Secret.
        /// </summary>
        public virtual string Secret { get; set; }

        /// <summary>
		/// The ExpiresUtc.
        /// </summary>
		public virtual DateTime ExpiresUtc { get; set; }

        #endregion

        #region - Public methods -

        #endregion
    }

}
