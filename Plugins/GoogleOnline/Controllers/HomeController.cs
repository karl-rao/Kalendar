using System;
using System.IO;
using System.Threading;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
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
            var scope =
                new[]
                {
                    "https://www.googleapis.com/auth/userinfo.profile",
                    "https://www.googleapis.com/auth/contacts.readonly",
                    "https://www.googleapis.com/auth/gmail.readonly",
                    "https://www.googleapis.com/auth/calendar"
                };
            var flow= 
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = AppId,
                    ClientSecret = AppSecret
                },
                Scopes = scope
            });

            UserCredential credential=
            new UserCredential(flow, "101041433066104582113", new TokenResponse
                {
                    AccessToken = "ya29.GlvDBNlRfrlZQ0PeKIYNZWSEbZoPS3EN-ngNe0H33QIMU5blkXP-36C3tQE8FmyCP4d7igH61gWiojMWIWH1OMoUny5ZZb16JvNNRiyNuZAJFTPaMP0LjPdK-l3U",
                    ExpiresInSeconds = 3600,
                    RefreshToken = "1/9nXnCiYlSI_13pgatubknJFz4FgIH_7WwJh_KciK0Hs",
                    Scope = "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/contacts.readonly https://www.googleapis.com/auth/gmail.readonly https://www.googleapis.com/auth/calendar"
                
            });



            //var scopes = new string[] { CalendarService.Scope.Calendar };
            //string credPath = Server.MapPath("/qs.json");
            //credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");
            //var clientSecrets = new ClientSecrets { ClientId = AppId, ClientSecret = AppSecret };

            //var credential =await GoogleWebAuthorizationBroker.AuthorizeAsync(
            //    clientSecrets,
            //    scopes,
            //    "user",
            //    CancellationToken.None,
            //    new FileDataStore(credPath, true)).Result;
            //Response.Write("Credential file saved to: " + credPath);

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