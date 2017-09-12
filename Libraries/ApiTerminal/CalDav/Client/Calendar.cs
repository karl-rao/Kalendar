using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Kalendar.Zero.ApiTerminal.CalDav;

namespace Kalendar.Zero.ApiTerminal.CalDav.Client {
	public class Calendar {
		public Uri Url { get; set; }
		public NetworkCredential Credentials { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public void Initialize() {
			var result = global::Kalendar.Zero.ApiTerminal.CalDav.Client.Common.Request(Url, "PROPFIND", global::Kalendar.Zero.ApiTerminal.CalDav.Common.xDav.Element("propfind",
				global::Kalendar.Zero.ApiTerminal.CalDav.Common.xDav.Element("allprop")), Credentials, new Dictionary<string, object> {
					{ "Depth", 0 }
				});
			var xdoc = XDocument.Parse(result.Item2);
			var desc = xdoc.Descendants(global::Kalendar.Zero.ApiTerminal.CalDav.Common.xCalDav.GetName("calendar-description")).FirstOrDefault();
			var name = xdoc.Descendants(global::Kalendar.Zero.ApiTerminal.CalDav.Common.xDav.GetName("displayname")).FirstOrDefault();
			if (name != null) Name = name.Value;
			if (desc != null) Description = desc.Value;

			var resourceTypes = xdoc.Descendants(global::Kalendar.Zero.ApiTerminal.CalDav.Common.xDav.GetName("resourcetype"));
			if (!resourceTypes.Any(
				r => r.Elements(global::Kalendar.Zero.ApiTerminal.CalDav.Common.xDav.GetName("collection")).Any()
					&& r.Elements(global::Kalendar.Zero.ApiTerminal.CalDav.Common.xCalDav.GetName("calendar")).Any()
				))
				throw new Exception("This server does not appear to support CALDAV");
		}

		public Kalendar.Zero.ApiTerminal.CalDav.CalendarCollection Search(global::Kalendar.Zero.ApiTerminal.CalDav.CalendarQuery query) {
			var result = global::Kalendar.Zero.ApiTerminal.CalDav.Client.Common.Request(Url, "REPORT", (XElement)query, Credentials, new Dictionary<string, object> {
				{ "Depth", 1 }
			});
			var xdoc = XDocument.Parse(result.Item2);
			var data = xdoc.Descendants(global::Kalendar.Zero.ApiTerminal.CalDav.Common.xCalDav.GetName("calendar-data"));
			var serializer = new Kalendar.Zero.ApiTerminal.CalDav.Serializer();
			return new Kalendar.Zero.ApiTerminal.CalDav.CalendarCollection(data.SelectMany(x => {
				using (var rdr = new System.IO.StringReader(x.Value)) {
					return serializer.Deserialize<Kalendar.Zero.ApiTerminal.CalDav.CalendarCollection>(rdr);
				}
			}));
		}

		public void Save(Event e) {
			if (string.IsNullOrEmpty(e.UID)) e.UID = Guid.NewGuid().ToString();
			e.LastModified = DateTime.UtcNow;

			var result = global::Kalendar.Zero.ApiTerminal.CalDav.Client.Common.Request(new Uri(Url, e.UID + ".ics"), "PUT", (req, str) => {
				req.Headers[System.Net.HttpRequestHeader.IfNoneMatch] = "*";
				req.ContentType = "text/calendar";
				var calendar = new global::Kalendar.Zero.ApiTerminal.CalDav.Calendar();
				e.Sequence = (e.Sequence ?? 0) + 1;
				calendar.Events.Add(e);
				global::Kalendar.Zero.ApiTerminal.CalDav.Client.Common.Serialize(str, calendar);

			}, Credentials);
			if (result.Item1 != System.Net.HttpStatusCode.Created && result.Item1 != HttpStatusCode.NoContent)
				throw new Exception("Unable to save event: " + result.Item1);
			e.Url = new Uri(Url, result.Item3[System.Net.HttpResponseHeader.Location]);

			GetObject(e.UID);
		}

		public CalendarCollection GetAll() {
			var result = global::Kalendar.Zero.ApiTerminal.CalDav.Client.Common.Request(Url, "PROPFIND", global::Kalendar.Zero.ApiTerminal.CalDav.Common.xCalDav.Element("calendar-multiget",
			global::Kalendar.Zero.ApiTerminal.CalDav.Common.xDav.Element("prop",
				global::Kalendar.Zero.ApiTerminal.CalDav.Common.xDav.Element("getetag"),
				global::Kalendar.Zero.ApiTerminal.CalDav.Common.xCalDav.Element("calendar-data")
				)
			), Credentials, new Dictionary<string, object> { { "Depth", 1 } });




			return null;
		}

		public CalendarCollection GetObject(string uid) {
			var result = global::Kalendar.Zero.ApiTerminal.CalDav.Client.Common.Request(Url, "REPORT", global::Kalendar.Zero.ApiTerminal.CalDav.Common.xCalDav.Element("calendar-multiget",
				global::Kalendar.Zero.ApiTerminal.CalDav.Common.xDav.Element("prop",
					global::Kalendar.Zero.ApiTerminal.CalDav.Common.xDav.Element("getetag"),
					global::Kalendar.Zero.ApiTerminal.CalDav.Common.xCalDav.Element("calendar-data")
					),
				global::Kalendar.Zero.ApiTerminal.CalDav.Common.xDav.Element("href", new Uri(Url, uid + ".ics"))
				), Credentials, new Dictionary<string, object> { { "Depth", 1 } });


			return null;

		}
	}
}
