using System.Web.Mvc;
using System.Web.Routing;

namespace Membrane.Web.Public.Areas.Api
{
	public class ApiAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get { return "Api"; }
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"API_01.00_DATA",
				"api/1.0/data/{action}/{identifier}",
				new { controller = "DataVersion0100", identifier = UrlParameter.Optional });

			context.MapRoute(
				"API_02.00_DATA",
				"api/2.0/data/{action}/{identifier}",
				new { controller = "DataVersion0200", identifier = UrlParameter.Optional });

			context.MapRoute(
				"API_01.00_Resource_GET",
				"api/1.0/resource/{resource}/{identifier}",
				new { controller = "RestVersion0100", action = "Get" },
				new { httpMethod = new HttpMethodConstraint("GET") });

			context.MapRoute(
				"API_01.00_Resource_GET_SET",
				"api/1.0/resource/{resource}",
				new { controller = "RestVersion0100", action = "GetSet" },
				new { httpMethod = new HttpMethodConstraint("GET") });

			context.MapRoute(
				"API_01.00_Resource_POST",
				"api/1.0/resource/{resource}",
				new { controller = "RestVersion0100", action = "Post" },
				new { httpMethod = new HttpMethodConstraint("POST") });

			context.MapRoute(
				"API_01.00_Resource_PUT",
				"api/1.0/resource/{resource}",
				new { controller = "RestVersion0100", action = "Put" },
				new { httpMethod = new HttpMethodConstraint("PUT") });

			context.MapRoute(
				"API_01.00_Resource_DELETE",
				"api/1.0/resource/{resource}/{identifier}",
				new { controller = "RestVersion0100", action = "Delete" },
				new { httpMethod = new HttpMethodConstraint("DELETE") });
		}
	}
}
