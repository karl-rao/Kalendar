using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kalendar.Web.Portal.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadContacts(
            int channelId = 0, 
            int channelSymbol=1,
            string token="",
            int page=1)
        {
            var result = new List<Zero.DB.Entity.Base.AccountContactsPO>();

            if (channelId > 0 && !string.IsNullOrEmpty(token))
            {
                switch (channelSymbol)
                {
                    case 1:
                        var ms = new Zero.Data.Clients.DataHelper
                        {
                            Channel = new Zero.DB.Entity.Base.ChannelPO {Id = channelId},
                            Avatar = new Zero.DB.Entity.Base.AccountAvatarsPO {Token = token}
                        };
                        result = ms.ReadContacts(page);

                        break;
                    case 2:

                        break;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}