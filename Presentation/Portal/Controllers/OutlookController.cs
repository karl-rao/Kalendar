using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kalendar.Zero.Data.Controls;
using Kalendar.Zero.DB.Entity.Base;

namespace Kalendar.Web.Portal.Controllers
{
    public class OutlookController : PortalController
    {
        private static ChannelPO Channel
        {
            get { return Zero.Utility.DataCache.Channel.CacheList().FindLast(o => o.Valid && o.ChannelSymbol == 1); }
        }

        private Zero.Data.Clients.MsonlineHelper _helper
            = new Zero.Data.Clients.MsonlineHelper
            {
                Channel = Channel
            };
        
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

            var avatar=_helper.ReadAvatar();
            var contacts=_helper.ReadContacts();
            var messages=_helper.ReadMessages();
            var schedules=_helper.ReadSchedules();

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
    }
}