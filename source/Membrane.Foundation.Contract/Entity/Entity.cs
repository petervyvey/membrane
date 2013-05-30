
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Membrane.Foundation.Entity
{
	/// <summary>
	/// Abstract base class for entities.
	/// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public abstract class Entity<TIdentifier>
        : IEntity<TIdentifier>
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected Entity()
            : base() { }

        #endregion

        #region - Constants & static fields -

        /// <summary>
        /// The LOCK object.
        /// </summary>
        private readonly object LOCK = new object();

        /// <summary>
        /// The default property path template.
        /// </summary>
        public const string DEFAULT_PROPERTY_PATH_TEMPLATE = "{0}.{1}";

        #endregion

        #region - Properties -

        /// <summary>
        /// The entity identifier.
        /// </summary>
        public virtual TIdentifier ID { get; set; }

        /// <summary>
        /// Container with references.
        /// </summary>
        public virtual References __references { get; set; }

        /// <summary>
        /// Container for the additional attributes attached to the <see cref="Entity"/>.
        /// </summary>
        public virtual IDictionary<string, object> __extensions { get; set; }

        #endregion

        /// <summary>
        /// Add an extension.
        /// </summary>
        /// <param name="name">The extension's name.</param>
        /// <param name="value">The extension's value.</param>
        public virtual void __addExtension(string name, object value)
        {
            this.SafeCreateUitbreidingen();
            this.__extensions[name] = value;
        }

        /// <summary>
        /// Adds a reference to the Members collection.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="reference">The reference value of the </param>
        public virtual void __addMemberReference(string name, string reference)
        {
            this.SafeCreateReferences();
            this.__references.AddMember(name, reference);
        }

        /// <summary>
        /// Adds a reference to the Collections collection.
        /// </summary>
        /// <param name="name">The name of the collection.</param>
        /// <param name="collection">The collection of reference values.</param>
        public virtual void __addCollectionReference(string name, IList<string> collection)
        {
            this.SafeCreateReferences();
            this.__references.AddCollection(name, collection);
        }

        #region - Private & protected methods -

        /// <summary>
        /// Checks if the Extension property is initialized, if not creates it.
        /// </summary>
        private void SafeCreateUitbreidingen()
        {
            if (default(IDictionary<string, object>) == this.__extensions)
            {
                lock (this.LOCK)
                {
                    if (default(IDictionary<string, object>) == this.__extensions)
                    {
                        this.__extensions = new ConcurrentDictionary<string, object>();
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the References property is initialized, if not creates it.
        /// </summary>
        private void SafeCreateReferences()
        {
            if (default(References) == this.__references)
            {
                lock (this.LOCK)
                {
                    if (default(References) == this.__references)
                    {
                        this.__references = new References();
                    }
                }
            }
        }

        #endregion
    }
}
