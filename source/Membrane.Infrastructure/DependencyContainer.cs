
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Ninject;
using Ninject.Parameters;
using Membrane.Foundation.Pattern.Creational;

namespace Membrane.Infrastructure
{
    /// <summary>
    /// Wrapper class for the Ninject Dependency Injection implemetation.
    /// </summary>
    [Export(typeof(IDependencyContainer))]
    public sealed class DependencyContainer
        : IDependencyContainer
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DependencyContainer()
            : base()
        {
            this.kernel.Load(DependencyContainer.DEFAULT_ASSEMBLY_FILENAME_PATTERN);
        }

        #endregion

        #region - Constants & static fields -

        /// <summary>
        /// The default filename pattern to search and load types into the dependency injection container.
        /// </summary>
        private const string DEFAULT_ASSEMBLY_FILENAME_PATTERN = "*.dll";

        #endregion

        #region - Private fields -

        /// <summary>
        /// A Ninject <see cref="IKernel"/> implementation.
        /// </summary>
        private readonly IKernel kernel = new StandardKernel();

        #endregion

        /// <summary>
        /// Gets an instance of the requested type.
        /// </summary>
        /// <typeparam name="T">The type of the requested instance.</typeparam>
        /// <returns>The instance.</returns>
        public T Get<T>()
        {
            return this.kernel.Get<T>();
        }

        /// <summary>
        /// Gets an instance of the requested type.
        /// </summary>
        /// <typeparam name="T">The type of the requested instance.</typeparam>
        /// <param name="parameters">The set of parameters to inject into the constructor.</param>
        /// <returns>The instance.</returns>
        public T Get<T>(params InjectionParameter[] parameters)
        {
            List<ConstructorArgument> constructorArguments = new List<ConstructorArgument>();
            parameters.ToList().ForEach(keyvalue =>
            {
                constructorArguments.Add(new ConstructorArgument(keyvalue.Name, keyvalue.Value));
            });

            return this.kernel.Get<T>(constructorArguments.ToArray());
        }
    }
}
