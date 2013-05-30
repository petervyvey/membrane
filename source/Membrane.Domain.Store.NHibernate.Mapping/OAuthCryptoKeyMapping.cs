
using FluentNHibernate.Mapping;
using Membrane.Domain.Entity;

namespace Membrane.Domain.Store.NHibernate.Mapping
{

    /// <summary>
	/// The OAuthCryptoKey mapping
    /// </summary>
    public class OAuth2CryptoKeyMapping
        : ClassMap<OAuth2CryptoKey>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
		public OAuth2CryptoKeyMapping()
        {
            this.Id(e => e.ID).GeneratedBy.Assigned();

            this.Map(e => e.Bucket);
            this.Map(e => e.Handle);
            this.Map(e => e.Secret);
            this.Map(e => e.ExpiresUtc);
        }
    }

}
