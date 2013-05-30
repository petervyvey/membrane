
using System;
using System.Diagnostics;
using Membrane.Foundation.Extension;
using Membrane.Foundation.Runtime.Logging;

namespace Membrane.Foundation.ExceptionManagement
{
	/// <summary>
	/// Handles exception raised in the ApplicationModel workspace.
	/// </summary>
	public static class ExceptionHandler
    {
        #region - Constants -

		/// <summary>
		/// Template to format the exception.
		/// </summary>
        private const string EXCEPTION_FORMAT = "{0} (Result: 0x{1}). Exception ID: {2}).";

		/// <summary>
		/// Template to format an exception with extended information.
		/// </summary>
        private const string EXCEPTION_FORMAT_EXTENDED = "{4}.\r\n{0}\\{2}\\0x{1}.\r\nException ID: {3}).";

        #endregion

		/// <summary>
		/// Formats and handles an exception.
		/// </summary>
		/// <param name="ex">The <see cref="ApplicationModelException"/> to handle.</param>
		/// <returns>A string message that can be send to client applications.</returns>
        public static string FormatServiceException(ApplicationModelException ex)
        {
            return ExceptionHandler.FormatServiceException(ex, false);
        }

		/// <summary>
		/// Formats and handles an exception.
		/// </summary>
		/// <param name="ex">The <see cref="ApplicationModelException"/> to handle.</param>
		/// <param name="isExtended">TRUE if the message should be in extended format, FALSE otherwise.</param>
		/// <returns>A string message that can be send to client applications.</returns>
        public static string FormatServiceException(ApplicationModelException ex, bool isExtended)
        {
            if (isExtended)
            {
                return string.Format(ExceptionHandler.EXCEPTION_FORMAT_EXTENDED, ex.GetType().Name.Humanize(), Enum.Format(typeof(ExceptionNumber), ex.Number, "x"), ex.Number, ex.ID, ex.Message);
            }
            else
            {
                return string.Format(ExceptionHandler.EXCEPTION_FORMAT, ex.GetType().Name.Humanize(), Enum.Format(typeof(ExceptionNumber), ex.Number, "x"), ex.ID);
            }
        }

		/// <summary>
		/// Checks if the exception is handled.
		/// </summary>
		/// <param name="ex">The exception.</param>
		/// <returns>TRUE if the exception if the exception has not been handled, FALSE otherwise.</returns>
		public static bool IsUnhandledException(Exception ex)
		{
			bool isUnhandled = true;

			if (ex is ApplicationModelException)
			{
				ApplicationModelException exceptionWrapper = (ApplicationModelException)ex;
				isUnhandled = exceptionWrapper.IsUnhandled;
			}

			return isUnhandled;
		}

		/// <summary>
		/// Handles an exception and wraps it into an <see cref="ApplicationModelException"/> of type T as needed.
		/// </summary>
		/// <typeparam name="T">The type of <see cref="ApplicationModelException"/> to wrap the original exception into.</typeparam>
        /// <param name="number">The excpttion number.</param>
		/// <param name="ex">The exception.</param>
		/// <param name="policyName">The name of the exception policy to apply.</param>
		/// <returns>An <see cref="ApplicationModelException"/> of type T.</returns>
        public static T HandleException<T>(long number, Exception ex, string policyName) 
            where T : ApplicationModelException
		{
			ex.CatchNullArgument("ex");
			policyName.CatchNullOrEmptyArgument("policyName");

			bool shouldWrap = false;

			T outerException = default(T);

			// First handle the execption by 
			//	- logging it to the exception log
			//	- if ex is of type ExceptionWrapper also set the IsUnhandled flag to FALSE
			//	- if ex is NOT of type ExceptionWrapper then just log the exception
			if (ex is ApplicationModelException)
			{
				ApplicationModelException wrapperException = (ApplicationModelException)ex;

				if (wrapperException.IsUnhandled)
				{
					// Write to exception log
                    Logger.Write(ex, policyName, TraceLevel.Error);

					// Ok ... this is handled
					wrapperException.IsUnhandled = false;
				}

                shouldWrap = typeof(T) != wrapperException.GetType();

                if (!shouldWrap)
                {
                    outerException = (T)wrapperException;
                }
			}
			else
			{
				// Write to exception log
                Logger.Write(ex, policyName, TraceLevel.Error);

				// Only wrap if we have a different type of exception
				shouldWrap = typeof(T) != typeof(Exception);
			}

            if (shouldWrap)
            {
                // Create an instance of the requested ExceptionWrapper subtype
                outerException = (T)Activator.CreateInstance(typeof(T), new object[] { number, ex.Message, ex });

                // If it is a ExceptionWrapper type also set the IsUnhandled flag to FALSE
                if (outerException is ApplicationModelException)
                {
                    (outerException as ApplicationModelException).IsUnhandled = false;
                }
            }

			return outerException;
		}
	}
}
