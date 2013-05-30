
using System;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Membrane.Web.Infrastructure
{
    /// <summary>
    /// Filters/parses the API action parameters.
    /// </summary>
    public class DataServiceActionFilter
        : ActionFilterAttribute
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataServiceActionFilter()
            : base() { }

        #endregion

        /// <summary>
        /// Intercepts the action method executed by the APS MVC framework.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                HttpRequestBase request = filterContext.HttpContext.Request;

                if (request.HttpMethod == "POST" || request.HttpMethod == "PUT")
                {
					string json = null;

                    // Read the JSON data from the request stream.
                    request.InputStream.Position = 0;
					using (StreamReader reader = new StreamReader(request.InputStream))
					{
						json = reader.ReadToEnd();
					}

                    // Deserialize the JSON object and set it as the DATA method parameter.
                    dynamic instance = JsonConvert.DeserializeObject<ExpandoObject>(json);
                    filterContext.ActionParameters["data"] = instance;
                }

                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                throw new HttpException((int)HttpStatusCode.UnsupportedMediaType, ex.Message);
            }
        }
    }
}