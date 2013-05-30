
using System;
using System.Collections.Concurrent;
using System.Linq;
using DotNetOpenAuth.Messaging.Bindings;
using DotNetOpenAuth.OAuth2;
using Membrane.DataTransfer;
using Membrane.Domain.Agent;
using Membrane.Domain.Persistence;
using Membrane.Domain.Store;
using Membrane.Foundation.DataTransfer;
using Membrane.Foundation.Entity;
using Membrane.Foundation.Model;
using Membrane.Foundation.Pattern.Enterprise;
using Membrane.Foundation.Runtime.Logging;
using Membrane.Service.ApplicationService;
using Membrane.Web.OAuth;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace Membrane.Infrastructure.Configuration
{
    /// <summary>
    /// Ninject configuration class.
    /// </summary>
    public sealed class ConfigurationModule 
        : NinjectModule
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConfigurationModule() :
            base() { }

        #endregion

        #region - Constants & static fields -

        /// <summary>
        /// The cahce for allready constructed IPORTAL types.
        /// </summary>
        private static readonly ConcurrentDictionary<string, Type> PORTAL_TYPES = new ConcurrentDictionary<string, Type>();

        #endregion

        /// <summary>
        /// Configures the dependency injection bindings.
        /// </summary>
        public override void Load()
        {
            // Application model.
            this.Bind<ApplicationModel>().To<ApplicationModel>().InSingletonScope();
            this.Bind<ILoggerStrategy>().To<NullLoggerStrategy>().InSingletonScope();
            this.Bind<IUnitOfWork>().To<NonTransactionalUnitOfWork>();

            // Domain.
            this.Bind<IEntityHelper>().To<NHibernateEntityHelper>().InSingletonScope();
            this.Bind<IEntityContext>().To<FluentNHibernateEntityContext>();
			this.Bind<IDemoApplicationAgent>().To<DemoApplicationAgent>();

            // Repositories.
            this.Bind<IDemoValueRepository>().To<DemoValueRepository>();
			this.Bind<IOAuth2CryptoKeyRepository>().To<OAuth2CryptoKeyRepository>();
			this.Bind<IOAuth2ClientApplicationRepository>().To<OAuth2ClientApplicationRepository>();
			this.Bind<IOAuth2ScopeRepository>().To<OAuth2ScopeRepository>();
			this.Bind<IOAuth2UserRepository>().To<OAuth2UserRepository>();
	        this.Bind<IOAuth2UserScopeRepository>().To<OAuth2UserScopeRepository>();
			this.Bind<IOAuth2UserIdentityRepository>().To<OAuth2UserIdentityRepository>();

			// Portal.
			this.Bind<IDataPortal>().To<DataPortal>();

            // Portal factory.
            this.Bind<IRestPortal<DataTransferObject>>().ToMethod(ConfigurationModule.PortalFactory);

            // Portal implementations.
            this.Bind<IRestPortal<DemoValue>>().To<DemoValuePortal>();

            // Portal data transfer object bindings.
            this.Bind<DataTransferObject>().To<DemoValue>().WithMetadata("resource", typeof(DemoValue).Name.ToLowerInvariant());

            // OAuth 
            this.Bind<IAuthorizationServerHost>().To<AuthorizationServerHost>();
			this.Bind<IOAuth2AuthorizationStoreAgent>().To<OAuth2AuthorizationStoreAgent>();
			this.Bind<ICryptoKeyStore>().To<AuthorizationStore>();
			this.Bind<INonceStore>().To<AuthorizationStore>();
        }

        #region - Private & protected methods -

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">The Ninject <see cref="IContext"/> instance.</param>
        /// <returns>The requested <see cref="IRestPortal{DataTransferValue}"/> implementation.</returns>
        private static IRestPortal<DataTransferObject> PortalFactory(IContext context)
        {
            if (context.Request.Parameters.Count == 0) new ArgumentException("Parameter is missing.");

            object parameter = (context.Request.Parameters.ToList()[0].GetValue(context, null));
            if (default(object) == parameter) throw new ArgumentException("The generic type parameter is missing.");

            string resource = parameter as string;
            if (default(string) == resource) throw new ArgumentException("The resource parameter is NULL.");

            Type constructedType;
            if (!ConfigurationModule.PORTAL_TYPES.TryGetValue(resource, out constructedType))
            {
                Type resourceType =
                    context
                        .Kernel.GetBindings(typeof(DataTransferObject))
                            .Where(b => b.Metadata.Get<string>("resource") == resource.ToLowerInvariant())
                            .Single()
                            .GetProvider(context)
                            .Type;

                Type portalType = typeof(IRestPortal<>);
                Type[] typeArgs = { resourceType };
                constructedType = ConfigurationModule.PORTAL_TYPES[resource] = portalType.MakeGenericType(typeArgs);
            }

            IRestPortal<DataTransferObject> portal = null;
            if (constructedType != null)
            {
                portal = (IRestPortal<DataTransferObject>)context.Kernel.Get(constructedType);
            }

            return portal;
        }

        #endregion
    }
}
