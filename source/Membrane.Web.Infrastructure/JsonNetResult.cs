
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Membrane.Foundation.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Membrane.Web.Infrastructure
{
    /// <summary>
    /// ASP MVC serializer using JSON.Net.
    /// </summary>
    public class JsonNetResult 
        : JsonResult
    {
        #region - Constructors -

        /// <summary>
        /// Default constructor.
        /// </summary>
        public JsonNetResult()
            : base()
        {
            this.InitializeObject();
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        public JsonNetResult(object data, string contentType, Encoding contentEncoding)
            : this()
        {
            this.Data = data;
            this.ContentType = contentType;
            this.ContentEncoding = contentEncoding;
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <param name="behavior"></param>
        public JsonNetResult(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
            : this(data, contentType, contentEncoding)
        {
            this.JsonRequestBehavior = behavior;
        }

        #endregion

        #region - Constants & static fields -

        /// <summary>
        /// The default JSON.Net serializer settings.
        /// </summary>
        public static readonly JsonSerializerSettings DEFAULT_JSON_SERIALIZER_SETTINGS =
            new JsonSerializerSettings()
            {
                ConstructorHandling = ConstructorHandling.Default,
                DefaultValueHandling = DefaultValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                TypeNameHandling = TypeNameHandling.None,
                Converters =
                    new List<JsonConverter>()
            	    {
            		    new IsoDateTimeConverter()
            	    }
            };

        #endregion

        #region - Properties -

        /// <summary>
        /// The JSON serializer settings.
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; private set; }

        /// <summary>
        /// The JSON formattiong setting.
        /// </summary>
        public Formatting Formatting { get; private set; }

        #endregion
        
        /// <summary>
        /// Coverts the data to JSON.
        /// </summary>
        /// <param name="context">The MVC controller context.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            context.CatchNullArgument("context");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = this.Formatting };

                JsonSerializer serializer = JsonSerializer.Create(this.SerializerSettings);
                serializer.Serialize(writer, this.Data);

                writer.Flush();
            }
        }

        #region - Private & protected methods -

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void InitializeObject()
        {
            this.Formatting = Formatting.Indented;
            //this.Formatting = Formatting.None;

            this.SerializerSettings = JsonNetResult.DEFAULT_JSON_SERIALIZER_SETTINGS;
        }

        #endregion
    }
}