
using System;
using System.Collections.Generic;
using DotNetOpenAuth.Messaging.Bindings;
using Membrane.Domain.Agent;
using Membrane.Foundation.Pattern.Creational;

namespace Membrane.Web.OAuth
{
	public class AuthorizationStore
		: ICryptoKeyStore, INonceStore
	{
		private readonly IOAuth2AuthorizationStoreAgent authorizationStoreAgent = DependencyInjection.Get<IOAuth2AuthorizationStoreAgent>();

		public CryptoKey GetKey(string bucket, string handle)
		{
			return this.authorizationStoreAgent.GetKey(bucket, handle);
		}

		public IEnumerable<KeyValuePair<string, CryptoKey>> GetKeys(string bucket)
		{
			return this.authorizationStoreAgent.GetKeys(bucket);
		}

		public void RemoveKey(string bucket, string handle)
		{
			this.authorizationStoreAgent.RemoveKey(bucket, handle);
		}

		public void StoreKey(string bucket, string handle, CryptoKey key)
		{
			this.authorizationStoreAgent.StoreKey(bucket, handle, key);
		}

		public bool StoreNonce(string context, string nonce, DateTime timestampUtc)
		{
			return this.authorizationStoreAgent.StoreNonce(context, nonce, timestampUtc);
		}
	}
}
