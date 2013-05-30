
using FluentNHibernate.Mapping;
using Membrane.Domain.Entity;

namespace Membrane.Domain.Store.NHibernate.Mapping
{

    /// <summary>
    /// The DemoValue mapping
    /// </summary>
    public class DemoValueMapping
        : ClassMap<DemoValue>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DemoValueMapping()
        {
            this.Id(e => e.ID).GeneratedBy.Assigned();

            this.Map(e => e.Code);
            this.Map(e => e.Description);
            this.Map(e => e.Value);
            this.Map(e => e.CreationTimestamp);
            this.Map(e => e.ValidFrom);
            this.Map(e => e.ValidUntil);



        }
    }

}
