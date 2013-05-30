
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Membrane.Foundation.Configuration;
using Membrane.Foundation.Pattern.Creational;
using Membrane.Foundation.Security.Authentication;
using Membrane.Foundation.Security.Authorization;

namespace Membrane.Foundation.Model
{
	/// <summary>
    /// Holds the references to the application's sessions, services and object repositories.
    /// </summary>
    /// <remarks>
    /// This APPLICATIONMODEL object is typicaly used as a singleton in the application.
    /// </remarks>
    public sealed partial class ApplicationModel 
		: IApplicationModel, IDisposable
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationModel() 
        {
            this.InitializeObject();
        }

        #endregion

        #region - Destructor -

        /// <summary>
        /// Destructor.
        /// </summary>
        ~ApplicationModel()
        {
            this.Dispose(false);
        }

        #endregion

		#region - Constants & static fields -

		/// <summary>
		/// The default options for parallel tasks.
		/// </summary>
		public static readonly ParallelOptions DEFAULT_PARALLEL_OPTIONS = new ParallelOptions { MaxDegreeOfParallelism = 2 };

		#endregion

		#region - Delegates -

		/// <summary>
        /// Delegate defining the signature for the method of the configured preloaders.
        /// </summary>
        private delegate void Preload();

        #endregion

        #region - Private fields -

        /// <summary>
        /// Container for the thread local SessionID.
        /// </summary>
        private ThreadLocal<Session> session = new ThreadLocal<Session>(() => default(Session));

		/// <summary>
		/// TRUE if the object is disposed, FALSE otherwise.
		/// </summary>
		private bool isDisposed = false;

        /// <summary>
        /// Token used to cancel the scheduled tasks.
        /// </summary>
        private CancellationTokenSource cancellationToken = new CancellationTokenSource();

        /// <summary>
        /// A authentication service.
        /// </summary>
		private IAuthenticationService authenticationService;

		/// <summary>
		/// The authorization service.
		/// </summary>
		private IAuthorizationService authorizationService;

		/// <summary>
		/// The audit service.
		/// </summary>
		private IAuditService auditService;

        #endregion

        #region - Properties -

		/// <summary>
		/// Returns the singleton instance of an <see cref="IApplicationModel"/> implementation.
		/// </summary>
        public static ApplicationModel Current { get { return DependencyInjection.Get<ApplicationModel>(); } }

        /// <summary>
        /// The current session identifier. Points to the thread specific container for the identifier.
        /// </summary>
        /// <remarks>Still we have to try to implement some thread-safety checking while accessing the SessionID.</remarks>
        public Session Session
        {
            get { return this.session.Value; }
        }

        /// <summary>
        /// Returns TRUE if a valid <see cref="SessionService.Session"/> is attached the current WORKSPACEHOST, FALSE otherwise.
        /// </summary>
        private bool HasValidSession
        {
            get { return this.Session != default(Session); }
        }

        #endregion

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
			if (!this.isDisposed)
			{
				this.Dispose(true);
			}

            GC.SuppressFinalize(this);
        }

        public Session CreateSession(SecurityToken securityIdentifier)
        {
            this.session.Value = new Session(securityIdentifier);
            this.session.Value.Closed += SessionClosed;

            return this.session.Value;
        }

        /// <summary>
        /// Handles the <see cref="Session"/>.Closed event.
        /// </summary>
        /// <param name="sender">The <see cref="Session"/>.</param>
        /// <param name="e">The <see cref="EventArgs"/>.</param>
        private void SessionClosed(object sender, EventArgs e)
        {
            // Set the SESSION value to NULL.
            Session session = this.session.Value;
            this.session.Value = default(Session);

            // Detach the event handler.
            session.Closed -= SessionClosed;
        }

		/// <summary>
		/// Authorizes the access to the resource for a user.
		/// </summary>
		/// <param name="resourcePath">The resource path.</param>
		/// <returns>The permission.</returns>
		public Permission GetPermission(string resourcePath)
		{
			Permission permission = Permission.NoAccess; // = this.authorizationService.GetPermission(this.Session.Token.UserToken, resourcePath);
			
			return permission;
		}

		/// <summary>
		/// Audits a message to the workspace backend store.
		/// </summary>
		/// <param name="entryType">The type of audit entry.</param>
		/// <param name="message">The audit message.</param>
		public void Audit(AuditEntryType entryType, string message)
		{
            //this.auditService.Audit(this.Session.SessionRegistryID, entryType, message);
		}

		/// <summary>
		/// Audits a message to the workspace back end store.
		/// </summary>
		/// <param name="entryType">The type of audit entry.</param>
		/// <param name="message">The audit message.</param>
		/// <param name="data">The additional audit object.</param>
		public void Audit<T>(AuditEntryType entryType, string message, T data)
		{
            //this.auditService.Audit<T>(this.Session.SessionRegistryID, entryType, message, data);
		}

        #region - Private & protected methods -

        /// <summary>
        /// Initializes the ApplicationService.
        /// </summary>
        private void InitializeObject()
        {
            this.StartScheduledTasks();
			this.ConfigureServices();
            this.ExecutePreloaders();
        }

		/// <summary>
		/// Configures the workspace services.
		/// </summary>
		private void ConfigureServices()
		{
			if (null != ApplicationModelConfiguration.Configuration)
			{
				Configuration.Environment environment = ApplicationModelConfiguration.Configuration.Server.Environments.GetEnabledEnvironment();

				// Configure the AUTHENTICATIONSERVICE.
                if (!string.IsNullOrEmpty(environment.AuthenticationService.Type))
                {
                    Type authenticationServiceType = Type.GetType(environment.AuthenticationService.Type);
                    this.authenticationService = Activator.CreateInstance(authenticationServiceType) as IAuthenticationService;
                }

				// Configure the AUTHORIZATIONSERVICE.
                if (!string.IsNullOrEmpty(environment.AuthorizationService.Type))
                {
                    Type authorizatinServiceType = Type.GetType(environment.AuthorizationService.Type);
                    this.authorizationService = Activator.CreateInstance(authorizatinServiceType) as IAuthorizationService;
                }

				// Configure the AUDITSERVICE.
                if (!string.IsNullOrEmpty(environment.AuditService.Type))
                {
                    Type auditServiceType = Type.GetType(environment.AuditService.Type);
                    this.auditService = Activator.CreateInstance(auditServiceType) as IAuditService;
                }
			}
		}

        /// <summary>
        /// Preload and initialize objects that take a longer time to load. This way we create a more responsive application.
        /// </summary>
        private void ExecutePreloaders()
        {
            Preload preload =
                delegate()
                {
					try
					{
						if (null != ApplicationModelConfiguration.Configuration)
						{
                            var preloaders = ApplicationModelConfiguration.Configuration.Server.Environments[0].Preloaders;

							foreach (Preloader preloader in preloaders)
							{
								try
								{
									// Grab the type that has the static method
									Type type = Type.GetType(preloader.Type);

									// Grab the specific static method
									MethodInfo methodInfo = type.GetMethod(preloader.Method, BindingFlags.Static | BindingFlags.Public);

									// Execute the method
									methodInfo.Invoke(null, null);
								}
								finally { /* IGNORE */}
							}
						}
					}
					finally { }
                };

            IAsyncResult result = preload.BeginInvoke(null, null);
        }

        /// <summary>
        /// Start the scheduled task on a seperate thread.
        /// </summary>
        private void StartScheduledTasks()
        {
            //DateTime nextInvokeTime = DateTime.Now.AddSeconds(30);

            //Task task = Task.Factory.StartNew(obj =>
            //{
            //    Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            //    while (!this.cancellationToken.IsCancellationRequested)
            //    {
            //        if (DateTime.Now > nextInvokeTime)
            //        {
            //            // This gets rid of the dead objects
            //            GC.Collect();

            //            // This waits for any finalizers to finish.
            //            GC.WaitForPendingFinalizers();

            //            // This releases the memory associated with the objects that were just finalized.
            //            GC.Collect();

            //            // Set next invoke time.
            //            nextInvokeTime = DateTime.Now.AddSeconds(30);
            //        }
            //        Thread.Sleep(100);
            //    }
            //}, this.cancellationToken);
        }

        /// <summary>
        /// Disposes managed and unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">Flag indicating how this protected method was called. 
        /// TRUE means via Dispose(), FALSE means via the destructor.
        /// Only in case of a call through the Dispose() method should managed resources be freed.</param>
        private void Dispose(bool isDisposing)
        {
			if (!this.isDisposed)
			{
				// Dispose the workspace services.
				if (null != this.authenticationService)
				{
					this.authenticationService.Dispose();
				}

				if (null != this.authorizationService)
				{
					this.authorizationService.Dispose();
				}
			}

			this.isDisposed = true;
        }

        /// <summary>
        /// Retrieves a service of the specified type.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <returns>The service instance.</returns>
        /// <remarks>An instance of a service can only be retrieved via <see cref="ApplicationModel.Portal"/>.</remarks>
        private T GetService<T>()
            where T : class, IApplicationService, new()
        {
            return this.Session.GetService<T>();
        }

        #endregion
    }
}
