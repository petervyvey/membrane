using System.Web.Mvc;

namespace Membrane.Web.Public.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			return RedirectToAction("Index", "Database", new { area = "Administration" });
        }
    }
}
