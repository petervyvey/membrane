
using System.Configuration;

namespace Membrane.Foundation.Configuration
{
	/// <summary>
	/// The preloader configuration section.
	/// </summary>
    public sealed class Preloader : ConfigurationElement
	{
		#region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Preloader()
		{
			base.Properties.Add(new ConfigurationProperty(Preloader.TYPE_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.IsRequired));
			base.Properties.Add(new ConfigurationProperty(Preloader.METHOD_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.IsRequired));
		}

		#endregion

		#region - Constants & static fields -

		/// <summary>
		/// The name of the TYPE configuration property.
		/// </summary>
		private const string TYPE_CONFIGURATION_PROPERTY = "type";

		/// <summary>
		/// The name of the METHOD configuration property.
		/// </summary>
		private const string METHOD_CONFIGURATION_PROPERTY = "method";

		#endregion

		/// <summary>
		/// The type of preloader.
		/// </summary>
        [ConfigurationProperty(Preloader.TYPE_CONFIGURATION_PROPERTY)]
		public string Type
		{
            get { return (string)base[Preloader.TYPE_CONFIGURATION_PROPERTY]; }
            set { base[Preloader.TYPE_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The method to execute.
		/// </summary>
		[ConfigurationProperty(Preloader.METHOD_CONFIGURATION_PROPERTY)]
		public string Method
		{
			get { return (string)base[Preloader.METHOD_CONFIGURATION_PROPERTY]; }
			set { base[Preloader.METHOD_CONFIGURATION_PROPERTY] = value; }
        }
	}
}
