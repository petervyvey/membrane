
using System.Configuration;

namespace Membrane.Foundation.Configuration
{
    /// <summary>
	/// The key/value configuration element.
	/// </summary>
    public sealed class KeyValue 
        : ConfigurationElement
	{
		#region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
        public KeyValue()
		{
            base.Properties.Add(new ConfigurationProperty(KeyValue.KEY_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.IsRequired));
            base.Properties.Add(new ConfigurationProperty(KeyValue.VALUE_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.IsRequired));
		}

		#endregion

		#region - Constants & static fields -

		/// <summary>
		/// The name of the TYPE configuration property.
		/// </summary>
		private const string KEY_CONFIGURATION_PROPERTY = "key";

		/// <summary>
		/// The name of the METHOD configuration property.
		/// </summary>
		private const string VALUE_CONFIGURATION_PROPERTY = "value";

		#endregion

		/// <summary>
		/// The type of preloader.
		/// </summary>
        [ConfigurationProperty(KeyValue.KEY_CONFIGURATION_PROPERTY)]
		public string Key
		{
            get { return (string)base[KeyValue.KEY_CONFIGURATION_PROPERTY]; }
            set { base[KeyValue.KEY_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The method to execute.
		/// </summary>
        [ConfigurationProperty(KeyValue.VALUE_CONFIGURATION_PROPERTY)]
		public string Value
		{
            get { return (string)base[KeyValue.VALUE_CONFIGURATION_PROPERTY]; }
            set { base[KeyValue.VALUE_CONFIGURATION_PROPERTY] = value; }
        }
	}
}
