
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Membrane.Foundation.DataTransfer;
using Membrane.Foundation.Model;
using Membrane.Foundation.Pattern.Creational;
using Membrane.Foundation.Security.Authentication;
using Membrane.Web.Data;
using Membrane.Web.Infrastructure;

namespace Membrane.Web.Public.Areas.Api.Controllers
{
	public class RestVersion0100Controller
			: Controller
	{
		#region - Constants & static fields -

		/// <summary>
		/// The API version this controller supports.
		/// </summary>
		internal const string API_VERSION = "1.0.0.0";

		/// <summary>
		/// The prefix of request parameters.
		/// </summary>
		private const string REQUEST_PARAMETER_PREFIX = "*";

		#endregion

		/// <summary>
		/// Gets the resource of the specified resource name and identifier.
		/// </summary>
		/// <param name="resource">The resource name.</param>
		/// <param name="identifier">The resource identifier.</param>
		/// <returns></returns>
		[HttpGet]
		[OAuth2Authorization]
		[DataServiceActionFilter]
		public async Task<JsonResult> Get(string resource, Guid identifier)
		{
			ServiceResponse response = default(ServiceResponse);

			using (Session session = ApplicationModel.Current.CreateSession(new SecurityToken(this.HttpContext.Request.Url.Host, this.HttpContext.User.Identity.Name)))
			{
				try
				{
					IRestPortal<DataTransferObject> portal = DependencyInjection.Get<IRestPortal<DataTransferObject>>(InjectionParameter.Create("resource", resource));
					DataTransferObject value = portal.Get(identifier);

					if (default(DataTransferObject) == value) HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

					ResponseStatus status = default(DataTransferObject) != value ? ResponseStatus.OK : ResponseStatus.NO_DATA;
					response = new ServiceDataResponse(RestVersion0100Controller.API_VERSION, status, value);
				}
				catch (Exception ex)
				{
					response = new ServiceErrorResponse(RestVersion0100Controller.API_VERSION, ResponseStatus.ERROR, new ServiceError(ex));
				}
			}

			return this.Json(response, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="resource">The resource name.</param>
		/// <returns>A set of resource items.</returns>
		[HttpGet]
		[OAuth2Authorization("application.role.user.default")]
		[DataServiceActionFilter]
		public async Task<JsonResult> GetSet(string resource)
		{
			ServiceResponse response = default(ServiceResponse);

			using (Session session = ApplicationModel.Current.CreateSession(new SecurityToken(this.HttpContext.Request.Url.Host, this.HttpContext.User.Identity.Name)))
			{
				try
				{
					IDictionary<string, string> parameters = this.GetParameters();

					IRestPortal<DataTransferObject> portal = DependencyInjection.Get<IRestPortal<DataTransferObject>>(InjectionParameter.Create("resource", resource));
					IEnumerable<DataTransferObject> set = portal.Get(parameters);

					ResponseStatus status = ResponseStatus.OK;
					response = new ServiceDataResponse(RestVersion0100Controller.API_VERSION, status, set);
				}
				catch (Exception ex)
				{
					response = new ServiceErrorResponse(RestVersion0100Controller.API_VERSION, ResponseStatus.ERROR, new ServiceError(ex));
				}
			}

			return this.Json(response, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="resource">The resource name.</param>
		/// <param name="data"></param>
		/// <returns></returns>
		[HttpPost]
		[OAuth2Authorization]
		[DataServiceActionFilter]
		public async Task<JsonResult> Post(string resource, dynamic data)
		{
			var value = new { value = "value" };

			return this.Json(value, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="resource">The resource name.</param>
		/// <param name="data"></param>
		/// <returns></returns>
		[HttpPut]
		[Authorize]
		[DataServiceActionFilter]
		public async Task<JsonResult> Put(string resource, dynamic data)
		{
			object result = null;
			return this.Json(result);
		}

		/// <summary>
		/// Deletes the resource with the given identifier.
		/// </summary>
		/// <param name="resource">The resource name.</param>
		/// <param name="identifier">The resource identifier.</param>
		/// <returns>TRUE if the resource was deleted successfully, FALSE otherwise.</returns>
		[HttpDelete]
		[OAuth2Authorization]
		[DataServiceActionFilter]
		public async Task<JsonResult> Delete(string resource, int identifier)
		{
			object result = null;
			return this.Json(result);
		}

		#region - Private & protected methods -

		/// <summary>
		/// <para>
		/// Creates a <see cref="JsonResult"/> object that serializes the specified object to JavaScript Object Notation (JSON) format
		/// using the content type, content encoding, and the JSON request behavior.
		/// </para>
		/// <para>
		/// This override uses Json.Net to serialize the object graph.
		/// </para>
		/// </summary>
		/// <param name="data">The javascript object graph to serialize.</param>
		/// <param name="contentType">The content type MIME type.</param>
		/// <param name="contentEncoding">The content encoding.</param>
		/// <returns>The result object that serializes the specified object to JSON format.</returns>
		protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
		{
			return new JsonNetResult(data, contentType, contentEncoding);
		}

		/// <summary>
		/// <para>
		/// Creates a <see cref="JsonResult"/> object that serializes the specified object to JavaScript Object Notation (JSON) format
		/// using the content type, content encoding, and the JSON request behavior.
		/// </para>
		/// <para>
		/// This override uses Json.Net to serialize the object graph.
		/// </para>
		/// </summary>
		/// <param name="data">The javascript object graph to serialize.</param>
		/// <param name="contentType">The content type MIME type.</param>
		/// <param name="contentEncoding">The content encoding.</param>
		/// <param name="behavior">The JSON request behavior.</param>
		/// <returns>The result object that serializes the specified object to JSON format.</returns>
		protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return new JsonNetResult(data, contentType, contentEncoding, behavior);
		}

		/// <summary>
		/// Convert the HTTP request parameters to an dictionary.
		/// </summary>
		/// <returns>The dictionary with filter values.</returns>
		private IDictionary<string, string> GetParameters()
		{
			Dictionary<string, string> dictionary =
				this.Request.QueryString.AllKeys.Where(x => x.StartsWith(RestVersion0100Controller.REQUEST_PARAMETER_PREFIX)).ToList()
					.Select(key => new { Key = key.TrimStart('*'), Value = this.Request[key] })
					.ToDictionary(x => x.Key, x => x.Value, StringComparer.InvariantCultureIgnoreCase);

			ConcurrentDictionary<string, string> parameters = new ConcurrentDictionary<string, string>(dictionary, StringComparer.InvariantCultureIgnoreCase);

			return parameters;
		}

		#endregion
	}
}
