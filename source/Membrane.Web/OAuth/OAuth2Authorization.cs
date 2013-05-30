using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OAuth2;
using Membrane.Web.OAuth;

namespace Membrane.Web
{
	public class OAuth2Authorization
		: AuthorizeAttribute
	{
		#region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
		public OAuth2Authorization()
            : base() 
		{
			this.InitializeObject();
		}

		/// <summary>
		/// Constructor with parameter.
		/// </summary>
		/// <param name="requiredScope">The required scope to approve this request.</param>
		public OAuth2Authorization(string requiredScope)
			: this()
		{
			this.RequiredScopes = new string[] { requiredScope };
		}

		/// <summary>
		/// Constructor with parameter.
		/// </summary>
		/// <param name="requiredScopes">The set of required scopes to approve this request.</param>
		public OAuth2Authorization(string[] requiredScopes)
			: this()
		{
			this.RequiredScopes = requiredScopes;
		}

        #endregion

		/// <summary>
		/// The set of required scopes to approve this request.
		/// </summary>
		public string[] RequiredScopes { get; set; }

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			bool isAuthorized = false;

			try
			{
				HttpRequestBase request = httpContext.Request;

				if (!string.IsNullOrEmpty(request.Headers["Authorization"]))
				{
					if (request.Headers["Authorization"].StartsWith("Bearer "))
					{
						RSACryptoServiceProvider authorizationServerSigningPublicKey = AuthorizationServerHost.CreateRsaCryptoServiceProvider(AuthorizationServerHost.AuthorizationServerSigningPublicKey);
						RSACryptoServiceProvider resourceServerEncryptionPrivateKey = AuthorizationServerHost.CreateRsaCryptoServiceProvider(AuthorizationServerHost.ResourceServerEncryptionPrivateKey);

						StandardAccessTokenAnalyzer tokenAnalyzer = new StandardAccessTokenAnalyzer(authorizationServerSigningPublicKey, resourceServerEncryptionPrivateKey);
						ResourceServer resourceServer = new ResourceServer(tokenAnalyzer);
						IPrincipal principal = resourceServer.GetPrincipal(request);

						if (principal.Identity.IsAuthenticated)
						{
							HttpContext.Current.User = principal;
							Thread.CurrentPrincipal = principal;

							isAuthorized = true;
						}

						var _token = resourceServer.GetAccessToken(request);

						if (this.RequiredScopes.Any())
						{
							var token = resourceServer.GetAccessToken(request, this.RequiredScopes);
						}
					}
				}
			}
			catch 
			{
				isAuthorized = false;
			}

			return isAuthorized;
		}

		#region - Protected & private methods  -

		/// <summary>
		/// Initializes the object.
		/// </summary>
		private void InitializeObject()
		{
			this.RequiredScopes = new string[] { };
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = new HttpUnauthorizedResult("invalid_token");
		}

		#endregion
	}
}
