using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth2;
using Membrane.Foundation.Pattern.Creational;

namespace Membrane.Web.Public.Areas.OAuth.Controllers
{
	public class AuthorizationController
		: Controller
	{
		/// <summary>
		/// Create an instance of the authorization server, an implementation of <see cref="IAuthorizationServerHost"/>.
		/// </summary>
		private readonly AuthorizationServer authorizationServer = new AuthorizationServer(DependencyInjection.Get<IAuthorizationServerHost>());

		[HttpPost]
		public ActionResult Index()
		{
			// Have the authorization server handle the token request. 
			// It will use the passed-in request to determine what the actual token request is. 
			// If the request does not contain a valid token request, an exception is thrown
			OutgoingWebResponse outgoingWebResponse = this.authorizationServer.HandleTokenRequest(this.Request);

			// Convert the outgoing web response to an ActionResult to correctly integrate with ASP.NET MVC flow
			return outgoingWebResponse.AsActionResult();
		}
	}
}
