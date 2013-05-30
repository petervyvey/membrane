
using System;

namespace Membrane.Foundation.Pattern.Creational
{
    /// <summary>
    ///  Interface defining the API of dependency injection containter implemetations.
    /// </summary>
    public interface IDependencyContainer
    {
        /// <summary>
        /// Gets an instance of the requested type.
        /// </summary>
        /// <typeparam name="T">The type of the requested instance.</typeparam>
        /// <returns>The instance.</returns>
        T Get<T>();

        /// <summary>
        /// Gets an instance of the requested type.
        /// </summary>
        /// <typeparam name="T">The type of the requested instance.</typeparam>
        /// <param name="parameters">The set of parameters to inject into the constructor.</param>
        /// <returns>The instance.</returns>
        T Get<T>(params InjectionParameter[] parameters);
    }
}
