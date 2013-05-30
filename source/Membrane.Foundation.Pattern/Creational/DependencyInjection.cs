
using System;

namespace Membrane.Foundation.Pattern.Creational
{
    /// <summary>
    ///  Interface defining the API of dependency injection containter implemetations.
    /// </summary>
    public static class DependencyInjection
    {
        #region - Constructors -

        /// <summary>
        /// Static constructor.
        /// </summary>
        static DependencyInjection()
        {
            DependencyInjection.container = ServiceLocator.Get<IDependencyContainer>();
        }

        #endregion
        
        #region - Private fields -

        /// <summary>
        /// Holds a reference to a concrete implementation of <see cref="IDependencyContainer"/>
        /// </summary>
        private static readonly IDependencyContainer  container;

        #endregion

        /// <summary>
        /// Gets an instance of the requested type.
        /// </summary>
        /// <typeparam name="T">The type of the requested instance.</typeparam>
        /// <returns>The instance.</returns>
        public static T Get<T>()
        {
            return DependencyInjection.container.Get<T>();
        }

        /// <summary>
        /// Gets an instance of the requested type.
        /// </summary>
        /// <typeparam name="T">The type of the requested instance.</typeparam>
        /// <param name="parameters">The set of parameters to inject into the constructor.</param>
        /// <returns>The instance.</returns>
        public static T Get<T>(params InjectionParameter[] parameters)
        {
            return DependencyInjection.container.Get<T>(parameters);
        }
    }
}
