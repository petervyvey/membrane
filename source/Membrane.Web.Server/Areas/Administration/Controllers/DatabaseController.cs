using System.Data;
using System.Data.Common;
using System.Web.Mvc;

namespace Membrane.Web.Public.Areas.Administration.Controllers
{
    public class DatabaseController : Controller
    {
        //
        // GET: /Administration/Database/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Administration/Database/Create
        public ActionResult Create()
        {
			string fileName = Server.MapPath("~/App_Data/membrane.data.sql");
			string script = System.IO.File.ReadAllText(fileName);

			DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SQLite");
			using (DbConnection connection = factory.CreateConnection())
			{ 
				connection.ConnectionString = "Data Source=|DataDirectory|\\membrane.data; Version=3";

				using (DbCommand command = connection.CreateCommand())
				{
					command.CommandType = CommandType.Text;
					command.CommandText = script;

					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}
			}

			return this.RedirectToAction("Index");
        }
    }
}
