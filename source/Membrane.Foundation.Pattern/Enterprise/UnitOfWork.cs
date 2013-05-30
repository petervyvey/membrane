

using System;
using System.Transactions;
using Membrane.Foundation.Extension;
using Membrane.Foundation.Pattern.Creational;

namespace Membrane.Foundation.Pattern.Enterprise
{
	/// <summary>
	/// Base class for the domain entity classes.
	/// </summary>
    public abstract class UnitOfWork
        : IUnitOfWork
	{
		#region - Constructors - 

		/// <summary>
		/// Default constructor.
		/// </summary>
		protected UnitOfWork()
			: base() { }

		#endregion

		#region - Destructor -

        /// <summary>
        /// Destructor.
        /// </summary>
		~UnitOfWork()
        {
            this.Dispose(false);
        }

        #endregion

		#region - Private fields -

		/// <summary>
		/// TRUE if the object is disposed, FALSE otherwise.
		/// </summary>
		private bool isDisposed = false;

		#endregion

        /// <summary>
		/// Disposes the object.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

        /// <summary>
        /// Executes the Unit of Work.
        /// </summary>
        /// <param name="isolationLevel">The transaction isolation level.</param>
        public abstract T Start<T>(Func<T> commit, Func<T> rollback, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Executes a Unit of Work.
        /// </summary>
        /// <typeparam name="T">The result type of the Unit of Work.</typeparam>
        /// <param name="commit">The function to execute during the Unit of Work.</param>
        /// <returns>An instance type T.</returns>
        public static T Execute<T>(Func<T> commit, Func<T> rollback = null, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            commit.CatchNullArgument("commit");

            T result = default(T);
            IUnitOfWork unitOfWork = null;

            try
            {
                // Set the default ROLLBACK delegate if we don't have any.
                if (rollback == null) rollback = () => { return default(T); };

                // Create the unit of work.
                unitOfWork = DependencyInjection.Get<IUnitOfWork>();
                result = unitOfWork.Start(commit, rollback, isolationLevel);
            }
            finally
            {
                unitOfWork.Dispose();
            }

            return result;
        }

		#region - Private & protected methods -

		/// <summary>
		/// Disposes managed and unmanaged resources.
		/// </summary>
		/// <param name="isDisposing">Flag indicating how this protected method was called. 
		/// TRUE means via Dispose(), FALSE means via the destructor.
		/// Only in case of a call through the Dispose() method should managed resources be freed.</param>
		protected virtual void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				// Free managed resources here
			}

			if (!this.isDisposed)
			{

			}

			this.isDisposed = true;
		}

		#endregion
	}
}
