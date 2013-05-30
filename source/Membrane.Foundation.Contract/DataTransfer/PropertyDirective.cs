
namespace Membrane.Foundation.DataTransfer
{
    /// <summary>
    /// A projection directive for entity property projection.
    /// </summary>
    public class PropertyDirective
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        private PropertyDirective()
            : base() { }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <param name="directive">The <see cref="InjectionDirective"/>.</param>
        private PropertyDirective(string propertyName, InjectionDirective injectionDirective)
            : this()
        {
            this.Property = propertyName;
            this.InjectionDirective = injectionDirective;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// The property name.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// The <see cref="InjectionDirective"/>.
        /// </summary>
        public InjectionDirective InjectionDirective { get; set; }

        #endregion

        /// <summary>
        /// The directive factory.
        /// </summary>
        /// <param name="property">The property name.</param>
        /// <param name="directive">The <see cref="InjectionDirective"/>.</param>
        /// <returns>An instance of <see cref="PropertyDirective"/>.</returns>
        public static PropertyDirective Create(string property, InjectionDirective directive)
        {
            return new PropertyDirective(property, directive);
        }
    }
}
