
using System.Configuration;

namespace Membrane.Foundation.Configuration
{
	/// <summary>
    /// The SERVER configuration section.
	/// </summary>
	public sealed class Server : ConfigurationElement
	{
		#region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
        public Server()
        {
			base.Properties.Add(new ConfigurationProperty(Server.ENVIRONMENTS_CONFIGURATION_PROPERTY, typeof(Environments), null, ConfigurationPropertyOptions.IsRequired));
        }

        #endregion

		#region - Constants & static fields -

		/// <summary>
		/// Node name of the ENVIRONMENTS configuration property.
		/// </summary>
		private const string ENVIRONMENTS_CONFIGURATION_PROPERTY = "environments";

		#endregion

		/// <summary>
		/// Collection of environments.
		/// </summary>
		[ConfigurationProperty(Server.ENVIRONMENTS_CONFIGURATION_PROPERTY)]
        public Environments Environments
        {
			get { return (Environments)base[Server.ENVIRONMENTS_CONFIGURATION_PROPERTY]; }
        }
	}
}
