
using FluentNHibernate.Mapping;
using Membrane.Domain.Entity;

namespace Membrane.Domain.Store.NHibernate.Mapping
{

    /// <summary>
	/// The <see cref="OAuth2User"/> mapping
    /// </summary>
	public class IdentityMapping
        : ClassMap<Identity>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
		public IdentityMapping()
        {
            this.Id(e => e.ID).GeneratedBy.Assigned();

            this.Map(e => e.Name);
            this.Map(e => e.FirstName);
			this.Map(e => e.Locale);
            this.Map(e => e.CreationTimestamp);

			this.HasMany(e => e.UserIdentities).KeyColumn("IdentityID").Cascade.All().LazyLoad();
        }
    }

}
