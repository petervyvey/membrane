
namespace Membrane.Foundation.Pattern.Creational
{
	/// <summary>
	/// Generic static class to host any object with a parameterless constructor as a singleton.
	/// </summary>
	/// <typeparam name="T">Type to be hosted as a singleton.</typeparam>
	public static class Singleton<T> 
		where T : class, new()
	{
		#region - Nested classes: Singleton.SingletonCreator -

		/// <summary>
        /// Internal class containing the actual singleton instance.
        /// </summary>
        private static class SingletonCreator
        {
            #region - Static constructors -

            /// <summary>
            /// Static constructor.
            /// </summary>
            static SingletonCreator() { }

            #endregion

            #region - Internal static fields -

            /// <summary>
            /// Field to hold the actual instance of the singleton.
            /// </summary>
            internal static readonly T Instance = new T();

            #endregion
        }

        #endregion

        #region - Static constructors -

        /// <summary>
        /// Static constructor.
        /// </summary>
        static Singleton() { }

        #endregion

		/// <summary>
		/// Publicly available property holding the singleton instance.
		/// </summary>
		public static T Instance
		{
            get { return SingletonCreator.Instance; }
		}
	}
}
