
using System;
using System.Collections.Generic;
using Membrane.Domain.Entity;
//using NHibernate.OData;

namespace Membrane.Domain.Agent
{
    /// <summary>
    /// Defines the API for application business logic.
    /// </summary>
    public interface IDemoApplicationAgent
    {
        /// <summary>
        /// Get the <see cref="ApplicationParameter"/> entity with the given identifier.
        /// </summary>
        /// <param name="identifier">The <see cref="ApplicationParameter"/> identifier.</param>
        /// <param name="filters">The parameters for entity selection.</param>
        /// <returns>The <see cref="ApplicationParameter"/> entity.</returns>
        DemoValue GetDemoValue(Guid identifier, IDictionary<string, string> filters = null);

        /// <summary>
        /// Get the <see cref="DemoValue"/> entity with the given identifier.
        /// </summary>
        /// <param name="filters">The parameters for entity selection.</param>
        /// <returns>The set of <see cref="DemoValue"/> entities.</returns>
        IList<DemoValue> GetDemoValues(IDictionary<string, string> filters = null);
    }
}
