
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Membrane.Domain.Entity;
using Membrane.Domain.Persistence;
using Membrane.Domain.Store;
using Membrane.Foundation.Pattern.Creational;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Membrane.Domain.Agent
{
	public class OAuth2AuthorizationStoreAgent
		: IOAuth2AuthorizationStoreAgent
	{
		public DotNetOpenAuth.Messaging.Bindings.CryptoKey GetKey(string bucket, string handle)
		{
			DotNetOpenAuth.Messaging.Bindings.CryptoKey key = null;

			using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
			{
				using (IOAuth2CryptoKeyRepository repository = DependencyInjection.Get<IOAuth2CryptoKeyRepository>(InjectionParameter.Create("context", context)))
				{
					IQueryOver<Entity.OAuth2CryptoKey, Entity.OAuth2CryptoKey> query =
						repository
							.Query()
							.Where(x => x.Bucket == bucket)
							.Where(x => x.Handle == handle);

					Entity.OAuth2CryptoKey _key = query.List().SingleOrDefault();
					if (_key != null)
					{
						key = new DotNetOpenAuth.Messaging.Bindings.CryptoKey(Encoding.Unicode.GetBytes(_key.Secret), new DateTime(_key.ExpiresUtc.Ticks, DateTimeKind.Utc));
					}
				}

				context.Commit();
			}

			return key;
		}

		public IEnumerable<KeyValuePair<string, DotNetOpenAuth.Messaging.Bindings.CryptoKey>> GetKeys(string bucket)
		{
			IEnumerable<KeyValuePair<string, DotNetOpenAuth.Messaging.Bindings.CryptoKey>> keySet;

			using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
			{
				using (IOAuth2CryptoKeyRepository repository = DependencyInjection.Get<IOAuth2CryptoKeyRepository>(InjectionParameter.Create("context", context)))
				{
					IQueryOver<Entity.OAuth2CryptoKey, Entity.OAuth2CryptoKey> query =
						repository
							.Query()
							.Where(x => x.Bucket == bucket);

					var set = query.List();
					keySet = set.Select(x => new KeyValuePair<string, DotNetOpenAuth.Messaging.Bindings.CryptoKey>(x.Handle, new DotNetOpenAuth.Messaging.Bindings.CryptoKey(Encoding.Unicode.GetBytes(x.Secret), new DateTime(x.ExpiresUtc.Ticks, DateTimeKind.Utc)))).ToList();
				}

				context.Commit();
			}

			return keySet;
		}

		public void RemoveKey(string bucket, string handle)
		{
			using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
			{
				using (IOAuth2CryptoKeyRepository repository = DependencyInjection.Get<IOAuth2CryptoKeyRepository>(InjectionParameter.Create("context", context)))
				{
					IQueryOver<Entity.OAuth2CryptoKey, Entity.OAuth2CryptoKey> query =
						repository
							.Query()
							.Where(x => x.Bucket == bucket);

					Entity.OAuth2CryptoKey _key = query.List().SingleOrDefault();

					repository.Delete(_key);
				}

				context.Commit();
			}
		}

		public void StoreKey(string bucket, string handle, DotNetOpenAuth.Messaging.Bindings.CryptoKey key)
		{
			using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
			{
				using (IOAuth2CryptoKeyRepository repository = DependencyInjection.Get<IOAuth2CryptoKeyRepository>(InjectionParameter.Create("context", context)))
				{
					Entity.OAuth2CryptoKey _key = new Entity.OAuth2CryptoKey();
					_key.ID = Guid.NewGuid();
					_key.Bucket = bucket;
					_key.Handle = handle;
					_key.Secret = Encoding.Unicode.GetString(key.Key);
					_key.ExpiresUtc = key.ExpiresUtc;

					repository.AddOrUpdate(_key);
				}

				context.Commit();
			}
		}

		public bool StoreNonce(string context, string nonce, DateTime timestampUtc)
		{
			throw new System.NotImplementedException();
		}

		public OAuth2ClientApplication GetClientApplication(string identifier)
		{
			OAuth2ClientApplication entity = null;

			using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
			{
				using (IOAuth2ClientApplicationRepository repository = DependencyInjection.Get<IOAuth2ClientApplicationRepository>(InjectionParameter.Create("context", context)))
				{
					IQueryOver<OAuth2ClientApplication, OAuth2ClientApplication> query =
						repository
							.Query()
							.Where(x => x.Identifier == identifier);

					entity = query.List().SingleOrDefault();
				}

				context.Commit();
			}

			return entity;
		}

		public OAuth2UserIdentity SignUpUser(OAuth2UserIdentity entity)
		{
			using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
			{
				using (IOAuth2UserRepository repository = DependencyInjection.Get<IOAuth2UserRepository>(InjectionParameter.Create("context", context)))
				{
					IList<OAuth2Scope> scopes =
						repository.Context.Query<OAuth2Scope>()
								  .Where(x => x.Code == "application.role.user.default")
						          .List();

					bool isValidIdentifier = this.IsValidEmail(entity.User.Identifier);
					if (isValidIdentifier)
					{
						OAuth2User user = new OAuth2User();
						user.ID = Guid.NewGuid();
						user.Identifier = entity.User.Identifier;
						user.Password = entity.User.Password;
						user.CreationTimestamp = DateTime.UtcNow;
						user.ValidFrom = DateTime.UtcNow;
						user.ValidUntil = new DateTime(DateTime.Parse("2100-01-01").Ticks, DateTimeKind.Utc);

						repository.Context.AddOrUpdate(user);

						Identity identity = new Identity();
						identity.ID = Guid.NewGuid();
						identity.FirstName = entity.Identity.FirstName;
						identity.Name = entity.Identity.Name;
						identity.Locale = entity.Identity.Locale;

						repository.Context.AddOrUpdate(identity);

						OAuth2UserScope userScope = new OAuth2UserScope();
						userScope.ID = Guid.NewGuid();
						userScope.Scope = scopes.Single();
						userScope.User = user;
						userScope.CreationTimestamp = DateTime.UtcNow;
						userScope.ValidFrom = DateTime.UtcNow;
						userScope.ValidUntil = new DateTime(DateTime.Parse("2100-01-01").Ticks, DateTimeKind.Utc);

						repository.Context.AddOrUpdate(userScope);

						OAuth2UserIdentity userIdentity = new OAuth2UserIdentity();
						userIdentity.ID = Guid.NewGuid();
						userIdentity.User = user;
						userIdentity.Identity = identity;
						userIdentity.CreationTimestamp = DateTime.UtcNow;
						userIdentity.ValidFrom = DateTime.UtcNow;
						userIdentity.ValidUntil = new DateTime(DateTime.Parse("2100-01-01").Ticks, DateTimeKind.Utc);

						repository.Context.AddOrUpdate(userIdentity);
					}
					else
					{
						throw new Exception("Invalid e-mail address.");
					}
				}

				context.Commit();
			}

			Domain.Entity.OAuth2UserIdentity _entity = this.GetUserIdentity(entity.User.Identifier);
			return _entity;
		}

		public OAuth2User GetUser(string identifier, string password)
		{
			OAuth2User entity = null;

			using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
			{
				using (IOAuth2UserRepository repository = DependencyInjection.Get<IOAuth2UserRepository>(InjectionParameter.Create("context", context)))
				{
					OAuth2Scope scope = null;
					QueryOver<OAuth2UserScope, OAuth2UserScope> subQuery =
						QueryOver.Of<OAuth2UserScope>()
							.Where(x => x.ValidFrom <= DateTime.UtcNow)
							.And(x => x.ValidUntil > DateTime.UtcNow)

							// Join with OAUTH2SCOPE, search for valid data.
							.JoinAlias(x => x.Scope, () => scope)
							.And(() => scope.ValidFrom <= DateTime.UtcNow)
							.And(() => scope.ValidUntil > DateTime.UtcNow)

							// Just keep the USER.ID to use in the query.
							.Select(t => t.User.ID);

					IQueryOver<OAuth2User, OAuth2User> query =
						repository.Query()
							.Fetch(x => x.UserScopes).Eager
							.Fetch(x => x.UserIdentities).Eager
							.Where(x => x.Identifier == identifier)
							.And(x => x.Password == password)
							.WithSubquery.WhereProperty(x => x.ID).In(subQuery)
							.TransformUsing(Transformers.DistinctRootEntity);

					entity = query.List().SingleOrDefault();
				}

				context.Commit();
			}

			return entity;
		}

		public IList<OAuth2Scope> GetUserScopes(string identifier)
		{
			IList<OAuth2Scope> set = null;

			using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
			{
				using (IOAuth2UserScopeRepository repository = DependencyInjection.Get<IOAuth2UserScopeRepository>(InjectionParameter.Create("context", context)))
				{
					var subQuery =
						QueryOver.Of<OAuth2User>()
							.Where(x => x.Identifier == identifier)
						// Just keep the USER.ID to use in the query.
							.Select(t => t.ID);

					var userScopeQuery =
						repository.Query()
							.WithSubquery.WhereProperty(x => x.User.ID).In(subQuery)
							.TransformUsing(Transformers.DistinctRootEntity)
							.Future<OAuth2UserScope>();

					set = userScopeQuery.Select(x=>x.Scope).ToList();
				}

				context.Commit();
			}

			return set;
		}

		public IList<OAuth2Scope> GetScopes(OAuth2ScopeType scopeType)
		{
			IList<OAuth2Scope> set = null;

			using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
			{
				using (IOAuth2ScopeRepository repository = DependencyInjection.Get<IOAuth2ScopeRepository>(InjectionParameter.Create("context", context)))
				{
					string scopeCodeBase = string.Empty;

					// Set the value of the scope code to be retrieved.
					switch (scopeType)
					{
						case OAuth2ScopeType.Client:
							scopeCodeBase = "application.client.role.";
							break;

						case OAuth2ScopeType.User:
							scopeCodeBase = "application.user.role.";
							break;

						default:
							break;
					}

					var query =
						repository.Query()
							.Where(x => x.Code.IsInsensitiveLike(scopeCodeBase, MatchMode.Start))
							.AndNot(x=>x.Code.IsInsensitiveLike("guest", MatchMode.End));

					set = query.List();
				}

				context.Commit();
			}

			return set;
		}

		public OAuth2UserIdentity GetUserIdentity(string identifier)
		{
			OAuth2UserIdentity entity = null;

			using (IEntityContext context = DependencyInjection.Get<IEntityContext>())
			{
				entity = this.GetUserIdentity(context, identifier);
				context.Commit();
			}

			return entity;
		}

		public OAuth2UserIdentity GetUserIdentity(IEntityContext context, string identifier)
		{
			OAuth2UserIdentity entity = null;

			using (IOAuth2UserIdentityRepository repository = DependencyInjection.Get<IOAuth2UserIdentityRepository>(InjectionParameter.Create("context", context)))
			{
				var subQuery =
					QueryOver.Of<OAuth2User>()
						.Where(x => x.Identifier == identifier)
					// Just keep the USER.ID to use in the query.
						.Select(t => t.ID);

				var userIdentityQuery =
					repository.Query()
						.Fetch(x => x.User.UserScopes).Eager
						.WithSubquery.WhereProperty(x => x.User.ID).In(subQuery)
						.TransformUsing(Transformers.DistinctRootEntity)
						.Future<OAuth2UserIdentity>();

				OAuth2UserIdentity userIdentity = userIdentityQuery.SingleOrDefault();
				if (userIdentity != null) entity = userIdentity;

			}

			return entity;
		}

		#region - Private & protected methods -

		private bool IsValidEmail(string emailAddress)
		{
			bool isValid = false;

			string matchEmailPattern =
				@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
					+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
					+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
					+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

			try
			{
				if (!string.IsNullOrEmpty(emailAddress))
				{
					isValid = Regex.IsMatch(emailAddress, matchEmailPattern);
				}
			}
			catch { /* IGNORE EXCEPTIONS */ }

			return isValid;
		}

		#endregion
	}
}
