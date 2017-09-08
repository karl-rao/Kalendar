using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients
{
    public class GoogleHelper
        : BaseHelper, IBaseHelper
    {
        public new string Signin()
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

        public new Entities.Avatar ExchangeToken(string code)
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
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return avatar;
        }

        public new Entities.Avatar RefreshToken(string refreshToken)
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

        public new Entities.Avatar ReadAvatar()
        {
            var now = DateTime.Now;
            var avatar = Avatar ?? new Entities.Avatar();
            var url = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={0}";

            var r = new BrowserClient();
            var response = r.SendHttpRequest(string.Format(url,avatar.Token),true,"GET","",null,null,null,"utf-8", "application/json");
            Logger.Info(response);
            var user = response.JsonToObjContract<Response.GoogleUser>();
            if (user != null)
            {
                avatar.ChannelIdentity = user.Id;
                avatar.ChannelId = Channel.Id;
                avatar.DisplayName = user.Name;
                avatar.Code = user.Id;
            }

            return avatar;
        }

        public new List<Entities.Message> ReadMessages(int page = 1)
        {
            var url = "https://www.googleapis.com/discovery/v1/apis/gmail/v1/rest";
            var response = ReadApi(url);

            Logger.Info(response);


            return new List<Entities.Message>();
        }

        public new List<Entities.Contact> ReadContacts(int page = 1)
        {
            var url = "https://www.google.com/m8/feeds/contacts/default/full";
            var response = ReadApi(url);

            Logger.Info(response);

            return new List<Entities.Contact>();

        }


        public new List<Entities.Event> ReadEvents(int page = 1)
        {
            var url = "https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest";
            var response = ReadApi(url);

            Logger.Info(response);
            return new List<Entities.Event>();
        }


        public new Entities.Event CreateEvent(Entities.Event eventInfo)
        {

            return eventInfo;
        }

        public new bool CancelEvent(Entities.Event eventInfo)
        {
            var result = true;


            return result;
        }

        public new Entities.Event UpdateEvent(Entities.Event eventInfo)
        {

            return eventInfo;
        }
    }
}
