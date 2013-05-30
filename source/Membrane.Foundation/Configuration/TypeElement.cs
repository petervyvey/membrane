
using System.Configuration;

namespace Membrane.Foundation.Configuration
{
	/// <summary>
	/// The type element configuration section.
	/// </summary>
    public sealed class TypeElement : ConfigurationElement
	{
		#region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TypeElement()
		{
			base.Properties.Add(new ConfigurationProperty(TypeElement.TYPE_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.IsRequired));
		}

		#endregion

		#region - Constants & static fields -

		/// <summary>
		/// The name of the TYPE configuration property.
		/// </summary>
		private const string TYPE_CONFIGURATION_PROPERTY = "type";

		#endregion

		/// <summary>
		/// The type of information.
		/// </summary>
		[ConfigurationProperty(TypeElement.TYPE_CONFIGURATION_PROPERTY)]
		public string Type
		{
			get { return (string)base[TypeElement.TYPE_CONFIGURATION_PROPERTY]; }
			set { base[TypeElement.TYPE_CONFIGURATION_PROPERTY] = value; }
		}
	}
}
