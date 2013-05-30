using System.Web.Mvc;

namespace Membrane.Web.Public.Areas.OAuth
{
	public class OAuthAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "OAuth";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"OAuth2_default",
				"OAuth2/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
