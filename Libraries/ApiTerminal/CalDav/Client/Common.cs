using System;
using System.Net;
using System.Xml.Linq;
using log4net.Repository.Hierarchy;

namespace Kalendar.Zero.ApiTerminal.CalDav.Client {
	internal static class Common {

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static Tuple<System.Net.HttpStatusCode, string, System.Net.WebHeaderCollection> Request(Uri url, string method, XDocument content, NetworkCredential credentials = null, System.Collections.Generic.Dictionary<string, object> headers = null) {
			return Request(url, method, content.Root, credentials, headers);
		}
		public static Tuple<System.Net.HttpStatusCode, string, System.Net.WebHeaderCollection> Request(Uri url, string method, XElement content, NetworkCredential credentials = null, System.Collections.Generic.Dictionary<string, object> headers = null) {
			return Request(url, method, (req, str) => {
				req.ContentType = "text/xml";
				var xml = content.ToString();
				using (var wrtr = new System.IO.StreamWriter(str))
					wrtr.Write(xml);
			}, credentials, headers);
		}

		public static Tuple<System.Net.HttpStatusCode, string, System.Net.WebHeaderCollection> Request(Uri url, string method, string contentType, string content, NetworkCredential credentials = null, System.Collections.Generic.Dictionary<string, object> headers = null) {
			return Request(url, method, (req, str) => {
				req.ContentType = contentType;
				using (var wrtr = new System.IO.StreamWriter(str))
					wrtr.Write(content);
			}, credentials, headers);
		}

		public static Tuple<System.Net.HttpStatusCode, string, System.Net.WebHeaderCollection> Request(
            Uri url, 
            string method = "GET", 
            Action<System.Net.HttpWebRequest, System.IO.Stream> content = null, 
            NetworkCredential credentials = null, 
            System.Collections.Generic.Dictionary<string, object> headers = null) {

            Logger.Info(method+" : "+url);

			var req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
			req.Method = method.ToUpper();

			//Dear .NET, please don't try to do things for me.  kthxbai
			System.Net.ServicePointManager.Expect100Continue = false;

			if (headers != null)
				foreach (var header in headers) {
					var value = Convert.ToString(header.Value);
					if (header.Key.Is("User-Agent"))
						req.UserAgent = value;
					else req.Headers[header.Key] = value;
				}

			if (credentials != null) {
				//req.Credentials = credentials;
				var b64 = credentials.UserName + ":" + credentials.Password;
				b64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(b64));
				req.Headers[HttpRequestHeader.Authorization] = "Basic " + b64;
			}

			using (var stream = req.GetRequestStream()) {
				if (content != null) {
					content(req, stream);
				}

				using (var res = GetResponse(req))
				using (var str = res.GetResponseStream())
				using (var rdr = new System.IO.StreamReader(str))
				{
				    var resp = rdr.ReadToEnd();

					return Tuple.Create(res.StatusCode,resp , res.Headers);
				}
			}
		}

		private static System.Net.HttpWebResponse GetResponse(System.Net.HttpWebRequest req) {
			try {
				return (System.Net.HttpWebResponse)req.GetResponse();
			} catch (System.Net.WebException wex) {
				return (System.Net.HttpWebResponse)wex.Response;
			}
		}

		public static void Serialize(System.IO.Stream stream, Event e, System.Text.Encoding encoding = null) {
			var ical = new global::Kalendar.Zero.ApiTerminal.CalDav.Calendar();
			ical.Events.Add(e);
			Serialize(stream, ical, encoding);
		}

		public static void Serialize(System.IO.Stream stream, global::Kalendar.Zero.ApiTerminal.CalDav.Calendar ical, System.Text.Encoding encoding = null) {
			var serializer = new global::Kalendar.Zero.ApiTerminal.CalDav.Serializer();
			serializer.Serialize(stream, ical, encoding);
		}
	}
}
