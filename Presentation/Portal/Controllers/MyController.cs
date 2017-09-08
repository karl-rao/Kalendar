using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kalendar.Zero.Data.Controls;

namespace Kalendar.Web.Portal.Controllers
{
    public class MyController : PortalController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string id)
        {
            var x = CurrentAccount;

            return string.IsNullOrEmpty(id)
                ? View(x)
                : View(id, x);
        }

        public ActionResult Update(FormCollection collection)
        {
            var result = "";

            var entity = AccountInfo;

            var oriPassword = entity.LoginPassword;
            var orisalt = entity.Salt;

            return Content(result);
        }

    }
}