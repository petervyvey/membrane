
using System.Configuration;

namespace Membrane.Foundation.Configuration
{
	/// <summary>
	/// The client configuration section.
	/// </summary>
	public sealed class Client : ConfigurationElement
	{
		#region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Client()
		{
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Client.NAME_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.IsRequired));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Client.DESCRIPTION_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.None));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Client.PRELOADERS_CONFIGURATION_PROPERTY, typeof(Preloaders), null, ConfigurationPropertyOptions.None));
		}

		#endregion

		#region - Constants & static fields -

		/// <summary>
		/// The NAME property identifier.
		/// </summary>
		private const string NAME_CONFIGURATION_PROPERTY = "name";

		/// <summary>
		/// The DESCRIPTION property identifier.
		/// </summary>
		private const string DESCRIPTION_CONFIGURATION_PROPERTY = "description";

        /// <summary>
        /// The PRELOADERS property identifier.
        /// </summary>
		private const string PRELOADERS_CONFIGURATION_PROPERTY = "preloaders";

        /// <summary>
        /// The SETTINGS property identifier.
        /// </summary>
        private const string SETTINGS_CONFIGURATION_PROPERTY = "settings";

		#endregion

		/// <summary>
		/// The client name.
		/// </summary>
		[ConfigurationProperty(Client.NAME_CONFIGURATION_PROPERTY)]
		public string Name
		{
			get { return (string)base[Client.NAME_CONFIGURATION_PROPERTY]; }
			set { base[Client.NAME_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The client description.
		/// </summary>
		[ConfigurationProperty(Client.DESCRIPTION_CONFIGURATION_PROPERTY)]
		public string Description
		{
			get { return (string)base[Client.DESCRIPTION_CONFIGURATION_PROPERTY]; }
			set { base[Client.DESCRIPTION_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The collection of client application startup preloaders.
		/// </summary>
		[ConfigurationProperty(Client.PRELOADERS_CONFIGURATION_PROPERTY)]
		public Preloaders Preloaders
		{
			get { return (Preloaders)base[Client.PRELOADERS_CONFIGURATION_PROPERTY]; }
			set { base[Client.PRELOADERS_CONFIGURATION_PROPERTY] = value; }
        }

        /// <summary>
        /// The collection of environment settings.
        /// </summary>
        [ConfigurationProperty(Client.SETTINGS_CONFIGURATION_PROPERTY)]
        public Settings Settings
        {
            get { return (Settings)base[Client.SETTINGS_CONFIGURATION_PROPERTY]; }
            set { base[Client.SETTINGS_CONFIGURATION_PROPERTY] = value; }
        }
	}
}
