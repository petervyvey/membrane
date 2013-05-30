
using System.Collections.Generic;
using Membrane.Foundation.Entity;

namespace Membrane.Foundation.DataTransfer
{
    /// <summary>
    /// Abstract base class for data transfer objects.
	/// </summary>
    public class DataTransferObject
		: IDataTransferObject
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataTransferObject()
            : base() { }

        #endregion
        
        /// <summary>
        /// Container with references.
        /// </summary>
        public References __references { get; set; }

        /// <summary>
        /// Container for the additional attributes mapped from the <see cref="Entity{TIdentifier}"/> to the <see cref="DataTransferObject"/> object.
        /// </summary>
        public IDictionary<string, object> __extensions { get; set; }
    }
}
