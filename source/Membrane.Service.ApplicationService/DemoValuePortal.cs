
using System;
using System.Collections.Generic;
using System.Linq;
using Membrane.DataTransfer;
using Membrane.DataTransfer.Projection;
using Membrane.Domain.Agent;
using Membrane.Foundation.Model;
using Membrane.Foundation.Pattern.Creational;
using Membrane.Foundation.Pattern.Enterprise;
using Membrane.Foundation.Security.Authentication;
//using NHibernate.OData;

namespace Membrane.Service.ApplicationService
{
    /// <summary>
    /// The <see cref="DemoValue"/> portal.
    /// </summary>
    public class DemoValuePortal
        : RestPortal<DemoValue>
    {
        #region - Public methods -

        /// <summary>
        /// Gets the <see cref="DemoValue"/> data transfer value with the given identifier and filtered by the given parameters.
        /// </summary>
        /// <param name="identifier">The data transfer value identifier.</param>
        /// <param name="queryExpression">The <see cref="ODataExpression"/> containing filter and order parameters.</param>
        /// <returns>The <see cref="DemoValue"/> data transfer value.</returns>
        //public override DemoValue Get(Guid identifier, ODataExpression queryExpression = null)
        public override DemoValue Get(Guid identifier)
        {
            DataTransfer.DemoValue value =
                UnitOfWork.Execute<DataTransfer.DemoValue>(() =>
                    {
                        //SecurityToken identity = ApplicationModel.Current.Session.Identity;

                        IDemoApplicationAgent agent = DependencyInjection.Get<IDemoApplicationAgent>();
                        Domain.Entity.DemoValue entity = agent.GetDemoValue(identifier);
                        DataTransfer.DemoValue _value = entity.ToDataTransferValue();

                        return _value;
                    });

            return value;
        }

        /// <summary>
        /// Gets the set <see cref="DemoValue"/> data transfer values with the given identifier and filtered by the given parameters.
        /// </summary>
        /// <param name="queryExpression">The <see cref="ODataExpression"/> containing query filter and entity order parameters.</param>
        /// <param name="parameters">The <see cref="IDictionary{String, String}"/> containing the additional request parameters.</param>
        /// <returns>The set <see cref="DemoValue"/> data transfer values.</returns>
        //public override IEnumerable<DemoValue> Get(ODataExpression queryExpression, IDictionary<string, string> parameters)
        public override IEnumerable<DemoValue> Get(IDictionary<string, string> parameters)
        {
            IList<DemoValue> set =
                UnitOfWork.Execute<IList<DataTransfer.DemoValue>>(() =>
                {
                    //SecurityToken identity = ApplicationModel.Current.Session.Identity;

                    IDemoApplicationAgent agent = DependencyInjection.Get<IDemoApplicationAgent>();
                    IList<Domain.Entity.DemoValue> _set = agent.GetDemoValues(parameters);
                    IList<DataTransfer.DemoValue> values = _set.ToDataTransferSet();

                    return values;
                });

            return set;
        }

        #endregion

        public override DemoValue Put(dynamic data)
        {
            throw new NotImplementedException();
        }

        public override DemoValue Post(dynamic data)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid identifier)
        {
            throw new NotImplementedException();
        }
    }
}
