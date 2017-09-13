using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Kalendar.Zero.ApiTerminal.Clients
{
    public class GoogleHelper
        : BaseHelper, IBaseHelper
    {
        private const string ApplicationName = "Atimer.cn";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Signin()
        {
            var url =
                string.Format(
                    "https://accounts.google.com/o/oauth2/auth?client_id={0}&redirect_uri={1}&state=atimer.CN&scope={2}&response_type=code&access_type=offline&approval_prompt=force",
                    Channel.AppId,
                    Channel.CodeCallback,
                    Channel.Parameters
                    );

            return url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public override Entities.Avatar ExchangeToken(string code)
        {
            var now = DateTime.Now;
            var avatar = Avatar ?? new Entities.Avatar();
            var url = "https://www.googleapis.com/oauth2/v4/token";

            var data =
                string.Format(
                    "grant_type=authorization_code&code={0}&redirect_uri={1}&client_id={2}&client_secret={3}",
                    code,
                    System.Web.HttpContext.Current.Server.UrlEncode(Channel.CodeCallback),
                    Channel.AppId,
                    Channel.AppSecret);

            try
            {
                var r = new BrowserClient();
                var response = r.SendHttpRequest(url, true, "POST", data, null, null, null, "utf-8", "application/json", "application/x-www-form-urlencoded");
                Logger.Info(response);
                var token = response.JsonToObjContract<Response.GoogleToken>();
                if (token != null)
                {
                    avatar.ChannelId = Channel.Id;
                    avatar.Token = token.AccessToken;
                    avatar.RefreshToken = token.RefreshToken;
                    avatar.TokenGenerated = now;
                    avatar.TokenExpires = now.AddSeconds(token.ExpiresIn);
                    avatar.SynchroTime = now;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return avatar;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public override Entities.Avatar RefreshToken(string refreshToken)
        {
            var now = DateTime.Now;
            var avatar = Avatar ?? new Entities.Avatar();
            var url = "https://www.googleapis.com/oauth2/v4/token";

            var data =
                string.Format(
                    "grant_type=refresh_token&refresh_token={0}&redirect_uri={1}&client_id={2}&client_secret={3}",
                    refreshToken,
                    System.Web.HttpContext.Current.Server.UrlEncode(Channel.CodeCallback),
                    Channel.AppId,
                    Channel.AppSecret);

            try
            {
                var r = new BrowserClient();
                var response = r.SendHttpRequest(url, true, "POST", data, null, null, null, "utf-8", "application/json", "application/x-www-form-urlencoded");
                Logger.Info(response);
                var token = response.JsonToObjContract<Response.MsonlineToken>();
                if (token != null)
                {
                    avatar.ChannelId = Channel.Id;
                    avatar.Token = token.AccessToken;
                    avatar.RefreshToken = token.RefreshToken;
                    avatar.TokenGenerated = now;
                    avatar.TokenExpires = now.AddSeconds(token.ExpiresIn);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return avatar;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Entities.Avatar ReadAvatar()
        {
            var now = DateTime.Now;
            var avatar = Avatar ?? new Entities.Avatar();
            var url = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={0}";

            try
            {
                var r = new BrowserClient();
                var response = r.SendHttpRequest(string.Format(url, avatar.Token), true, "GET", "", null, null, null,
                    "utf-8", "application/json");
                Logger.Info(response);
                var user = response.JsonToObjContract<Response.GoogleUser>();
                if (user != null)
                {
                    avatar.ChannelIdentity = user.Id;
                    avatar.ChannelId = Channel.Id;
                    avatar.DisplayName = user.Name;
                    avatar.Code = user.Id;
                }

                var email=ReadApi("https://www.googleapis.com/userinfo/email");
                if (email.Contains("email=") && email.Contains("&"))
                {
                    email = email.Substring(email.IndexOf("email=", StringComparison.Ordinal) + 6);
                    email = email.Substring(0, email.IndexOf("&", StringComparison.Ordinal));
                    avatar.Code = email;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return avatar;
        }

        private ServiceAccountCredential GetCredential()
        {
            //var initializer = new GoogleAuthorizationCodeFlow.Initializer
            //{
            //    ClientSecrets = new ClientSecrets
            //    {
            //        ClientId = Channel.AppId,
            //        ClientSecret = Channel.AppSecret
            //    },
            //    Scopes = Channel.Parameters.Split(' ')
            //};
            //var flow = new GoogleAuthorizationCodeFlow(initializer);
            //var token =
            //    new TokenResponse
            //    {
            //        AccessToken = Avatar.Token,
            //        ExpiresInSeconds = Avatar.TokenExpires.Subtract(Avatar.TokenGenerated).TotalSeconds.ToBigInt(),
            //        RefreshToken = Avatar.RefreshToken,
            //        Scope = Channel.Parameters,
            //        TokenType = "Bearer",
            //    };
            //var uc=new UserCredential(flow, Avatar.Code, token);
            //Logger.Debug(uc.SerializeXml());
            //return uc;

            var p12File = HttpContext.Current.Server.MapPath("/key.p12");
            String serviceAccountEmail = "atimerservice@ukalendar-171604.iam.gserviceaccount.com";

            var certificate = new X509Certificate2(p12File, "notasecret", X509KeyStorageFlags.Exportable);

            ServiceAccountCredential credential = new ServiceAccountCredential(
               new ServiceAccountCredential.Initializer(serviceAccountEmail)
               {
                   Scopes = Channel.Parameters.Split(' ')
               }.FromCertificate(certificate));

            // Create the service.
            return credential;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public override List<Entities.Message> ReadMessages(int page = 1)
        {
            //var url = "https://www.googleapis.com/discovery/v1/apis/gmail/v1/rest";
            //var response = ReadApi(url);

            //Logger.Info(response);

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GetCredential(),
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            // List labels.
            IList<Label> labels = request.Execute().Labels;
            Logger.Info("Labels:");
            if (labels != null && labels.Count > 0)
            {
                foreach (var labelItem in labels)
                {
                    Logger.Info($"{labelItem.Name}");
                }
            }
            else
            {
                Logger.Info("No labels found.");
            }

            return new List<Entities.Message>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public override List<Entities.Contact> ReadContacts(int page = 1)
        {
            var url = "https://www.google.com/m8/feeds/contacts/default/thin";
            var response = ReadApi(url);

            Logger.Info(response);

            //GoogleCredential credential = GoogleCredential.FromAccessToken(Avatar.Token);
            //Contactsetr

            return new List<Entities.Contact>();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public override List<Entities.Event> ReadEvents(int page = 1)
        {
            Logger.Info("READ CROSS GOOGLE APIS");
            var url =
                //"https://www.googleapis.com/calendar/v3/users/me/calendarList?maxResults=20&key=AIzaSyANE0wp9JV-wLdAB758yypFefoY13URLTs";
            "https://www.googleapis.com/calendar/v3/calendars/primary/events?maxResults=100&key=AIzaSyANE0wp9JV-wLdAB758yypFefoY13URLTs";
            //"https://www.googleapis.com/calendar/v3/calendars/primary/events?oauth_token="+Avatar.Token;
            //    //"https://www.googleapis.com/calendar/v3/users/me/calendarList/primary";
            //    //"https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest";
            //https://www.googleapis.com/calendar/v3/users/me/calendarList
            //https://content.googleapis.com/calendar/v3/users/me/calendarList?maxResults=20
            //https://content.googleapis.com/calendar/v3/users/me/calendarList?key=
            //var url = "https://www.googleapis.com/calendar/v3/users/me/calendarList";
            Logger.Info(url);
            var response = ReadApi(url);

            Logger.Info(response);
            //Logger.Debug("SET CREDENTIAL.");


            ////GoogleCredential credential =GoogleCredential.FromAccessToken(Avatar.Token);
            
            ////Logger.Debug("credential.IsCreateScopedRequired=" + credential.IsCreateScopedRequired);
            //Logger.Debug("INIT CLIENTINITIAZER.");

            //var clientInitializer = new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = GetCredential(),
            //    ApplicationName = ApplicationName,
            //};

            //Logger.Debug("INIT SERVICE.");
            //var service = new CalendarService(clientInitializer);

            //Logger.Debug("START REQUEST");
            //// Fetch the list of calendars.
            //var calendars = service.CalendarList.List().Execute();
            //Logger.Info(calendars.Items.Count);

            ////var fetchTasks = new List<Task<Google.Apis.Calendar.v3.Data.Events>>(calendars..Count);
            ////foreach (var calendar in calendars.Items)
            ////{
            ////    var request = service.Events.List(calendar.Id);
            ////    request.MaxResults = MaxEventsPerCalendar;
            ////    request.SingleEvents = true;
            ////    request.TimeMin = DateTime.Now;
            ////    fetchTasks.Add(request.ExecuteAsync());
            ////}
            ////var fetchResults = await Task.WhenAll(fetchTasks);

            ////// Sort the events and put them in the model.
            ////var upcomingEvents = from result in fetchResults
            ////                     from evt in result.Items
            ////                     where evt.Start != null
            ////                     let date = evt.Start.DateTime.HasValue ?
            ////                         evt.Start.DateTime.Value.Date :
            ////                         DateTime.ParseExact(evt.Start.Date, "yyyy-MM-dd", null)
            ////                     let sortKey = evt.Start.DateTimeRaw ?? evt.Start.Date
            ////                     orderby sortKey
            ////                     select new { evt, date };
            ////var eventsByDate = from result in upcomingEvents.Take(MaxEventsOverall)
            ////                   group result.evt by result.date into g
            ////                   orderby g.Key
            ////                   select g;

            ////var eventGroups = new List<CalendarEventGroup>();
            ////foreach (var grouping in eventsByDate)
            ////{
            ////    eventGroups.Add(new CalendarEventGroup
            ////    {
            ////        GroupTitle = grouping.Key.ToLongDateString(),
            ////        Events = grouping,
            ////    });
            ////}
            ////Logger.Debug("CREATE REQUEST.");
            ////EventsResource.ListRequest request = service.Events.List("primary");
            ////request.TimeMin = DateTime.Now;
            ////request.ShowDeleted = false;
            ////request.SingleEvents = true;
            ////request.MaxResults = 10;
            ////request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            ////Logger.Debug("EXECUTE REQUEST.");
            ////// List events.
            ////Events events = request.Execute();
            ////Logger.Info("Upcoming events:"+Avatar.SerializeXml());
            ////if (events.Items != null && events.Items.Count > 0)
            ////{
            ////    foreach (var eventItem in events.Items)
            ////    {
            ////        string when = eventItem.Start.DateTime.ToString();
            ////        if (String.IsNullOrEmpty(when))
            ////        {
            ////            when = eventItem.Start.Date;
            ////        }
            ////        Logger.Info(string.Format("{0} ({1})", eventItem.Summary, when));
            ////    }
            ////}
            ////else
            ////{
            ////    Logger.Info("No upcoming events found.");
            ////}


            return new List<Entities.Event>();
        }

        //public async 


        public override Entities.Event CreateEvent(Entities.Event eventInfo)
        {

            return eventInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventInfo"></param>
        /// <returns></returns>
        public override bool CancelEvent(Entities.Event eventInfo)
        {
            var result = true;


            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventInfo"></param>
        /// <returns></returns>
        public override Entities.Event UpdateEvent(Entities.Event eventInfo)
        {

            return eventInfo;
        }
    }
}
