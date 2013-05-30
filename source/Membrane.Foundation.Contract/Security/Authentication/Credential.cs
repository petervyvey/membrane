
using System.Runtime.Serialization;
using Membrane.Foundation.Model;

namespace Membrane.Foundation.Security.Authentication
{
    /// <summary>
    /// User credential to log onto an <see cref="IApplicationModel"/> implementation.
    /// </summary>
    public abstract class Credential
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected Credential()
            : base() { }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="login">The user login.</param>
        /// <param name="password">The user password.</param>
        public Credential(string login, string password) :
            base()
        {
            this.Login = login;
            this.Password = password;
        }

        #endregion

        /// <summary>
        /// The user login.
        /// </summary>
        public string Login { get; protected set; }

        /// <summary>
        /// The user password.
        /// </summary>
        public string Password { get; protected set; }

    }
}
