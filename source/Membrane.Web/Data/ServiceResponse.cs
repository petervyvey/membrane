
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Membrane.Web.Data
{
    /// <summary>
    /// The abstract base class for API service responses.
    /// </summary>
    public abstract class ServiceResponse
    {
        #region - Constructors -

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="version">The API version of the response.</param>
        /// <param name="status">The response status.</param>
        public ServiceResponse(string version, ResponseStatus status)
            : base()
        {
            this.Version = version;
            this.Status = status.ToString();

            this.EndPoint = this.ResolveEndpoint();
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="version">The API version of the response.</param>
        /// <param name="endPoint">The API service endpoint.</param>
        /// <param name="status">The response status.</param>
        public ServiceResponse(string version, string endPoint, ResponseStatus status)
            : this(version, status)
        {
            this.EndPoint = endPoint;
        }

        #endregion

        /// <summary>
        /// The template to build the server URL.
        /// </summary>
        private const string SERVER_URL_TEMPLATE = "{0}://{1}";

        /// <summary>
        /// The template to construct a request path.
        /// </summary>
        public const string REQUEST_PATH_TEMPLATE = "{0}:{1}/{2}/{3}";

        /// <summary>
        /// The API version of the response.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The API service endpoint.
        /// </summary>
        public string EndPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        private string ResolveEndpoint()
        {
            string route = ((System.Web.Routing.Route)((MvcHandler)HttpContext.Current.CurrentHandler).RequestContext.RouteData.Route).Url;
            string serverUrl = string.Format(ServiceResponse.REQUEST_PATH_TEMPLATE, HttpContext.Current.Request.ServerVariables["SERVER_NAME"], HttpContext.Current.Request.Url.Port, HttpContext.Current.Request.ApplicationPath, route).TrimEnd("/".ToCharArray());
            serverUrl = string.Format(SERVER_URL_TEMPLATE, HttpContext.Current.Request.Url.Scheme, string.Join("/", serverUrl.Split('/').Where(x => !string.IsNullOrEmpty(x)).Where(x => !x.StartsWith("{") && !x.EndsWith("}")).ToArray()));

            return serverUrl;
        }
    }
}