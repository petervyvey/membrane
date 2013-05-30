
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Membrane.Foundation.Pattern.Creational
{
    /// <summary>
    /// Service locator implementation.
    /// </summary>
    internal static class ServiceLocator
    {
        #region - Constructors -

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ServiceLocator()
        {
            ServiceLocator.Initialize();
        }

        #endregion
        
        #region - Private fields -

        /// <summary>
        /// The composition container.
        /// </summary>
        private static CompositionContainer container;

        /// <summary>
        /// The catalog.
        /// </summary>
        private static AggregateCatalog catalog;

        #endregion

        /// <summary>
        /// Gets an exported instance of the requested type.
        /// </summary>
        /// <typeparam name="T">The type of the requested instance.</typeparam>
        /// <returns>The exported instance.</returns>
        public static T Get<T>()
        {
            return ServiceLocator.container.GetExportedValue<T>();
        }

        /// <summary>
        /// Gets an exported instances of the requested type.
        /// </summary>
        /// <typeparam name="T">The type of the requested instance.</typeparam>
        /// <returns>The exported instances.</returns>
        public static IEnumerable<T> GetAll<T>()
        {
            return ServiceLocator.container.GetExportedValues<T>();
        }

        #region - Private & protected methods -

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private static void Initialize()
        {
            string path = string.Empty;

            try
            {
                path = HttpRuntime.CodegenDir;
            }
            catch
            {
                path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            }
            List<string> fileNames = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories).ToList();

            // Create the aggregate catalog and try to load all available assemblies.
            ServiceLocator.catalog = new AggregateCatalog();
            fileNames.ForEach(fileName =>
            {
                AssemblyCatalog assemblyCatalog = new AssemblyCatalog(Assembly.LoadFrom(fileName));
                ServiceLocator.catalog.Catalogs.Add(assemblyCatalog);
            });
            // Add the catalog to the container.
            ServiceLocator.container = new CompositionContainer(ServiceLocator.catalog);
        }

        #endregion
    }
}
