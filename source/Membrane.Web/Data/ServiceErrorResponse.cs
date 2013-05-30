
namespace Membrane.Web.Data
{
    /// <summary>
    /// The service error response container.
    /// </summary>
    public sealed class ServiceErrorResponse
        : ServiceResponse
    {
        #region - Constructors -

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="version">The API version of the response.</param>
        /// <param name="status">The response status.</param>
        /// <param name="error">The <see cref="ServiceError"/> instance containing the error details.</param>
        public ServiceErrorResponse(string version, ResponseStatus status, ServiceError error)
            : base(version, status)
        {
            this.Error = error;
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="version">The API version of the response.</param>
        /// <param name="endPoint">The API service endpoint.</param>
        /// <param name="status">The response status.</param>
        /// <param name="error">The <see cref="ServiceError"/> instance containing the error details.</param>
        public ServiceErrorResponse(string version, string endPoint, ResponseStatus status, ServiceError error)
            : base(version, endPoint, status)
        {
            this.Error = error;
        }

        #endregion

        /// <summary>
        /// The error details.
        /// </summary>
        public ServiceError Error { get; private set; }
    }
}