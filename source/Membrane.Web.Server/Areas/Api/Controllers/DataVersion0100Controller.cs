using System;
using System.Dynamic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Membrane.DataTransfer;
using Membrane.Foundation.Model;
using Membrane.Foundation.Pattern.Creational;
using Membrane.Foundation.Security.Authentication;
using Membrane.Service.ApplicationService;
using Membrane.Web.Data;
using Membrane.Web.Infrastructure;
using Membrane.Web.Utils;
using Newtonsoft.Json;

namespace Membrane.Web.Public.Areas.Api.Controllers
{
    public class DataVersion0100Controller : Controller
    {
		[HttpGet]
		[OAuth2Authorization]
		public async Task<JsonResult> GetUserIdentity()
		{
			ServiceResponse response = default(ServiceResponse);

			using (Session session = ApplicationModel.Current.CreateSession(new SecurityToken(this.HttpContext.Request.Url.Host, this.HttpContext.User.Identity.Name)))
			{
				try
				{
					IDataPortal portal = DependencyInjection.Get<IDataPortal>();
					var value = portal.GetUserIdentity();

					// Set the status on HTTP and response level.
					if (value == null) HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					ResponseStatus status = value == null ? ResponseStatus.NO_DATA : ResponseStatus.OK;

					response = new ServiceDataResponse(RestVersion0100Controller.API_VERSION, status, value);
				}
				catch (Exception ex)
				{
					HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					response = new ServiceErrorResponse(RestVersion0100Controller.API_VERSION, ResponseStatus.ERROR, new ServiceError(ex));
				}
			}

			return this.Json(response, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		[OAuth2Authorization]
		public async Task<JsonResult> SignUpUser(OAuth2UserIdentity userIdentity)
		{
			ServiceResponse response = default(ServiceResponse);

			using (Session session = ApplicationModel.Current.CreateSession(new SecurityToken(this.HttpContext.Request.Url.Host, this.HttpContext.User.Identity.Name)))
			{
				try
				{
					IDataPortal portal = DependencyInjection.Get<IDataPortal>();
					OAuth2UserIdentity value = portal.SignUpUser(userIdentity);

					// Set the status on HTTP and response level.
					if (value == null) HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					ResponseStatus status = value == null ? ResponseStatus.NO_DATA : ResponseStatus.OK;

					response = new ServiceDataResponse(RestVersion0100Controller.API_VERSION, status, value);
				}
				catch (Exception ex)
				{
					HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					response = new ServiceErrorResponse(RestVersion0100Controller.API_VERSION, ResponseStatus.ERROR, new ServiceError(ex));
				}
			}

			return this.Json(response, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		[OAuth2Authorization]
		public async Task<JsonResult> CalculateBrutoNetto(dynamic parameters)
		{
			ServiceResponse response = default(ServiceResponse);

			using (Session session = ApplicationModel.Current.CreateSession(new SecurityToken(this.HttpContext.Request.Url.Host, this.HttpContext.User.Identity.Name)))
			{
				try
				{
					HttpRequestAgent connector = new HttpRequestAgent();
					//string value = connector.Post("https://brutnet.attentia.be/GetBrutoNettoBerekening?onlyValidate=true", parameters);
					string value = "{ \"Bruto\": \"2500.0\", \"Netto\": \"1250.00\" }";

					if (string.IsNullOrEmpty(value)) HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					ResponseStatus status = string.IsNullOrEmpty(value) ? ResponseStatus.NO_DATA : ResponseStatus.OK;

					response = new ServiceDataResponse(RestVersion0100Controller.API_VERSION, status, value);
				}
				catch (Exception ex)
				{
					HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					response = new ServiceErrorResponse(RestVersion0100Controller.API_VERSION, ResponseStatus.ERROR, new ServiceError(ex));
				}
			}

			return this.Json(response, JsonRequestBehavior.AllowGet);
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

		#endregion
	}
}
