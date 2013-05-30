
using System;
using System.Transactions;
using Membrane.Foundation.Extension;
using Membrane.Foundation.Pattern.Enterprise;
using Membrane.Infrastructure;

namespace Membrane.Service.ApplicationService
{
	/// <summary>
	/// Implementation of the Unit of Work pattern to process an <see cref="Action"/> with transactional operations on domain entities.
	/// </summary>
	public sealed class TransactionalUnitOfWork
		: UnitOfWork
	{
		#region - Constructors - 

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TransactionalUnitOfWork()
			: base() { }

		#endregion

		#region - Destructor -

        /// <summary>
        /// Destructor.
        /// </summary>
		~TransactionalUnitOfWork()
        {
            this.Dispose(false);
        }

        #endregion

        /// <summary>
        /// Executes the Unit of Work.
        /// </summary>
        /// <param name="isolationLevel">The transaction isolation level.</param>
        public override T Start<T>(Func<T> commit, Func<T> rollback, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            commit.CatchNullArgument("commit");
            rollback.CatchNullArgument("rollback");

            T result = default(T);

            try
            {
                TransactionOptions options = new TransactionOptions { IsolationLevel = isolationLevel };
                using (TransactionScope scope = new TransactionScope(DomainConstant.TRANSACTION_SCOPE_OPTION_REQUIRED, options))
                {
                    result = commit.Invoke();
                    scope.Complete();
                }
            }
            catch
            {
                result = rollback.Invoke();
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
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
		}

		#endregion
	}
}
