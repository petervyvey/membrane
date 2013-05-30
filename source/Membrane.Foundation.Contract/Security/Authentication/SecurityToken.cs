
using System;
using System.Runtime.Serialization;

namespace Membrane.Foundation.Security.Authentication
{
    /// <summary>
    /// Security token.
    /// </summary>
    public sealed class SecurityToken
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        private SecurityToken()
            : base() { }

        /// <summary>
        /// Constructor with paramaters.
        /// </summary>
        /// <param name="authority">The authority providing the security token.</param>
        /// <param name="identifier">The identifier.</param>
        public SecurityToken(string authority, string identifier)
            : this()
        {
            this.Authority = authority;
            this.Identifier = identifier;
        }

        #endregion

        /// <summary>
        /// The authority providing the identifier.
        /// </summary>
        public string Authority { get; private set; }

        /// <summary>
        /// The identifier.
        /// </summary>
        public string Identifier { get; private set; }

    }
}
