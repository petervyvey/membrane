
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Membrane.Foundation.Entity
{
    /// <summary>
	/// Container holding pointers to related members.
	/// </summary>
    public sealed class References
        : IReferenceContainerOperation
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        public References()
            : base() { }

        #endregion

        #region - Constants & static fields -

        /// <summary>
        /// The LOCK object.
        /// </summary>
        private readonly object LOCK = new object();

        #endregion

        #region - Properties -

        /// <summary>
        /// Reference identifiers to the child entities.
        /// </summary>
        public IDictionary<string, string> Members { get; set; }

        /// <summary>
        /// Set of reference identifier to child collections.
        /// </summary>
        public IDictionary<string, IList<string>> Collections { get; set; }

        #endregion

        /// <summary>
        /// Adds a reference to the Members collection.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="reference">The reference value of the </param>
        public void AddMember(string name, string reference)
        {
            this.SafeCreateMembersContainer();
            this.Members[name] = reference;
        }

        /// <summary>
        /// Adds a reference to the Collections collection.
        /// </summary>
        /// <param name="name">The name of the collection.</param>
        /// <param name="collection">The collection of reference values.</param>
        public void AddCollection(string name, IList<string> collection)
        {
            this.SafeCreateCollectionsContainer();
            this.Collections[name] = collection;
        }

        #region - Private & protected methods -

        /// <summary>
        /// Checks if the Members property is initialized, if not creates it.
        /// </summary>
        private void SafeCreateMembersContainer()
        {
            if (default(IDictionary<string, string>) == this.Members)
            {
                lock (this.LOCK)
                {
                    if (default(IDictionary<string, string>) == this.Members)
                    {
                        this.Members = new ConcurrentDictionary<string, string>();
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the Collections property is initialized, if not creates it.
        /// </summary>
        private void SafeCreateCollectionsContainer()
        {
            if (default(IDictionary<string, IList<string>>) == this.Collections)
            {
                lock (this.LOCK)
                {
                    if (default(IDictionary<string, IList<string>>) == this.Collections)
                    {
                        this.Collections = new ConcurrentDictionary<string, IList<string>>();
                    }
                }
            }
        }

        #endregion
    }
}
