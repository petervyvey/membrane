
using System;
using Membrane.Foundation.Extension;
using Membrane.Foundation.Pattern.Creational;
using Membrane.Foundation.Security.Authentication;

namespace Membrane.Foundation.Model
{
    /// <summary>
    /// A session containing pointers to <see cref="IApplicationService"/> instances.
    /// </summary>
    public sealed class Session 
        : IDisposable
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        private Session()
            : base()
        {
            this.InitializeObject();
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public Session(SecurityToken identity)
            : this()
        {
            this.Identity = identity;
        }

        #endregion

        #region - Destructor -

        /// <summary>
        /// Destructor.
        /// </summary>
        ~Session()
        {
            this.Dispose(false);
        }

        #endregion

        #region - Private fields -

        /// <summary>
        /// TRUE if the object is disposed, FALSE otherwise.
        /// </summary>
        private bool isDisposed = false;

        #endregion

        #region - Events -

        /// <summary>
        /// Raised when the <see cref="Session"/> is closing.
        /// </summary>
        public event EventHandler Closing;

        /// <summary>
        /// Raised when the <see cref="Session"/> is closed.
        /// </summary>
        public event EventHandler Closed;

        #endregion

        #region - Properties -

        /// <summary>
        /// The <see cref="Session"/> ID.
        /// </summary>
        public Guid ID { get; private set; }

        /// <summary>
        /// The <see cref="SecurityToken"/> associated with the <see cref="Session"/>.
        /// </summary>
        public SecurityToken Identity { get; private set; }

        /// <summary>
        /// TRUE if this <see cref="Session"/> is anonymous, FALSE otherwise.
        /// </summary>
        public bool IsAnonymous { get { return default(SecurityToken) != this.Identity; } }

        /// <summary>
        /// Timestamp of the last activity for the session.
        /// </summary>
        internal DateTime LastActivity { get; private set; }

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

        /// <summary>
        /// Retrieves a service of the specified interface type.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <returns>The service instance.</returns>
        public T GetService<T>()
            where T : IApplicationService
        {
            IApplicationService service = DependencyInjection.Get<T>();

            return (T)service;
        }

        /// <summary>
        /// Closes the <see cref="Session"/>.
        /// </summary>
        public void Close()
        {
            this.Dispose();
        }

        #region - Private, protected & internal methods -

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
                if (isDisposing)
                {
                    this.OnClosing();

                    // Dispose managed resources here.
                }
            }

            this.OnClosed();
            this.isDisposed = true;
        }

        /// <summary>
        /// Initializes the session.
        /// </summary>
        private void InitializeObject()
        {
            this.ID = Guid.NewGuid();
        }

        /// <summary>
        /// Raises the Closing event.
        /// </summary>
        private void OnClosing()
        {
            this.Closing.Raise(this);
        }

        /// <summary>
        /// Raises the Closed event.
        /// </summary>
        private void OnClosed()
        {
            this.Closed.Raise(this);
        }

        #endregion
    }

}
