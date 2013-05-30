
using System;
using System.Collections.Generic;
using DotNetOpenAuth.Messaging.Bindings;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Agent
{
	public interface IOAuth2AuthorizationStoreAgent
	{
		DotNetOpenAuth.Messaging.Bindings.CryptoKey GetKey(string bucket, string handle);

		IEnumerable<KeyValuePair<string, DotNetOpenAuth.Messaging.Bindings.CryptoKey>> GetKeys(string bucket);

		void RemoveKey(string bucket, string handle);

		void StoreKey(string bucket, string handle, DotNetOpenAuth.Messaging.Bindings.CryptoKey key);

		bool StoreNonce(string context, string nonce, DateTime timestampUtc);

		OAuth2ClientApplication GetClientApplication(string identifier);

		IList<OAuth2Scope> GetScopes(OAuth2ScopeType scopeType);

		OAuth2UserIdentity SignUpUser(OAuth2UserIdentity userIdentity);

		OAuth2User GetUser(string userIdentifier, string password);

		IList<OAuth2Scope> GetUserScopes(string identifier);

		OAuth2UserIdentity GetUserIdentity(string identifier);

		OAuth2UserIdentity GetUserIdentity(IEntityContext context, string identifier);
	}
}
