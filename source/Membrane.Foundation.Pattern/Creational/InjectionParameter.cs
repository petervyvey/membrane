
using System;

namespace Membrane.Foundation.Pattern.Creational
{
    /// <summary>
    /// Abstract container for dependency injection parameters.
    /// </summary>
    public class InjectionParameter
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        private InjectionParameter()
            : base() { }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        public InjectionParameter(string name, object value)
            : this()
        {
            this.Name = name;
            this.Value = value;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// The parameter name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The parameter value.
        /// </summary>
        public object Value { get; private set; }

        #endregion

        /// <summary>
        /// Creates a new instance of a <see cref="InjectionParameter"/> with given name and value.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns></returns>
        public static InjectionParameter Create(string name, object value)
        {
            return new InjectionParameter(name, value);
        }
    }
}
