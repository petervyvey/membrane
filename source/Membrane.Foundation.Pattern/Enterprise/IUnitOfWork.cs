
using System;
using System.Transactions;

namespace Membrane.Foundation.Pattern.Enterprise
{
	/// <summary>
	/// Interface defining the API for a Unit of Work.
	/// </summary>
	public interface IUnitOfWork
		: IDisposable
	{
		/// <summary>
		/// Executes the Unit of Work.
		/// </summary>
        /// <param name="isolationLevel">The transaction isolation level.</param>
        T Start<T>(Func<T> commit, Func<T> rollback, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
	}
}
