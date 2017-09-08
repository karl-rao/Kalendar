using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalendar.Zero.Data.Clients.Response;
using Kalendar.Zero.DB.Entity.Base;
using Kalendar.Zero.Utility.Common;

namespace Kalendar.Zero.Data.Clients
{
    public class GoogleHelper
        : BaseHelper, IBaseHelper
    {
        public string Signin()
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

        public AccountAvatarsPO ExchangeToken(string code)
        {
            var now = DateTime.Now;
            var avatar = Avatar ?? new AccountAvatarsPO { CreateTime = now, Valid = true };
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
                var response = r.SendHttpRequest(url,true,"POST", data,null,null,null,"UTF-8","application/json", "application/x-www-form-urlencoded");
                Logger.Info(response);
                var token = response.JsonToObjContract<Response.GoogleToken>();
                if (token != null)
                {
                    avatar.ChannelId = Channel.Id;
                    avatar.Token = token.AccessToken;
                    avatar.RefreshToken = token.RefreshToken;
                    avatar.TokenGenerated = now;
                    avatar.TokenExpires = now.AddSeconds(token.ExpiresIn);
                    avatar.UpdateTime = now;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return avatar;
        }

        public AccountAvatarsPO RefreshToken(string refreshToken)
        {
            var now = DateTime.Now;
            var avatar = Avatar ?? new AccountAvatarsPO { CreateTime = now, Valid = true };
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
                var response = r.SendHttpRequest(url, true, "POST", data, null, null, null, "UTF-8", "application/json", "application/x-www-form-urlencoded");
                Logger.Info(response);
                var token = response.JsonToObjContract<Response.MsonlineToken>();
                if (token != null)
                {
                    avatar.ChannelId = Channel.Id;
                    avatar.Token = token.AccessToken;
                    avatar.RefreshToken = token.RefreshToken;
                    avatar.TokenGenerated = now;
                    avatar.TokenExpires = now.AddSeconds(token.ExpiresIn);
                    avatar.UpdateTime = now;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return avatar;
        }

        public AccountAvatarsPO ReadAvatar()
        {
            var now = DateTime.Now;
            var avatar = Avatar ?? new AccountAvatarsPO { CreateTime = now, Valid = true };
            var url = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={0}";

            var r = new BrowserClient();
            var response =
                r.SendHttpRequest(string.Format(url, avatar.Token), true, "GET", "", null, null, null, "UTF-8", "application/json", "application/json");
            //r.SendHttpRequestGet(string.Format(url,avatar.Token),"", "application/json", null);
            Logger.Info(response);
            var user = response.JsonToObjContract<GoogleUser>();
            if (user != null)
            {
                avatar.ChannelIdentity = user.Id;
                avatar.ChannelId = Channel.Id;
                avatar.DisplayName = user.Name;
                avatar.Code = user.Id;
            }

            return avatar;
        }

        public List<DB.Entity.Base.AccountMessagesPO> ReadMessages(int page = 1)
        {
            var url = "https://www.googleapis.com/discovery/v1/apis/gmail/v1/rest";
            var response = ReadApi(url);

            Logger.Info(response);


            return new List<AccountMessagesPO>();
        }

        public List<DB.Entity.Base.AccountContactsPO> ReadContacts(int page = 1)
        {
            var url = "https://www.google.com/m8/feeds/contacts/default/full";
            var response = ReadApi(url);

            Logger.Info(response);

            return new List<AccountContactsPO>();

        }


        public List<DB.Entity.Base.SchedulePO> ReadSchedules(int page = 1)
        {
            var url = "https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest";
            var response = ReadApi(url);

            Logger.Info(response);
            return new List<SchedulePO>();
        }


        public SchedulePO CreateSchedules(SchedulePO schedule)
        {

            return schedule;
        }

        public bool CancelSchedules(SchedulePO schedule)
        {
            var result = true;


            return result;
        }

        public SchedulePO UpdateSchedules(SchedulePO schedule)
        {

            return schedule;
        }

        private string ReadApi(string url)
        {
            try
            {
                var kv = new Dictionary<string, string>
                {
                    {"Authorization", string.Format("Bearer {0}", Avatar.Token)}
                };

                var r = new BrowserClient();
                //var response = r.SendHttpRequestGet(url, "application/x-www-form-urlencoded", "application/json", kv);
                var response = r.SendHttpRequest(url, true, "GET", "", kv, null, null, "utf-8", "application/json", "application/x-www-form-urlencoded");
                Logger.Info(response);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return "";
        }
    }
}
