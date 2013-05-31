
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Membrane.Web.Utils
{
    /// <summary>
    /// Agent om HTTP request uit te voeren.
    /// </summary>
    public class HttpRequestAgent
    {
        /// <summary>
        /// Consumes the service at the given URL by invoking a HTTP GET.
        /// </summary>
        /// <param name="url">The service endpoint URL.</param>
        /// <returns>The service method response.</returns>
        public string Getc(string url)
        {
            // Used to build entire response
            StringBuilder builder = new StringBuilder();

            // Used on each read operation
            byte[] buffer = new byte[8192];

            // Prepare the web service we will consume.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Execute the request.
			string result = HttpRequestAgent.ExecuteRequest(request);

            return result;
        }

	    /// <summary>
		/// Consumes the service at the given URL by invoking a HTTP POST.
		/// </summary>
		/// <param name="url">The service endpoint URL.</param>
		/// <param name="json">The JSON string.</param>
		/// <returns>The service method response.</returns>
        public string Post(string url, string json)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.AllowWriteStreamBuffering = true;
            request.KeepAlive = false;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "application/json";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            //string parameters = string.Format("{0}={1}", Uri.EscapeDataString("value"), Uri.EscapeDataString(json));
		    string parameters = json;

            Stream requestStream = request.GetRequestStream();
            StreamWriter writer = new StreamWriter(requestStream);
            writer.Write(parameters);
            writer.Close();

			// Execute the request.
			string result = HttpRequestAgent.ExecuteRequest(request);

            return result;
        }

		/// <summary>
		/// Executes the <paramref name="request"/>.
		/// </summary>
		/// <param name="request">The <see cref="HttpWebRequest"/>.</param>
		/// <returns>The HTTP response string.</returns>
		private static string ExecuteRequest(HttpWebRequest request)
		{
			string result = string.Empty;

			// Used to build entire response
			StringBuilder builder = new StringBuilder();

			// Used on each read operation
			byte[] buffer = new byte[8192];

			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				// Read data via the response stream.
				using (Stream stream = response.GetResponseStream())
				{
					string tempString = null;
					int count = 0;

					do
					{
						// Fill the buffer with data.
						count = stream.Read(buffer, 0, buffer.Length);

						// Make sure we read some data.
						if (count != 0)
						{
							// Encode from bytes to ASCII text.
							tempString = Encoding.ASCII.GetString(buffer, 0, count);

							// Append to the the string
							builder.Append(tempString);
						}
					} while (count > 0);

					// print out page source
					result = builder.ToString();

					stream.Close();
				}

				response.Close();
			}
			return result;
		}

    }
}
