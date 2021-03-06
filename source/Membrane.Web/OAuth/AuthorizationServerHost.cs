﻿
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using DotNetOpenAuth.Messaging.Bindings;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.OAuth2.ChannelElements;
using DotNetOpenAuth.OAuth2.Messages;
using DotNetOpenAuth.OpenId.Provider;
using Membrane.Domain.Agent;
using Membrane.Domain.Entity;
using Membrane.Foundation.Pattern.Creational;
using Newtonsoft.Json;
using Membrane.DataTransfer.Projection;

namespace Membrane.Web.OAuth
{
    /// <summary>
    /// Our implementation of the <see cref="IAuthorizationServerHost"/> interface. 
    /// This class will be the entry to the OAuth 2.0 server, as it will handle all token requests.
    /// </summary>
    public class AuthorizationServerHost 
        : IAuthorizationServerHost
    {
        /// <summary>
        /// The default resource server public key used for encrypting access tokens. In a real-life situation, you would
        /// get this from a certificate file.
        /// </summary>
		internal static readonly RSAParameters ResourceServerEncryptionPublicKey =
			new RSAParameters
			{
				Exponent = new byte[] { 1, 0, 1 },
				Modulus = new byte[] { 166, 175, 117, 169, 211, 251, 45, 215, 55, 53, 202, 65, 153, 155, 92, 219, 235, 243, 61, 170, 101, 250, 221, 214, 239, 175, 238, 175, 239, 20, 144, 72, 227, 221, 4, 219, 32, 225, 101, 96, 18, 33, 117, 176, 110, 123, 109, 23, 29, 85, 93, 50, 129, 163, 113, 57, 122, 212, 141, 145, 17, 31, 67, 165, 181, 91, 117, 23, 138, 251, 198, 132, 188, 213, 10, 157, 116, 229, 48, 168, 8, 127, 28, 156, 239, 124, 117, 36, 232, 100, 222, 23, 52, 186, 239, 5, 63, 207, 185, 16, 137, 73, 137, 147, 252, 71, 9, 239, 113, 27, 88, 255, 91, 56, 192, 142, 210, 21, 34, 81, 204, 239, 57, 60, 140, 249, 15, 101 },
			};

		/// <summary>
		/// The FOR SAMPLE ONLY hard-coded private key used to decrypt access tokens intended for this resource server.
		/// </summary>
		/// <remarks>
		/// In a real app, the resource server would likely use its own HTTPS certificate or some other certificate stored securely
		/// on the server rather than hard-coded in compiled code for security and ease of changing the certificate in case it was compromised.
		/// </remarks>
		internal static readonly RSAParameters ResourceServerEncryptionPrivateKey = 
			new RSAParameters
			{
				Exponent = new byte[] { 1, 0, 1 },
				Modulus = new byte[] { 166, 175, 117, 169, 211, 251, 45, 215, 55, 53, 202, 65, 153, 155, 92, 219, 235, 243, 61, 170, 101, 250, 221, 214, 239, 175, 238, 175, 239, 20, 144, 72, 227, 221, 4, 219, 32, 225, 101, 96, 18, 33, 117, 176, 110, 123, 109, 23, 29, 85, 93, 50, 129, 163, 113, 57, 122, 212, 141, 145, 17, 31, 67, 165, 181, 91, 117, 23, 138, 251, 198, 132, 188, 213, 10, 157, 116, 229, 48, 168, 8, 127, 28, 156, 239, 124, 117, 36, 232, 100, 222, 23, 52, 186, 239, 5, 63, 207, 185, 16, 137, 73, 137, 147, 252, 71, 9, 239, 113, 27, 88, 255, 91, 56, 192, 142, 210, 21, 34, 81, 204, 239, 57, 60, 140, 249, 15, 101 },
				P = new byte[] { 227, 25, 96, 71, 220, 99, 11, 55, 15, 241, 153, 20, 32, 213, 68, 127, 246, 162, 153, 204, 98, 26, 10, 99, 46, 189, 35, 18, 162, 180, 184, 134, 230, 198, 156, 87, 52, 174, 74, 155, 163, 204, 252, 51, 232, 189, 135, 172, 88, 24, 52, 174, 72, 157, 81, 90, 118, 59, 142, 154, 152, 201, 62, 177 },
				Q = new byte[] { 187, 229, 223, 233, 118, 20, 5, 251, 85, 8, 196, 3, 220, 232, 38, 159, 15, 95, 174, 162, 36, 13, 138, 239, 16, 85, 220, 104, 4, 162, 174, 160, 234, 133, 156, 33, 117, 139, 22, 112, 108, 214, 97, 178, 100, 191, 13, 177, 164, 30, 124, 48, 33, 118, 21, 137, 38, 59, 191, 13, 183, 5, 16, 245 },
				DP = new byte[] { 225, 112, 117, 117, 160, 191, 233, 136, 53, 153, 158, 94, 174, 225, 71, 104, 200, 75, 77, 229, 232, 148, 245, 46, 212, 93, 9, 142, 28, 90, 206, 187, 140, 40, 41, 87, 32, 130, 204, 169, 136, 135, 154, 237, 100, 227, 144, 229, 115, 102, 68, 21, 167, 28, 20, 128, 122, 210, 80, 148, 3, 139, 243, 97 },
				DQ = new byte[] { 133, 252, 100, 207, 232, 184, 92, 143, 157, 82, 115, 220, 65, 81, 118, 0, 228, 136, 153, 81, 219, 157, 160, 157, 218, 171, 47, 81, 41, 69, 12, 123, 136, 224, 159, 182, 40, 72, 119, 70, 210, 5, 137, 131, 25, 94, 55, 152, 157, 236, 115, 40, 43, 36, 54, 53, 39, 131, 97, 56, 153, 114, 206, 101 },
				InverseQ = new byte[] { 129, 119, 84, 118, 29, 35, 194, 186, 96, 169, 7, 7, 200, 22, 187, 34, 72, 131, 200, 246, 79, 120, 49, 242, 8, 220, 74, 114, 195, 95, 90, 108, 80, 2, 212, 71, 125, 100, 184, 77, 203, 236, 64, 122, 108, 212, 150, 129, 66, 248, 218, 3, 186, 71, 213, 236, 142, 66, 33, 196, 150, 216, 138, 114 },
				D = new byte[] { 94, 20, 94, 119, 18, 92, 141, 13, 17, 238, 92, 80, 22, 96, 232, 82, 128, 164, 115, 195, 191, 119, 142, 202, 135, 210, 103, 8, 10, 11, 51, 60, 208, 207, 168, 179, 253, 164, 250, 80, 245, 42, 201, 128, 97, 123, 108, 161, 69, 63, 47, 49, 24, 150, 165, 139, 105, 214, 154, 104, 172, 159, 86, 208, 64, 134, 158, 156, 234, 125, 140, 210, 3, 32, 60, 28, 62, 154, 198, 21, 132, 191, 236, 10, 158, 12, 247, 159, 177, 77, 178, 53, 238, 95, 165, 9, 200, 28, 148, 242, 35, 70, 189, 121, 169, 248, 97, 91, 111, 45, 103, 1, 167, 220, 67, 250, 175, 89, 122, 238, 192, 144, 142, 248, 198, 101, 96, 129 },
			};

		/// <summary>
		/// The FOR SAMPLE ONLY hard-coded public key of the authorization server that is used to verify the signature on access tokens.
		/// </summary>
		/// <remarks>
		/// In a real app, the authorization server public key would likely come from the server's HTTPS certificate,
		/// but in any case would be determined by the authorization server and its policies.
		/// The hard-coded value used here is so this sample works well with the OAuthAuthorizationServer sample,
		/// which has the corresponding sample private key. 
		/// </remarks>
		public static readonly RSAParameters AuthorizationServerSigningPublicKey = 
			new RSAParameters
			{
				Exponent = new byte[] { 1, 0, 1 },
				Modulus = new byte[] { 210, 95, 53, 12, 203, 114, 150, 23, 23, 88, 4, 200, 47, 219, 73, 54, 146, 253, 126, 121, 105, 91, 118, 217, 182, 167, 140, 6, 67, 112, 97, 183, 66, 112, 245, 103, 136, 222, 205, 28, 196, 45, 6, 223, 192, 76, 56, 180, 90, 120, 144, 19, 31, 193, 37, 129, 186, 214, 36, 53, 204, 53, 108, 133, 112, 17, 133, 244, 3, 12, 230, 29, 243, 51, 79, 253, 10, 111, 185, 23, 74, 230, 99, 94, 78, 49, 209, 39, 95, 213, 248, 212, 22, 4, 222, 145, 77, 190, 136, 230, 134, 70, 228, 241, 194, 216, 163, 234, 52, 1, 64, 181, 139, 128, 90, 255, 214, 60, 168, 233, 254, 110, 31, 102, 58, 67, 201, 33 },
			};

        /// <summary>
        /// The authorization server signing key, which is used for private signing operations. In a real-life situation, you would
        /// get this from a certificate file.
        /// </summary>
		internal static readonly RSAParameters AuthorizationServerSigningPrivateKey = 
			new RSAParameters
            {
                Exponent = new byte[] { 1, 0, 1 },
                Modulus = new byte[] { 210, 95, 53, 12, 203, 114, 150, 23, 23, 88, 4, 200, 47, 219, 73, 54, 146, 253, 126, 121, 105, 91, 118, 217, 182, 167, 140, 6, 67, 112, 97, 183, 66, 112, 245, 103, 136, 222, 205, 28, 196, 45, 6, 223, 192, 76, 56, 180, 90, 120, 144, 19, 31, 193, 37, 129, 186, 214, 36, 53, 204, 53, 108, 133, 112, 17, 133, 244, 3, 12, 230, 29, 243, 51, 79, 253, 10, 111, 185, 23, 74, 230, 99, 94, 78, 49, 209, 39, 95, 213, 248, 212, 22, 4, 222, 145, 77, 190, 136, 230, 134, 70, 228, 241, 194, 216, 163, 234, 52, 1, 64, 181, 139, 128, 90, 255, 214, 60, 168, 233, 254, 110, 31, 102, 58, 67, 201, 33 },
                P = new byte[] { 237, 238, 79, 75, 29, 57, 145, 201, 57, 177, 215, 108, 40, 77, 232, 237, 113, 38, 157, 195, 174, 134, 188, 175, 121, 28, 11, 236, 80, 146, 12, 38, 8, 12, 104, 46, 6, 247, 14, 149, 196, 23, 130, 116, 141, 137, 225, 74, 84, 111, 44, 163, 55, 10, 246, 154, 195, 158, 186, 241, 162, 11, 217, 77 },
                Q = new byte[] { 226, 89, 29, 67, 178, 205, 30, 152, 184, 165, 15, 152, 131, 245, 141, 80, 150, 3, 224, 136, 188, 248, 149, 36, 200, 250, 207, 156, 224, 79, 150, 191, 84, 214, 233, 173, 95, 192, 55, 123, 124, 255, 53, 85, 11, 233, 156, 66, 14, 27, 27, 163, 108, 199, 90, 37, 118, 38, 78, 171, 80, 26, 101, 37 },
                DP = new byte[] { 108, 176, 122, 132, 131, 187, 50, 191, 203, 157, 84, 29, 82, 100, 20, 205, 178, 236, 195, 17, 10, 254, 253, 222, 226, 226, 79, 8, 10, 222, 76, 178, 106, 230, 208, 8, 134, 162, 1, 133, 164, 232, 96, 109, 193, 226, 132, 138, 33, 252, 15, 86, 23, 228, 232, 54, 86, 186, 130, 7, 179, 208, 217, 217 },
                DQ = new byte[] { 175, 63, 252, 46, 140, 99, 208, 138, 194, 123, 218, 101, 101, 214, 91, 65, 199, 196, 220, 182, 66, 73, 221, 128, 11, 180, 85, 198, 202, 206, 20, 147, 179, 102, 106, 170, 247, 245, 229, 127, 81, 58, 111, 218, 151, 76, 154, 213, 114, 2, 127, 21, 187, 133, 102, 64, 151, 7, 245, 229, 34, 50, 45, 153 },
                InverseQ = new byte[] { 137, 156, 11, 248, 118, 201, 135, 145, 134, 121, 14, 162, 149, 14, 98, 84, 108, 160, 27, 91, 230, 116, 216, 181, 200, 49, 34, 254, 119, 153, 179, 52, 231, 234, 36, 148, 71, 161, 182, 171, 35, 182, 46, 164, 179, 100, 226, 71, 119, 23, 0, 16, 240, 4, 30, 57, 76, 109, 89, 131, 56, 219, 71, 206 },
                D = new byte[] { 108, 15, 123, 176, 150, 208, 197, 72, 23, 53, 159, 63, 53, 85, 238, 197, 153, 187, 156, 187, 192, 226, 186, 170, 26, 168, 245, 196, 65, 223, 248, 81, 170, 79, 91, 191, 83, 15, 31, 77, 39, 119, 249, 143, 245, 183, 49, 105, 115, 15, 122, 242, 87, 221, 94, 230, 196, 146, 59, 7, 103, 94, 9, 223, 146, 180, 189, 86, 190, 94, 242, 59, 32, 54, 23, 181, 124, 170, 63, 172, 90, 158, 169, 140, 6, 102, 170, 0, 135, 199, 35, 196, 212, 238, 196, 56, 14, 0, 140, 197, 169, 240, 156, 43, 182, 123, 102, 79, 89, 20, 120, 171, 43, 223, 58, 190, 230, 166, 185, 162, 186, 226, 31, 206, 196, 188, 104, 1 },
            };

        /// <summary>
        /// Standard, in-memory provider application store that is used a crypto key- and nonce store.
        /// </summary>
		private readonly ICryptoKeyStore cryptoKeyStore = DependencyInjection.Get<ICryptoKeyStore>();
		private readonly INonceStore nonceStore = DependencyInjection.Get<INonceStore>();

        public AuthorizationServerHost()
        {
            // Use a default in-memory provider application store. This is a class that is ideal for use in test
            // applications as it requires no further setup and can be used as both the crypto key- and nonce store.
            // In real-life situations you would of course implement your own crypto key- and nonce store, which will
            // most likely use some kind of persistent storage to store keys and nonces. As the nonces are kept in memory
            // only, it is not possible to refresh tokens as the issued tokens will have been removed from memory the moment
            // the refresh token request is being processed
        }

        /// <summary>
        /// Gets the store for storing crypto keys used to symmetrically encrypt and sign authorization codes and refresh tokens.
        /// </summary>
        /// <remarks>
        /// This store should be kept strictly confidential in the authorization server(s)
        /// and NOT shared with the resource server.  Anyone with these secrets can mint
        /// tokens to essentially grant themselves access to anything they want.
        /// </remarks>
        public ICryptoKeyStore CryptoKeyStore
        {
            get
            {
				return this.cryptoKeyStore;
            }
        }

        /// <summary>
        /// Gets the authorization code nonce store to use to ensure that authorization codes can only be used once.
        /// </summary>
        /// <value>
        /// The authorization code nonce store.
        /// </value>
        public INonceStore NonceStore
        {
            get
            {
				return this.nonceStore;
            }
        }

        /// <summary>
        /// Acquires the access token and related parameters that go into the formulation of the token endpoint's response to a client.
        /// </summary>
        /// <param name="accessTokenRequestMessage">Details regarding the resources that the access token will grant access to, and the identity of the client that will receive that access.
        /// Based on this information the receiving resource server can be determined and the lifetime of the access token can be set based on the sensitivity of the resources.
		/// </param>
        /// <returns>A non-null parameters instance that DotNetOpenAuth will dispose after it has been used.</returns>
        public AccessTokenResult CreateAccessToken(IAccessTokenRequest accessTokenRequestMessage)
        {
			AuthorizationServerAccessToken accessToken = new AuthorizationServerAccessToken();
			accessToken.Lifetime = TimeSpan.FromMinutes(5);
			accessToken.ExtraData.Add("userIdentity", "{ user: \"some identity\" }");

			//accessToken.Lifetime = TimeSpan.FromSeconds(20);
            accessToken.ResourceServerEncryptionKey = CreateRsaCryptoServiceProvider(ResourceServerEncryptionPublicKey);
			accessToken.AccessTokenSigningKey = CreateRsaCryptoServiceProvider(AuthorizationServerSigningPrivateKey);

            return new AccessTokenResult(accessToken);
        }

        /// <summary>
        /// Gets the client with a given identifier.
        /// </summary>
        /// <param name="clientIdentifier">The client identifier.</param>
        /// <returns>The client registration. Never null.</returns>
        public IClientDescription GetClient(string clientIdentifier)
        {
			IOAuth2AuthorizationStoreAgent agent = DependencyInjection.Get<IOAuth2AuthorizationStoreAgent>();
			OAuth2ClientApplication clientApplication = agent.GetClientApplication(clientIdentifier);

			// Does the CLIENT IDENTIFIER exist or is expired ? Oh yes ... then an exception is thrown.
			if (clientApplication == null) { throw new ArgumentException("No client could be found with the specified client identifier.", "clientIdentifier"); }

			return new ClientDescription(clientApplication.Secret, null, ClientType.Public);    
        }

        /// <summary>
        /// Determines whether a described authorization is (still) valid.
        /// </summary>
        /// <param name="authorization">The authorization.</param>
        /// <returns>
        ///   <c>true</c> if the original authorization is still valid; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///   <para>When establishing that an authorization is still valid,
        /// it's very important to only match on recorded authorizations that
        /// meet these criteria:</para>
        /// 1) The client identifier matches.
        /// 2) The user account matches.
        /// 3) The scope on the recorded authorization must include all scopes in the given authorization.
        /// 4) The date the recorded authorization was issued must be <em>no later</em> that the date the given authorization was issued.
        ///   <para>One possible scenario is where the user authorized a client, later revoked authorization,
        /// and even later reinstated authorization.  This subsequent recorded authorization
        /// would not satisfy requirement #4 in the above list.  This is important because the revocation
        /// the user went through should invalidate all previously issued tokens as a matter of
        /// security in the event the user was revoking access in order to sever authorization on a stolen
        /// account or piece of hardware in which the tokens were stored. </para>
        /// </remarks>
        public bool IsAuthorizationValid(IAuthorizationDescription authorization)
        {
            // We check once again if the user is authorized for the specified scopes
            return UserIsAuthorizedForRequestedScopes(authorization.User, authorization.Scope);
        }

        /// <summary>
        /// Determines whether a given set of resource owner credentials is valid based on the authorization server's user database
        /// and if so records an authorization entry such that subsequent calls to <see cref="M:DotNetOpenAuth.OAuth2.IAuthorizationServerHost.IsAuthorizationValid(DotNetOpenAuth.OAuth2.ChannelElements.IAuthorizationDescription)" /> would
        /// return <c>true</c>.
        /// </summary>
        /// <param name="userName">Username on the account.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="accessRequest">The access request the credentials came with.
        /// This may be useful if the authorization server wishes to apply some policy based on the client that is making the request.</param>
        /// <returns>
        /// A value that describes the result of the authorization check.
        /// </returns>
		public AutomatedUserAuthorizationCheckResponse CheckAuthorizeResourceOwnerCredentialGrant(string userName, string password, IAccessTokenRequest accessRequest)
		{
			IOAuth2AuthorizationStoreAgent agent = DependencyInjection.Get<IOAuth2AuthorizationStoreAgent>();
			var user = agent.GetUser(userName, password);

			// We use a fixed username and password to determine if the username/password combination is correct.
			var userCredentialsAreCorrect = user != null;

			// Add the user's scopes.
			IList<OAuth2Scope> scopes = agent.GetUserScopes(userName);
			scopes.Select(x => x.Code).ToList().ForEach(x =>
				{
					accessRequest.Scope.Add(x);
				});

			OAuth2UserIdentity userIdentity = agent.GetUserIdentity(userName);
			var _userIdentity = userIdentity.ToDataTransferValue();
			accessRequest.ExtraData.Add(new KeyValuePair<string, string>("userIdentity", JsonConvert.SerializeObject(_userIdentity)));


			// The token request is approved when the user credentials are correct and the user is authorized for the requested scopes
			var isApproved = userCredentialsAreCorrect && UserIsAuthorizedForRequestedScopes(userName, accessRequest.Scope);

			return new AutomatedUserAuthorizationCheckResponse(accessRequest, isApproved, userName);
		}

        /// <summary>
        /// Determines whether an access token request given a client credential grant should be authorized
        /// and if so records an authorization entry such that subsequent calls to <see cref="M:DotNetOpenAuth.OAuth2.IAuthorizationServerHost.IsAuthorizationValid(DotNetOpenAuth.OAuth2.ChannelElements.IAuthorizationDescription)" /> would
        /// return <c>true</c>.
        /// </summary>
        /// <param name="accessRequest">The access request the credentials came with.
        /// This may be useful if the authorization server wishes to apply some policy based on the client that is making the request.</param>
        /// <returns>
        /// A value that describes the result of the authorization check.
        /// </returns>
        public AutomatedAuthorizationCheckResponse CheckAuthorizeClientCredentialsGrant(IAccessTokenRequest accessRequest)
        {
            // We define the scopes the client is authorized for. Once again, you would expect these scopes to be retrieved from
            // a persistent store. Note: the scopes a client is authorized for can very well differ between clients
			HashSet<string> scopesClientIsAuthorizedFor = new HashSet<string>(OAuthUtilities.ScopeStringComparer);
            scopesClientIsAuthorizedFor.Add("demo-client-scope");

            // Check if the scopes that are being requested are a subset of the scopes the user is authorized for.
            // If not, that means that the user has requested at least one scope it is not authorized for
			bool clientIsAuthorizedForRequestedScopes = accessRequest.Scope.IsSubsetOf(scopesClientIsAuthorizedFor);

            // The token request is approved when the client is authorized for the requested scopes
			bool isApproved = clientIsAuthorizedForRequestedScopes;

            return new AutomatedAuthorizationCheckResponse(accessRequest, isApproved);
        }

        /// <summary>
        /// Check if the user is authorized.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="requestedScopes">The scopes the user has requested.</param>
        /// <returns><c>true</c>, if the user is authorized for the specified scopes; otherwise, <c>false</c>.</returns>
        private static bool UserIsAuthorizedForRequestedScopes(string userName, HashSet<string> requestedScopes)
        {
            // We define the scopes the user is authorized for. Once again, you would expect these scopes to be retrieved from
            // a persistent store. Note: the scopes a user is authorized for can very well differ between users. Think of an
            // admin user being authorized for more scopes than a regular user

			// Get the scopes the user is authorized for from the database.
			IOAuth2AuthorizationStoreAgent agent = DependencyInjection.Get<IOAuth2AuthorizationStoreAgent>();
	        IList<OAuth2Scope> scopes = agent.GetUserScopes(userName);

			HashSet<string> scopesUserIsAuthorizedFor = new HashSet<string>(OAuthUtilities.ScopeStringComparer);
			scopes.Select(x => x.Code).ToList().ForEach(x =>
			{
				scopesUserIsAuthorizedFor.Add(x);
			});

            // Check if the scopes that are being requested are a subset of the scopes the user is authorized for.
            // If not, that means that the user has requested at least one scope it is not authorized for
			bool userIsAuthorizedForRequestedScopes = requestedScopes.IsSubsetOf(scopesUserIsAuthorizedFor);
            
            return userIsAuthorizedForRequestedScopes;
        }

        /// <summary>
        /// Creates the RSA crypto service provider.
        /// </summary>
        /// <param name="parameters">The RSA parameters</param>
        /// <returns>The RSA crypto service provider.</returns>
        internal static RSACryptoServiceProvider CreateRsaCryptoServiceProvider(RSAParameters parameters)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(parameters);
            return rsa;
        }
    }
}