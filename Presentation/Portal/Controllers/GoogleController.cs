using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kalendar.Zero.Data.Controls;
using Kalendar.Zero.DB.Entity.Base;
using Kalendar.Zero.Utility.Common;

namespace Kalendar.Web.Portal.Controllers
{
    public class GoogleController : PortalController
    {
        private static ChannelPO Channel
        {
            get { return Zero.Utility.DataCache.Channel.CacheList().FindLast(o => o.Valid && o.ChannelSymbol == 2); }
        }

        private Zero.Data.Clients.DataHelper _helper
            = new Zero.Data.Clients.DataHelper
            {
                Channel = Channel
            };

        // GET: Google
        public ActionResult Index()
        {
            if (AccountId == 0)
                return RedirectToAction("Signin");

            var data = Zero.Data.Domain.AccountHelper.Get(AccountId);
            data.CurrentData = Channel;

            return View(data);
        }

        public ActionResult Signin()
        {
            return Redirect(_helper.Signin());
        }

        public ActionResult Callback(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction("Index", "Home");
            }

            _helper.Avatar = _helper.ExchangeToken(code);
            Logger.Info("获取AccessToken:"+ _helper.Avatar.Token);
            var avatar = _helper.ReadAvatar();
            Logger.Info("ReadAvatar");
            var contacts = _helper.ReadContacts();
            var messages = _helper.ReadMessages();
            var schedules = _helper.ReadSchedules();

            var account = Zero.Data.Domain.AccountHelper.SaveAccount(
                _helper.Channel,
                AccountInfo,
                avatar,
                contacts,
                messages,
                schedules
                );

            if (account != null)
            {
                SetAuthorizedAccountTiket(account, true);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Error");
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult TestContacts()
        {
            var token =
                "ya29.GluvBO1iWvywBLMbzLY-9xloOA1r8uo2sJxgxBWzWW3wEUeearc_HdSoRAwk2hG2LBcT27skAK-lx5-Q57ntOGdcZGXQQhDJRmp5ONZDKQ_bNY0eIeFd_jHp4qGF";

            var url = "https://www.google.com/m8/feeds/contacts/default/full";
            var response = ReadApi(url,token);
            
            return Content(response);
        }

        public ActionResult TestMessages()
        {
            var token =
                "ya29.GluvBO1iWvywBLMbzLY-9xloOA1r8uo2sJxgxBWzWW3wEUeearc_HdSoRAwk2hG2LBcT27skAK-lx5-Q57ntOGdcZGXQQhDJRmp5ONZDKQ_bNY0eIeFd_jHp4qGF";

            var url = "https://www.googleapis.com/gmail/v1/users/me/messages";
                //"https://www.googleapis.com/gmail/v1/users/";
                //"https://www.googleapis.com/discovery/v1/apis/gmail/v1/rest";
            var response = ReadApi(url, token);

            return Content(response);
        }
        //
        public ActionResult TestMessage()
        {
            var token =
                "ya29.GluvBO1iWvywBLMbzLY-9xloOA1r8uo2sJxgxBWzWW3wEUeearc_HdSoRAwk2hG2LBcT27skAK-lx5-Q57ntOGdcZGXQQhDJRmp5ONZDKQ_bNY0eIeFd_jHp4qGF";

            var url = "https://www.googleapis.com/gmail/v1/users/me/messages/15e0a5e9f32043b2";
            //"https://www.googleapis.com/gmail/v1/users/";
            //"https://www.googleapis.com/discovery/v1/apis/gmail/v1/rest";
            var response = ReadApi(url, token);

            return Content(response);
        }

        public ActionResult TestScedules()
        {
            var token =
                "ya29.GluvBO1iWvywBLMbzLY-9xloOA1r8uo2sJxgxBWzWW3wEUeearc_HdSoRAwk2hG2LBcT27skAK-lx5-Q57ntOGdcZGXQQhDJRmp5ONZDKQ_bNY0eIeFd_jHp4qGF";

            var url = "https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest";
            var response = ReadApi(url, token);

            return Content(response);
        }


        private string ReadApi(string url,string token)
        {
            try
            {
                var kv = new Dictionary<string, string>
                {
                    {"Authorization", string.Format("Bearer {0}", token)}
                };

                var r = new BrowserClient();
                var response = r.SendHttpRequest(url, true, "GET", "", kv, null, null, "utf-8", "application/json",
                    "application/x-www-form-urlencoded");

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