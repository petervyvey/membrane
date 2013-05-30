
using FluentNHibernate.Mapping;
using Membrane.Domain.Entity;

namespace Membrane.Domain.Store.NHibernate.Mapping
{

    /// <summary>
	/// The <see cref="OAuth2UserScope"/> mapping
    /// </summary>
	public class OAuth2UserScopeMapping
		: ClassMap<OAuth2UserScope>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
		public OAuth2UserScopeMapping()
        {
            this.Id(e => e.ID).GeneratedBy.Assigned();

            this.Map(e => e.CreationTimestamp);
            this.Map(e => e.ValidFrom);
			this.Map(e => e.ValidUntil);

			this.References<OAuth2User>(e => e.User).Column("UserID").Cascade.All().Not.LazyLoad().Fetch.Join();
			this.References<OAuth2Scope>(e => e.Scope).Column("ScopeID").Cascade.All().Not.LazyLoad().Fetch.Join();
        }
    }

}
