
using FluentNHibernate.Mapping;
using Membrane.Domain.Entity;

namespace Membrane.Domain.Store.NHibernate.Mapping
{

    /// <summary>
	/// The <see cref="OAuth2User"/> mapping
    /// </summary>
	public class OAuth2UserMapping
        : ClassMap<OAuth2User>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
		public OAuth2UserMapping()
        {
            this.Id(e => e.ID).GeneratedBy.Assigned();

            this.Map(e => e.Identifier);
            this.Map(e => e.Password);
            this.Map(e => e.CreationTimestamp);
			this.Map(e => e.IsAdministrator);
            this.Map(e => e.ValidFrom);
			this.Map(e => e.ValidUntil);

			this.HasMany(e => e.UserScopes).KeyColumn("UserID").Cascade.All().LazyLoad();
			this.HasMany(e => e.UserIdentities).KeyColumn("UserID").Cascade.All().LazyLoad();
        }
    }

}
