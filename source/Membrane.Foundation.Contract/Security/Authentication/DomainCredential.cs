
using System.Runtime.Serialization;
using Membrane.Foundation.Model;

namespace Membrane.Foundation.Security.Authentication
{
    /// <summary>
    /// User credential to log onto an <see cref="IApplicationModel"/> implementation.
    /// </summary>
    public sealed class DomainCredential
        : Credential
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        private DomainCredential()
            : base() { }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="domainName">The domain name.</param>
        /// <param name="login">The user login.</param>
        /// <param name="password">The user password.</param>
        public DomainCredential(string domainName, string login, string password)
            : this()
        {
            this.Login = login;
            this.Password = password;
            this.DomainName = domainName;
        }

        #endregion

        /// <summary>
        /// The domain name.
        /// </summary>
        public string DomainName { get; private set; }

    }
}
