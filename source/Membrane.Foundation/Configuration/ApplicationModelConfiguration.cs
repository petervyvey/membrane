
using System.Configuration;
using System.Web.Configuration;
using Membrane.Foundation.ExceptionManagement;

namespace Membrane.Foundation.Configuration
{
    /// <summary>
	/// Represents the workspace configuration.
	/// </summary>
	public static class ApplicationModelConfiguration
	{
		#region - Constants & static fields -

		/// <summary>
		/// Key of the APPLICATION MODEL node in the conffiguration file.
		/// </summary>
		private const string APPLICATION_MODEL = "applicationModel";

		/// <summary>
		/// Template to format the configuration filename.
		/// </summary>
		private const string CONFIGURATION_FILE_TEMPLATE = "{0}.config";

		/// <summary>
		/// The configuration file name in a web environment.
		/// </summary>
		private const string WEB_CONFIGURATION_FILE_NAME = "~/web.config";

		#endregion

		#region - Static fields -

		/// <summary>
		/// Backingstore field for the ConfigurationManager property.
		/// </summary>
		private static System.Configuration.Configuration configurationManager = null;

		/// <summary>
		/// Backingstore field for the ConfigurationFileMap property.
		/// </summary>
		private static ExeConfigurationFileMap configurationFileMap;

		/// <summary>
		/// Backingstore field for the Model property.
		/// </summary>
		private static ConfigurationModel model = null;

		#endregion

		/// <summary>
		/// Reference to the workspace model configuration. 
		/// </summary>
        public static System.Configuration.Configuration ConfigurationManager
        {
            get
            {
                try
                {
					configurationManager = WebConfigurationManager.OpenWebConfiguration(ApplicationModelConfiguration.WEB_CONFIGURATION_FILE_NAME);
                }
                catch
                {
                    try
                    {
                        configurationManager = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(ApplicationModelConfiguration.ConfigurationFileMap, ConfigurationUserLevel.None);
                    }
                    catch
                    {
						throw new ApplicationModelConfigurationException((long)ExceptionNumber.MissingConfigurationFile);
                    }
                }

                return configurationManager;
            }
        }

		/// <summary>
		/// Reference to the configuration file map.
		/// </summary>
		public static ExeConfigurationFileMap ConfigurationFileMap
		{
			get
			{
				if (configurationFileMap == null)
				{
					configurationFileMap = new ExeConfigurationFileMap();
					configurationFileMap.ExeConfigFilename = string.Format(ApplicationModelConfiguration.CONFIGURATION_FILE_TEMPLATE, System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
				}

				return configurationFileMap;
			}
		}

		/// <summary>
		/// The configuration model.
		/// </summary>
		public static ConfigurationModel Configuration
		{
			get 
			{
				try
				{
					if (ApplicationModelConfiguration.model == null)
					{
						ApplicationModelConfiguration.model = (ConfigurationModel)ConfigurationManager.GetSection(ApplicationModelConfiguration.APPLICATION_MODEL);
					}
				}
				catch { /* IGNORE */ }

				return ApplicationModelConfiguration.model;
			}
			set { ApplicationModelConfiguration.model = value; }
		}
	}
}