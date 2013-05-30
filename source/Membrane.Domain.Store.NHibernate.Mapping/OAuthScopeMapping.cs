
using FluentNHibernate.Mapping;
using Membrane.Domain.Entity;

namespace Membrane.Domain.Store.NHibernate.Mapping
{

    /// <summary>
	/// The <see cref="OAuth2UserScope"/> mapping
    /// </summary>
	public class OAuth2ScopeMapping
		: ClassMap<OAuth2Scope>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
		public OAuth2ScopeMapping()
        {
            this.Id(e => e.ID).GeneratedBy.Assigned();

            this.Map(e => e.Code);
            this.Map(e => e.Description);
            this.Map(e => e.CreationTimestamp);
            this.Map(e => e.ValidFrom);
			this.Map(e => e.ValidUntil);
        }
    }

}
