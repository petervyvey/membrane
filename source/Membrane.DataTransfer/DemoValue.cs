
using System;
using System.Collections.Generic;
using Membrane.Foundation.DataTransfer;

namespace Membrane.DataTransfer
{

    /// <summary>
    /// The <see cref="DemoValue"/> data value.
    /// </summary>
    public class DemoValue
        : DataTransferObject
    {

        /// <summary>
        /// The ID.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// The Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The CreationTimestamp.
        /// </summary>
        public DateTime CreationTimestamp { get; set; }

        /// <summary>
        /// The ValidFrom.
        /// </summary>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// The ValidUntil.
        /// </summary>
        public DateTime ValidUntil { get; set; }

    }

}
