
using System;
using System.Collections.Generic;
//using NHibernate.OData;

namespace Membrane.Foundation.Model
{
    /// <summary>
    /// Interface defining the REST data portal API.
    /// </summary>
    /// <typeparam name="TDataTransferValue">The type of the data transfer value.</typeparam>
    /// <remarks>
    /// The generic type <typeparamref name="TDataTransferValue"/> is marked as OUT to make it covariant.
    /// This allows to cast for an implementation instance to be casted back to the base type of IDataPortal{DataTransferValue}. 
    /// </remarks>
    public interface IRestPortal<out TDataTransferValue>
        : IApplicationService 
        where TDataTransferValue : class, new()
    {
        /// <summary>
        /// Gets a data transfer value filtered by the given parameters.
        /// </summary>
        /// <param name="identifier">The data transfer value identifier.</param>
        /// <param name="queryExpression">The <see cref="ODataExpression"/> containing filter and order parameters.</param>
        /// <returns>The data transfer value.</returns>
        //TDataTransferValue Get(Guid identifier, ODataExpression queryExpression = null);
        TDataTransferValue Get(Guid identifier);

        /// <summary>
        /// Gets a set of data transfer values filtered by the given parameters.
        /// </summary>
        /// <param name="queryExpression">The <see cref="ODataExpression"/> containing query filter and entity order parameters.</param>
        /// <param name="parameters">The <see cref="IDictionary{String, String}"/> containing the additional request parameters.</param>
        /// <returns>The data transfer value of type T.</returns>
        //IEnumerable<TDataTransferValue> Get(ODataExpression queryExpression, IDictionary<string, string> parameters);
        IEnumerable<TDataTransferValue> Get(IDictionary<string, string> parameters);

        /// <summary>
        /// Posts a data transfer to the repository.
        /// </summary>
        /// <param name="data">The data transfer value as dynamic instance. The post data can be a partial data transfer value.</param>
        /// <returns>The data transfer value.</returns>
        TDataTransferValue Post(dynamic data);
    }
}
