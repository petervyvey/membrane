
using System;
using System.Runtime.Serialization;

namespace Membrane.Foundation.Security.Authentication
{
    /// <summary>
    /// Identity container.
    /// </summary>
    public sealed class Identity
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        private Identity()
            : base() { }

        /// <summary>
        /// Constructor with paramaters.
        /// </summary>
        /// <param name="securityIdentifier">The security identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="givenName">The given name.</param>
        /// <param name="title">The title.</param>
        public Identity(SecurityToken securityIdentifier, string name, string givenName, string title)
            : this()
        {
            this.SecurityIdentifier = securityIdentifier;
            this.Name = name;
            this.GivenName = givenName;
            this.Title = title;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// The security identifier.
        /// </summary>
        public SecurityToken SecurityIdentifier { get; private set; }

        /// <summary>
        /// The name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The given name.
        /// </summary>
        public string GivenName { get; private set; }

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; private set; }

        #endregion
    }
}
