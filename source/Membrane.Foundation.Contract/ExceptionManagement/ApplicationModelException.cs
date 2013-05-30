
using System;
using System.Runtime.Serialization;

namespace Membrane.Foundation.ExceptionManagement
{
	/// <summary>
	/// The base class for ApplicationModel exceptions.
	/// </summary>
	public abstract class ApplicationModelException : Exception
	{
		#region - Constructors -

		/// <summary>
		/// Initializes a new instance of the WrappedException class.
		/// </summary>
        public ApplicationModelException()
            : base()
        {
            this.InitializeObject();
        }

        /// <summary>
        /// Initializes a new instance of the WrappedException class.
        /// </summary>
        /// <param name="number">The exception type that will escalate to the presentation layer.</param>
        public ApplicationModelException(long number)
            : base(string.Format("0x{0:X}", number))
        {
            this.Number = number;
            this.InitializeObject();
        }

		/// <summary>
		/// Initializes a new instance of the WrappedException class with a specified error message.
		/// </summary>
		/// <param name="message"> The message that describes the error.</param>
        public ApplicationModelException(string message)
            : base(message)
        {
            this.InitializeObject();
        }

        /// <summary>
        /// Initializes a new instance of the WrappedException class with a specified error message.
		/// </summary>
        /// <param name="number">The exception type that will escalate to the presentation layer.</param>
        /// <param name="message"> The message that describes the error.</param>
        public ApplicationModelException(long number, string message)
            : base(message)
        {
            this.Number = number;
            this.InitializeObject();
        }

		/// <summary>
		/// Initializes a new instance of the WrappedException class with a specified
		/// error message and a reference to the inner exception that is the cause of
		/// this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ApplicationModelException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.InitializeObject();
        }

        /// <summary>
        /// Initializes a new instance of the WrappedException class with a specified
        /// error message and a reference to the inner exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="number">The exception type that will escalate to the presentation layer.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ApplicationModelException(long number, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Number = number;
            this.InitializeObject();
        }

		/// <summary>
		/// Initializes a new instance of the WrappedException class with serialized
		/// data.
		/// </summary>
		/// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected ApplicationModelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.InitializeObject();
        }

		#endregion

		/// <summary>
		/// The exception ID.
		/// </summary>
		public Guid ID { get; set; }

		/// <summary>
		/// Flag indicating if the exception was handled by the ExceptionHandler.
		/// </summary>
		public bool IsUnhandled { get; set; }

		/// <summary>
		/// The exception number.
		/// </summary>
        public long Number { get; set; }

		#region - Private & protected methods -

		/// <summary>
		/// Initializes the object.
		/// </summary>
		private void InitializeObject()
		{
            this.ID = Guid.NewGuid();
			this.IsUnhandled = true;
		}

		#endregion
	}
}
