using System;
using System.IO;
using System.Threading;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Kalendar.Plugins.GoogleOnline.Controllers
{
    public class HomeController : Controller
    {
        private const string AppId = "709789577070-ef1ebhmsbhe94fejfm5dcujdep6avbaq.apps.googleusercontent.com";
        private const string AppSecret = "ZUcxZYn8x2MUyZ6FHPUji-IO";

        public ActionResult Index()
        {
            UserCredential credential;

            var scopes = new string[] {CalendarService.Scope.Calendar};
            string credPath = Server.MapPath("/qs.json");
            //credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");
            var clientSecrets = new ClientSecrets { ClientId = AppId, ClientSecret = AppSecret };

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            Response.Write("Credential file saved to: " + credPath);


            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Atimer.cn",
            });

            //new CalendarService { }

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Response.Write("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Response.Write(string.Format("{0} ({1})", eventItem.Summary, when));
                }
            }
            else
            {
                Response.Write("No upcoming events found.");
            }


            return Content("");
        }

    }
}