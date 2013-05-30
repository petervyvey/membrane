
using Membrane.DataTransfer;
namespace Membrane.Service.ApplicationService
{
	/// <summary>
	/// The API for the data portal.
	/// </summary>
	public interface IDataPortal
	{
		/// <summary>
		/// Get the user's <see cref="OAuth2UserIdentity"/> account.
		/// </summary>
		/// <returns></returns>
		OAuth2UserIdentity GetUserIdentity();

		/// <summary>
		/// Sign up a new <see cref="OAuth2UserIdentity"/> account.
		/// </summary>
		/// <param name="user">The <see cref="OAuth2UserIdentity"/> data transfer object.</param>
		/// <returns>The registered <see cref="OAuth2UserIdentity"/> account.</returns>
		OAuth2UserIdentity SignUpUser(OAuth2UserIdentity userIdentity);
	}
}
