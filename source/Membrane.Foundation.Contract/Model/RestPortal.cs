
using System;
using System.Collections.Generic;
//using NHibernate.OData;

namespace Membrane.Foundation.Model
{
    /// <summary>
    /// The base implementation of <see cref="IRestPortal{TDataTransferValue}"/>.
    /// </summary>
    /// <typeparam name="TDataTransferValue">The type of the data transfer value.</typeparam>
    public abstract class RestPortal<TDataTransferValue>
        : IRestPortal<TDataTransferValue> where TDataTransferValue : class, new()
    {
        /// <summary>
        /// Gets a data transfer value filtered by the given parameters.
        /// </summary>
        /// <param name="identifier">The data transfer value identifier.</param>
        /// <param name="queryExpression">The <see cref="ODataExpression"/> containing filter and order parameters.</param>
        /// <returns>The data transfer value of type T.</returns>
        //public abstract TDataTransferValue Get(Guid identifier, ODataExpression queryExpression = null);
        public abstract TDataTransferValue Get(Guid identifier);

        /// <summary>
        /// Gets a set of data transfer values filtered by the given parameters.
        /// </summary>
        /// <param name="queryExpression">The <see cref="ODataExpression"/> containing query filter and entity order parameters.</param>
        /// <param name="parameters">The <see cref="IDictionary{String, String}"/> containing the additional request parameters.</param>
        /// <returns>The data transfer value of type T.</returns>
        //public abstract IEnumerable<TDataTransferValue> Get(ODataExpression queryExpression, IDictionary<string, string> parameters);
        public abstract IEnumerable<TDataTransferValue> Get(IDictionary<string, string> parameters);

        /// <summary>
        /// Puts/inserts a data transfer value to the repository.
        /// </summary>
        /// <param name="data">The data transfer value as dynamic instance. The post data can be a partial data transfer value.</param>
        /// <returns>The data transfer value.</returns>
        public abstract TDataTransferValue Put(dynamic data);

        /// <summary>
        /// Posts/updates a data transfer value to the repository.
        /// </summary>
        /// <param name="data">The data transfer value as dynamic instance. The post data can be a partial data transfer value.</param>
        /// <returns>The data transfer value.</returns>
        public abstract TDataTransferValue Post(dynamic data);

        /// <summary>
        /// Deletes a data transfer value from the repository.
        /// </summary>
        /// <param name="identifier">The data transfer value identifier.</param>
        public abstract void Delete(Guid identifier);
    }
}
