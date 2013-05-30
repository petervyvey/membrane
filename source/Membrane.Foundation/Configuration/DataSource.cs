
using System.Configuration;

namespace Membrane.Foundation.Configuration
{
	/// <summary>
	/// The datasource configuration element.
	/// </summary>
    public sealed class DataSource : ConfigurationElement
	{
		#region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
		public DataSource()
		{
			base.Properties.Add(new ConfigurationProperty(DataSource.METADATA_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.IsRequired));
			base.Properties.Add(new ConfigurationProperty(DataSource.PROVIDER_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.IsRequired));
			base.Properties.Add(new ConfigurationProperty(DataSource.CONNECTION_STRING_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.IsRequired));
		}

		#endregion

		#region - Constants & static fields -

		/// <summary>
		/// The name of the METADATA configuration attribute.
		/// </summary>
		private const string METADATA_CONFIGURATION_PROPERTY = "metadata";

		/// <summary>
		/// The name of the PROVIDER configuration attribute.
		/// </summary>
		private const string PROVIDER_CONFIGURATION_PROPERTY = "provider";

        /// <summary>
        /// The name of the DIALECT configuration attribute.
        /// </summary>
        private const string DIALECT_CONFIGURATION_PROPERTY = "dialect";

		/// <summary>
		/// The name of the CONNECTIONSTRING configuration attribute.
		/// </summary>
		private const string CONNECTION_STRING_CONFIGURATION_PROPERTY = "connectionString";

		#endregion

		/// <summary>
		/// The metadata information.
		/// </summary>
		[ConfigurationProperty(DataSource.METADATA_CONFIGURATION_PROPERTY)]
		public string Metadata
		{
			get { return (string)base[DataSource.METADATA_CONFIGURATION_PROPERTY]; }
			set { base[DataSource.METADATA_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The provider information.
		/// </summary>
		[ConfigurationProperty(DataSource.PROVIDER_CONFIGURATION_PROPERTY)]
		public string Provider
		{
			get { return (string)base[DataSource.PROVIDER_CONFIGURATION_PROPERTY]; }
			set { base[DataSource.PROVIDER_CONFIGURATION_PROPERTY] = value; }
		}

        /// <summary>
        /// The dialect information.
        /// </summary>
        [ConfigurationProperty(DataSource.DIALECT_CONFIGURATION_PROPERTY)]
        public string Dialect
        {
            get { return (string)base[DataSource.DIALECT_CONFIGURATION_PROPERTY]; }
            set { base[DataSource.DIALECT_CONFIGURATION_PROPERTY] = value; }
        }

		/// <summary>
		/// The connection string.
		/// </summary>
		[ConfigurationProperty(DataSource.CONNECTION_STRING_CONFIGURATION_PROPERTY)]
		public string ConnectionString
		{
			get { return (string)base[DataSource.CONNECTION_STRING_CONFIGURATION_PROPERTY]; }
			set { base[DataSource.CONNECTION_STRING_CONFIGURATION_PROPERTY] = value; }
		}
	}
}
