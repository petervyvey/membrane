
using System.Configuration;

namespace Membrane.Foundation.Configuration
{
	/// <summary>
	/// The environment configuration section.
	/// </summary>
	public sealed class Environment : ConfigurationElement
	{
		#region - Constructors -

		/// <summary>
		/// Default  constructor.
		/// </summary>
		public Environment()
		{
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.NAME_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.IsRequired));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.ENABLED_CONFIGURATION_PROPERTY, typeof(bool), null, ConfigurationPropertyOptions.None));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.DESCRIPTION_CONFIGURATION_PROPERTY, typeof(string), null, ConfigurationPropertyOptions.None));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.DATASOURCE_CONFIGURATION_PROPERTY, typeof(DataSource), null, ConfigurationPropertyOptions.None));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.AUTHENTICATION_SERVICE_CONFIGURATION_PROPERTY, typeof(TypeElement), null, ConfigurationPropertyOptions.None));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.AUTHORIZATION_SERVICE_CONFIGURATION_PROPERTY, typeof(TypeElement), null, ConfigurationPropertyOptions.None));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.AUDIT_SERVICE_CONFIGURATION_PROPERTY, typeof(TypeElement), null, ConfigurationPropertyOptions.None));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.USER_PROFILE_SERVICE_CONFIGURATION_PROPERTY, typeof(TypeElement), null, ConfigurationPropertyOptions.None));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.SESSION_REGISTRY_SERVICE_CONFIGURATION_PROPERTY, typeof(TypeElement), null, ConfigurationPropertyOptions.None));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.SEQUENCER_SERVICE_CONFIGURATION_PROPERTY, typeof(TypeElement), null, ConfigurationPropertyOptions.None));
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environment.PRELOADERS_CONFIGURATION_PROPERTY, typeof(Preloaders), null, ConfigurationPropertyOptions.None));
		}

		#endregion

		#region - Constants & static fields -

		/// <summary>
		/// The NAME configuration property.
		/// </summary>
		private const string NAME_CONFIGURATION_PROPERTY = "name";

		/// <summary>
		/// The ENABLED configuration property.
		/// </summary>
		private const string ENABLED_CONFIGURATION_PROPERTY = "enabled";

		/// <summary>
		/// The DESCRIPTION configuration property.
		/// </summary>
		private const string DESCRIPTION_CONFIGURATION_PROPERTY = "description";

		/// <summary>
		/// The AUTHENTICATIONSERVICE configuration property.
		/// </summary>
		private const string DATASOURCE_CONFIGURATION_PROPERTY = "dataSource";

		/// <summary>
		/// The AUTHENTICATIONSERVICE configuration property.
		/// </summary>
		private const string AUTHENTICATION_SERVICE_CONFIGURATION_PROPERTY = "authenticationService";

		/// <summary>
		/// The AUTHORIZATIONSERVICE configuration property.
		/// </summary>
		private const string AUTHORIZATION_SERVICE_CONFIGURATION_PROPERTY = "authorizationService";

		/// <summary>
		/// The AUDITSERVICE configuration property.
		/// </summary>
		private const string AUDIT_SERVICE_CONFIGURATION_PROPERTY = "auditService";

		/// <summary>
		/// The USERPROFILESERVICE configuration property.
		/// </summary>
		private const string USER_PROFILE_SERVICE_CONFIGURATION_PROPERTY = "userProfileService";

		/// <summary>
		/// The SESSIONREGISTRYSERVICE configuration property.
		/// </summary>
		private const string SESSION_REGISTRY_SERVICE_CONFIGURATION_PROPERTY = "sessionRegistryService";

		/// <summary>
		/// The SEQUENCERSERVICE configuration property.
		/// </summary>
		private const string SEQUENCER_SERVICE_CONFIGURATION_PROPERTY = "sequencerService";

		/// <summary>
		/// The PRELOADERS configration.
		/// </summary>
		private const string PRELOADERS_CONFIGURATION_PROPERTY = "preloaders";

        /// <summary>
        /// The SETTINGS configration.
        /// </summary>
        private const string SETTINGS_CONFIGURATION_PROPERTY = "settings";

		#endregion

		/// <summary>
		/// The environment name.
		/// </summary>
		[ConfigurationProperty(Environment.NAME_CONFIGURATION_PROPERTY)]
		public string Name
		{
			get { return (string)base[Environment.NAME_CONFIGURATION_PROPERTY]; }
			set { base[Environment.NAME_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The environment name.
		/// </summary>
		[ConfigurationProperty(Environment.ENABLED_CONFIGURATION_PROPERTY)]
		public bool Enabled
		{
			get { return (bool)base[Environment.ENABLED_CONFIGURATION_PROPERTY]; }
			set { base[Environment.ENABLED_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The environment description.
		/// </summary>
		[ConfigurationProperty(Environment.DESCRIPTION_CONFIGURATION_PROPERTY)]
		public string Description
		{
			get { return (string)base[Environment.DESCRIPTION_CONFIGURATION_PROPERTY]; }
			set { base[Environment.DESCRIPTION_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The datasource property.
		/// </summary>
		[ConfigurationProperty(Environment.DATASOURCE_CONFIGURATION_PROPERTY)]
		public DataSource DataSource
		{
			get { return (DataSource)base[Environment.DATASOURCE_CONFIGURATION_PROPERTY]; }
			set { base[Environment.DATASOURCE_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The authentication service.
		/// </summary>
		[ConfigurationProperty(Environment.AUTHENTICATION_SERVICE_CONFIGURATION_PROPERTY)]
		public TypeElement AuthenticationService
		{
			get { return (TypeElement)base[Environment.AUTHENTICATION_SERVICE_CONFIGURATION_PROPERTY]; }
			set { base[Environment.AUTHENTICATION_SERVICE_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The authorization service.
		/// </summary>
		[ConfigurationProperty(Environment.AUTHORIZATION_SERVICE_CONFIGURATION_PROPERTY)]
		public TypeElement AuthorizationService
		{
			get { return (TypeElement)base[Environment.AUTHORIZATION_SERVICE_CONFIGURATION_PROPERTY]; }
			set { base[Environment.AUTHORIZATION_SERVICE_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The audit service.
		/// </summary>
		[ConfigurationProperty(Environment.AUDIT_SERVICE_CONFIGURATION_PROPERTY)]
		public TypeElement AuditService
		{
			get { return (TypeElement)base[Environment.AUDIT_SERVICE_CONFIGURATION_PROPERTY]; }
			set { base[Environment.AUDIT_SERVICE_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The user profile service.
		/// </summary>
		[ConfigurationProperty(Environment.USER_PROFILE_SERVICE_CONFIGURATION_PROPERTY)]
		public TypeElement UserProfileService
		{
			get { return (TypeElement)base[Environment.USER_PROFILE_SERVICE_CONFIGURATION_PROPERTY]; }
			set { base[Environment.USER_PROFILE_SERVICE_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The session resgitry service.
		/// </summary>
		[ConfigurationProperty(Environment.SESSION_REGISTRY_SERVICE_CONFIGURATION_PROPERTY)]
		public TypeElement SessionRegistryService
		{
			get { return (TypeElement)base[Environment.SESSION_REGISTRY_SERVICE_CONFIGURATION_PROPERTY]; }
			set { base[Environment.SESSION_REGISTRY_SERVICE_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The sequencer service.
		/// </summary>
		[ConfigurationProperty(Environment.SEQUENCER_SERVICE_CONFIGURATION_PROPERTY)]
		public TypeElement SequencerService
		{
			get { return (TypeElement)base[Environment.SEQUENCER_SERVICE_CONFIGURATION_PROPERTY]; }
			set { base[Environment.SEQUENCER_SERVICE_CONFIGURATION_PROPERTY] = value; }
		}

		/// <summary>
		/// The collection of environment preloaders.
		/// </summary>
		[ConfigurationProperty(Environment.PRELOADERS_CONFIGURATION_PROPERTY)]
		public Preloaders Preloaders
		{
			get { return (Preloaders)base[Environment.PRELOADERS_CONFIGURATION_PROPERTY]; }
			set { base[Environment.PRELOADERS_CONFIGURATION_PROPERTY] = value; }
        }

        /// <summary>
        /// The collection of environment settings.
        /// </summary>
        [ConfigurationProperty(Environment.SETTINGS_CONFIGURATION_PROPERTY)]
        public Settings Settings
        {
            get { return (Settings)base[Environment.SETTINGS_CONFIGURATION_PROPERTY]; }
            set { base[Environment.SETTINGS_CONFIGURATION_PROPERTY] = value; }
        }
	}
}
