
using System;
using System.Collections.Generic;
using Membrane.Foundation.Entity;
using System.Reflection;

namespace Membrane.Domain.Entity
{
    public partial class DemoValue
        : Entity<Guid>
    {
        #region - Class: Metadata -

        /// <summary>
        /// The <see cref="DemoValue" /> metadata.
        /// </summary>
        public static class Metadata
        {

            /// <summary>
            /// The <see cref="DemoValue" /> public properties.
            /// </summary>
            public static readonly PropertyInfo[] PROPERTIES = typeof(Domain.Entity.DemoValue).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            /// <summary>
            /// The name of the entity.
            /// </summary>
            public static readonly string NAME = typeof(DemoValue).Name;

            /// <summary>
            /// The ID property marker.
            /// </summary>
            public static readonly string ID_PROPERTY = "ID";

            /// <summary>
            /// The ID property path.
            /// </summary>
            public static readonly string ID_PROPERTY_PATH = string.Format(Entity<Guid>.DEFAULT_PROPERTY_PATH_TEMPLATE, DemoValue.Metadata.NAME, DemoValue.Metadata.ID_PROPERTY);

            /// <summary>
            /// The Code property marker.
            /// </summary>
            public static readonly string CODE_PROPERTY = "Code";

            /// <summary>
            /// The Code property path.
            /// </summary>
            public static readonly string CODE_PROPERTY_PATH = string.Format(Entity<Guid>.DEFAULT_PROPERTY_PATH_TEMPLATE, DemoValue.Metadata.NAME, DemoValue.Metadata.CODE_PROPERTY);

            /// <summary>
            /// The Description property marker.
            /// </summary>
            public static readonly string DESCRIPTION_PROPERTY = "Description";

            /// <summary>
            /// The Description property path.
            /// </summary>
            public static readonly string DESCRIPTION_PROPERTY_PATH = string.Format(Entity<Guid>.DEFAULT_PROPERTY_PATH_TEMPLATE, DemoValue.Metadata.NAME, DemoValue.Metadata.DESCRIPTION_PROPERTY);

            /// <summary>
            /// The Value property marker.
            /// </summary>
            public static readonly string VALUE_PROPERTY = "Value";

            /// <summary>
            /// The Value property path.
            /// </summary>
            public static readonly string VALUE_PROPERTY_PATH = string.Format(Entity<Guid>.DEFAULT_PROPERTY_PATH_TEMPLATE, DemoValue.Metadata.NAME, DemoValue.Metadata.VALUE_PROPERTY);

            /// <summary>
            /// The CreationTimestamp property marker.
            /// </summary>
            public static readonly string CREATIONTIMESTAMP_PROPERTY = "CreationTimestamp";

            /// <summary>
            /// The CreationTimestamp property path.
            /// </summary>
            public static readonly string CREATIONTIMESTAMP_PROPERTY_PATH = string.Format(Entity<Guid>.DEFAULT_PROPERTY_PATH_TEMPLATE, DemoValue.Metadata.NAME, DemoValue.Metadata.CREATIONTIMESTAMP_PROPERTY);

            /// <summary>
            /// The ValidFrom property marker.
            /// </summary>
            public static readonly string VALIDFROM_PROPERTY = "ValidFrom";

            /// <summary>
            /// The ValidFrom property path.
            /// </summary>
            public static readonly string VALIDFROM_PROPERTY_PATH = string.Format(Entity<Guid>.DEFAULT_PROPERTY_PATH_TEMPLATE, DemoValue.Metadata.NAME, DemoValue.Metadata.VALIDFROM_PROPERTY);

            /// <summary>
            /// The ValidUntil property marker.
            /// </summary>
            public static readonly string VALIDUNTIL_PROPERTY = "ValidUntil";

            /// <summary>
            /// The ValidUntil property path.
            /// </summary>
            public static readonly string VALIDUNTIL_PROPERTY_PATH = string.Format(Entity<Guid>.DEFAULT_PROPERTY_PATH_TEMPLATE, DemoValue.Metadata.NAME, DemoValue.Metadata.VALIDUNTIL_PROPERTY);

        }

        #endregion
    }

}
