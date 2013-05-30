
using System;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Membrane.Foundation.Configuration;
using NHibernate.Cfg;

namespace Membrane.Domain.Store
{
    /// <summary>
    /// Implementation of <see cref="NHibernateEntityContext"/> with Fluent NHibernate.
    /// </summary>
    public sealed class FluentNHibernateEntityContext
        : NHibernateEntityContext
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FluentNHibernateEntityContext()
            : base() { }

        #endregion

        #region - Destructor -

        /// <summary>
        /// Destructor.
        /// </summary>
        ~FluentNHibernateEntityContext()
        {
            this.Dispose(false);
        }

        #endregion

        #region - Constants & static fields -

        /// <summary>
        /// The ENABLED ENVIRONMENT marker.
        /// </summary>
        private const string ENABLED_ENVIRONMENT_MARKER = "ENABLED_ENVIRONMENT";

        /// <summary>
        /// The MAPPING ASSEMBLY identifier.
        /// </summary>
        private const string MAPPING_ASSEMBLY_IDENTIFIER = "MappingAssembly";

        #endregion

        #region - Private & protected methods -

        /// <summary>
        /// The session factory.
        /// </summary>
        /// <returns>A session factory.</returns>
        protected override ISessionFactory CreateSessionFactory()
        {
            ISessionFactory factory;
            if (!NHibernateEntityContext.sessionFactories.TryGetValue(ENABLED_ENVIRONMENT_MARKER, out factory))
            {
                IPersistenceConfigurer persistence = null;

                switch (ApplicationModelConfiguration.Configuration.Server.Environments.GetEnabledEnvironment().DataSource.Dialect)
                {
                    case "MsSql2005":
                        {
                            string connectionString = ApplicationModelConfiguration.Configuration.Server.Environments.GetEnabledEnvironment().DataSource.ConnectionString;
                            persistence = MsSqlConfiguration.MsSql2005.ConnectionString(connectionString);
                        }
                        break;

                    case "MsSql2008":
                        {
                            string connectionString = ApplicationModelConfiguration.Configuration.Server.Environments.GetEnabledEnvironment().DataSource.ConnectionString;
                            persistence = MsSqlConfiguration.MsSql2008.ConnectionString(connectionString);
                        }
                        break;

                    case "SQLite":
                        {
                            string connectionString = ApplicationModelConfiguration.Configuration.Server.Environments.GetEnabledEnvironment().DataSource.ConnectionString;
                            persistence = SQLiteConfiguration.Standard.ConnectionString(connectionString);
                        }
                        break;

                    default:
                        throw new NotImplementedException();
                }

                string mappingAssembly = ApplicationModelConfiguration.Configuration.Server.Environments.GetEnabledEnvironment().Settings[MAPPING_ASSEMBLY_IDENTIFIER].Value;
				
				Configuration configuration = new Configuration();
				configuration.SetInterceptor(new NhibernateSqlInterceptor());

                FluentConfiguration fluentConfiguration =
                    Fluently.Configure(configuration)
                        .Database(persistence)
                        .Mappings(mappings => mappings.FluentMappings.AddFromAssembly(Assembly.Load(mappingAssembly)));

                NHibernateEntityContext.sessionFactories[ENABLED_ENVIRONMENT_MARKER] = factory = fluentConfiguration.BuildSessionFactory();
            }

            return factory;
        }

        #endregion
    }
}
