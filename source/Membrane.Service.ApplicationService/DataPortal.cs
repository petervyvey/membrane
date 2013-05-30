using System.Threading;
using Membrane.DataTransfer;
using Membrane.DataTransfer.Projection;
using Membrane.Domain.Agent;
using Membrane.Foundation.Pattern.Creational;
using Membrane.Foundation.Pattern.Enterprise;

namespace Membrane.Service.ApplicationService
{
	/// <summary>
	/// Implementation of <see cref="IDataPortal"/> API.
	/// </summary>
	public class DataPortal 
		: IDataPortal
	{
		/// <summary>
		/// Get the user's profile.
		/// </summary>
		/// <returns></returns>
		public DataTransfer.OAuth2UserIdentity GetUserIdentity()
		{
			DataTransfer.OAuth2UserIdentity value =
				UnitOfWork.Execute<DataTransfer.OAuth2UserIdentity>(() =>
				{
					IOAuth2AuthorizationStoreAgent agent = DependencyInjection.Get<IOAuth2AuthorizationStoreAgent>();
					Domain.Entity.OAuth2UserIdentity entity = agent.GetUserIdentity(Thread.CurrentPrincipal.Identity.Name);
					var _value = entity.ToDataTransferValue();

					return _value;
				});

			return value;
		}

		/// <summary>
		/// Sign up a new <see cref="OAuth2User"/> account.
		/// </summary>
		/// <param name="user">The <see cref="OAuth2User"/> data transfer object.</param>
		/// <returns>The registered <see cref="OAuth2User"/> account.</returns>
		public DataTransfer.OAuth2UserIdentity SignUpUser(OAuth2UserIdentity userIdentity)
		{
			DataTransfer.OAuth2UserIdentity value =
				UnitOfWork.Execute<DataTransfer.OAuth2UserIdentity>(() =>
				{
					IOAuth2AuthorizationStoreAgent agent = DependencyInjection.Get<IOAuth2AuthorizationStoreAgent>();
					Domain.Entity.OAuth2UserIdentity _userIdentity = userIdentity.ToEntity();
					Domain.Entity.OAuth2UserIdentity entity = agent.SignUpUser(_userIdentity);

					DataTransfer.OAuth2UserIdentity _value = entity.ToDataTransferValue();

					return _value;
				});

			return value;
		}
	}
}
