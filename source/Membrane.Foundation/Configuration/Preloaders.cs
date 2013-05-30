
using System.Configuration;

namespace Membrane.Foundation.Configuration
{
	/// <summary>
	/// The collection of preloader configuration section.
	/// </summary>
    public sealed class Preloaders : ConfigurationElementCollection
    {
        #region - Properties -

		/// <summary>
		/// Indexer property returning the <see cref="Preloader"/> configuration at the given index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The <see cref="Preloader"/> configuration.</returns>
        public Preloader this[int index]
        {
            get { return (Preloader)base.BaseGet(index); }
        }

		/// <summary>
		/// Indexer property returning the <see cref="Preloader"/> configuration at the given index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The <see cref="Preloader"/> configuration.</returns>
		public new Preloader this[string index]
		{
			get { return (Preloader)base.BaseGet(index); }
		}

		/// <summary>
		/// Returns the configuration collection type.
		/// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

		/// <summary>
		/// Identifier for the element configuration.
		/// </summary>
        protected override string ElementName
        {
            get { return "preloader"; }
        }

        #endregion

		#region - Constants & static fields -



		#endregion

		#region - Private & protected methods -

		/// <summary>
		/// Creates a new element.
		/// </summary>
		/// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new Preloader();
        }

		/// <summary>
		/// Gets the element key.
		/// </summary>
		/// <param name="element">The configurationn elemen.</param>
		/// <returns>The key.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Preloader)element).Type;
        }

        #endregion

		/// <summary>
		/// Clears the collection of <see cref="Preloader"/>.
		/// </summary>
        public void Clear()
        {
            this.BaseClear();
        }

		/// <summary>
		/// Adds a <see cref="Preloader"/> the configuration.
		/// </summary>
		/// <param name="element">The preloader configuration.</param>
        public void Add(Preloader element)
        {
            if (element != null)
            {
                this.BaseAdd(element);
            }
        }

		/// <summary>
		/// Removes the <see cref="Preloader"/> with the given name from the collection.
		/// </summary>
		/// <param name="name"></param>
        public void Remove(string name)
        {
            this.BaseRemove(name);
        }
    }
}
