
using FluentNHibernate.Mapping;
using Membrane.Domain.Entity;

namespace Membrane.Domain.Store.NHibernate.Mapping
{

    /// <summary>
	/// The <see cref="OAuth2UserScope"/> mapping
    /// </summary>
	public class OAuth2UserIdentityMapping
		: ClassMap<OAuth2UserIdentity>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
		public OAuth2UserIdentityMapping()
        {
            this.Id(e => e.ID).GeneratedBy.Assigned();

            this.Map(e => e.CreationTimestamp);
            this.Map(e => e.ValidFrom);
			this.Map(e => e.ValidUntil);

			this.References<OAuth2User>(e => e.User).Column("UserID").Cascade.All().Not.LazyLoad().Fetch.Join();
			this.References<Identity>(e => e.Identity).Column("IdentityID").Cascade.All().Not.LazyLoad().Fetch.Join();
        }
    }

}
