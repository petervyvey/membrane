
using System;
using System.Collections.Generic;
using System.Linq;
using Membrane.Domain.Entity;
using Membrane.Domain.Persistence;
using Membrane.Domain.Store;
using Membrane.Foundation.Pattern.Creational;
using NHibernate;

namespace Membrane.Domain.Agent
{
    /// <summary>
    /// Implementation of <see cref="IDemoApplicationAgent"/> API.
    /// </summary>
    public class DemoApplicationAgent :
        IDemoApplicationAgent
    {
        #region - Public methods -

        #region - DemoValue -

        /// <summary>
        /// Get the <see cref="DemoValue"/> entity with the given identifier.
        /// </summary>
        /// <param name="identifier">The <see cref="DemoValue"/> identifier.</param>
        /// <param name="filters">The parameters for entity selection.</param>
        /// <returns>The <see cref="DemoValue"/> entitie with the given identifier.</returns>
        public DemoValue GetDemoValue(Guid identifier, IDictionary<string, string> filters = null)
        {
            DemoValue entity = default(DemoValue);

            using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
            {
                using (IDemoValueRepository repository = DependencyInjection.Get<IDemoValueRepository>(InjectionParameter.Create("context", context)))
                {
                    IQueryOver<DemoValue, DemoValue> query =
                        repository
                            .Query()
                            .Where(x => x.ID == identifier);

                    entity = query.List().SingleOrDefault();

                    // The next line is just for demo purposes.
                    //set.ForEach(item => item.AddCollectionReference(Corporation.Metadata.SUBSIDUARIES_PROPERTY, item.Subsiduaries.Select(e => e.ID.ToString()).ToList()));
                }

                context.Commit();
            }

            return entity;
        }

        /// <summary>
        /// Get the <see cref="DemoValue"/> entity with the given identifier.
        /// </summary>
        /// <param name="filters">The parameters for entity selection.</param>
        /// <returns>The set of <see cref="DemoValue"/> entities.</returns>
        public IList<DemoValue> GetDemoValues(IDictionary<string, string> filters = null)
        {
            IList<DemoValue> set = default(IList<DemoValue>);

            using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
            {
                using (IDemoValueRepository repository = DependencyInjection.Get<IDemoValueRepository>(InjectionParameter.Create("context", context)))
                {
                    IQueryOver<DemoValue, DemoValue> query =
                        repository
                            .Query();

                    set = query.List();

                    // The next line is just for demo purposes.
                    //set.ForEach(item => item.AddCollectionReference(Corporation.Metadata.SUBSIDUARIES_PROPERTY, item.Subsiduaries.Select(e => e.ID.ToString()).ToList()));
                }

                context.Commit();
            }

            return set;
        }

        #endregion

        #endregion
    }
}
