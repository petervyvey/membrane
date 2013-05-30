
using System.Configuration;

namespace Membrane.Foundation.Configuration
{
	/// <summary>
	/// The root configuration section.
	/// </summary>
	public sealed class ConfigurationModel : ConfigurationSection
	{		
		#region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
        public ConfigurationModel() 
			: base() 
        {
            base.Properties.Add(new ConfigurationProperty(ConfigurationModel.CLIENT_CONFIGURATION_PROPERTY, typeof(Client)));
            base.Properties.Add(new ConfigurationProperty(ConfigurationModel.SERVER_CONFIGURATION_PROPERTY, typeof(Server)));
		}

		#endregion

		#region - Constants & static fields -

		/// <summary>
		/// Name of the CLIENT configuration node.
		/// </summary>
		private const string CLIENT_CONFIGURATION_PROPERTY = "client";

		/// <summary>
		/// Name of the HOST configuration node.
		/// </summary>
		private const string SERVER_CONFIGURATION_PROPERTY = "server";

		#endregion

		/// <summary>
		/// The client configuration section.
		/// </summary>
		[ConfigurationProperty(ConfigurationModel.CLIENT_CONFIGURATION_PROPERTY)]
		public Client Client
		{
			get { return (Client)base[ConfigurationModel.CLIENT_CONFIGURATION_PROPERTY]; }
			set { base[ConfigurationModel.CLIENT_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The host configuration section.
		/// </summary>
        [ConfigurationProperty(ConfigurationModel.SERVER_CONFIGURATION_PROPERTY)]
        public Server Server
		{
            get { return (Server)base[ConfigurationModel.SERVER_CONFIGURATION_PROPERTY]; }
            set { base[ConfigurationModel.SERVER_CONFIGURATION_PROPERTY] = value; }
        }
	}
}
