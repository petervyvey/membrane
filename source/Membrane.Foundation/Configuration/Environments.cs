
using System.Configuration;
using System.Linq;

namespace Membrane.Foundation.Configuration
{
	/// <summary>
	/// The collection of environment configuration section.
	/// </summary>
    public sealed class Environments : ConfigurationElementCollection
    {
        #region - Constructors -

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Environments()
		{
			base.Properties.Add(new System.Configuration.ConfigurationProperty(Environments.ENABLED_CONFIGURATION_PROPERTY, typeof(string)));
		}

		#endregion

		#region - Constants & static fields -

		/// <summary>
		/// Element name of ENVIRONMENT.
		/// </summary>
		private const string ENVIRONMENT_ELEMENTNAME = "environment";

		/// <summary>
		/// Node name of DEFAULT configuration property.
		/// </summary>
		private const string ENABLED_CONFIGURATION_PROPERTY = "enabled";

		#endregion

		#region - Properties -

		/// <summary>
		/// The name of the default environment.
		/// </summary>
		[ConfigurationProperty(Environments.ENABLED_CONFIGURATION_PROPERTY)]
		public string Enabled
		{
			get { return (string)base[Environments.ENABLED_CONFIGURATION_PROPERTY]; }
			set { base[Environments.ENABLED_CONFIGURATION_PROPERTY] = value; }
        }

		/// <summary>
		/// Indexer property returning the environment at the index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The environment.</returns>
        public Environment this[int index]
        {
            get { return (Environment)base.BaseGet(index); }
        }

		/// <summary>
		/// Indexer property returning the environment identified by the index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The environment.</returns>
		public new Environment this[string index]
		{
			get { return (Environment)base.BaseGet(index); }
		}

		/// <summary>
		/// Returns the configuration collection type.
		/// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

		/// <summary>
		/// Identifier for the element configuration.
		/// </summary>
        protected override string ElementName
        {
			get { return Environments.ENVIRONMENT_ELEMENTNAME; }
        }

        #endregion

        #region - Private & protected methods -

		/// <summary>
		/// Creates a new <see cref="Environment"/> element.
		/// </summary>
		/// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new Environment();
        }

		/// <summary>
		/// Gets the element key.
		/// </summary>
		/// <param name="element">The configuration element</param>
		/// <returns>The key.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Environment)element).Name;
        }

        #endregion

		/// <summary>
		/// Clears the collection of environments.
		/// </summary>
        public void Clear()
        {
            this.BaseClear();
        }

		/// <summary>
		/// Adds a environment to the configuration.
		/// </summary>
		/// <param name="element"></param>
        public void Add(Environment element)
        {
            if (element != null)
            {
                this.BaseAdd(element);
            }
        }

		/// <summary>
		/// Removes a environment from the collection.
		/// </summary>
		/// <param name="name"></param>
        public void Remove(string name)
        {
            this.BaseRemove(name);
        }

		/// <summary>
		/// Gets the enabled environment.
		/// </summary>
		/// <returns></returns>
		public Environment GetEnabledEnvironment()
		{
			// Try to find the enabled ENVIRONMENT, this will return no results in case of a WEBCONFIG.
			Environment environment = this.Cast<Environment>().Where(e => e.Name == this.Enabled).SingleOrDefault();

			// If none found, select the ENVIRONMENT with the enabled flag set.
			if (null == environment)
			{
				environment = this.Cast<Environment>().Where(e => e.Enabled).Single();
			}

			return environment;
		}
    }
}
