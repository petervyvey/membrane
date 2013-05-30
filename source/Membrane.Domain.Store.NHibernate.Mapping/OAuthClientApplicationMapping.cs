
using FluentNHibernate.Mapping;
using Membrane.Domain.Entity;

namespace Membrane.Domain.Store.NHibernate.Mapping
{

    /// <summary>
	/// The OAuthClientApplication mapping
    /// </summary>
    public class OAuth2ClientApplicationMapping
        : ClassMap<OAuth2ClientApplication>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
		public OAuth2ClientApplicationMapping()
        {
            this.Id(e => e.ID).GeneratedBy.Assigned();

            this.Map(e => e.Description);
            this.Map(e => e.Identifier);
            this.Map(e => e.Secret);
            this.Map(e => e.CreationTimestamp);
			this.Map(e => e.ValidFrom);
			this.Map(e => e.ValidUntil);
        }
    }

}
