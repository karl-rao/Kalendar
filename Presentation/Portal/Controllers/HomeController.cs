using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;
using System.Web.Mvc;
using Kalendar.Zero.Data.Controls;
using Kalendar.Zero.Utility.Common;

namespace Kalendar.Web.Portal.Controllers
{
    public class HomeController : PortalController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(string id)
        {
            return string.IsNullOrEmpty(id)
                ? View(CurrentAccount)
                : View(id, CurrentAccount);
        }

        public ActionResult Projects(string id,int pageSize=20,int pageNo=1)
        {
            CurrentAccount.CurrentData = new Zero.Data.Domain.ProjectPack(id, pageSize, pageNo, "Home", "Project",AccountId);
            
            return View(CurrentAccount);
        }

        [HttpPost]
        public ActionResult Refresh()
        {
            var projectIds = Request["ProjectIds"] ?? "";

            if (!string.IsNullOrEmpty(projectIds))
            {
                var ac = CurrentAccount;
                ac.ViewProjectIds = projectIds;
                CurrentAccount = ac;
            }

            return Json(CurrentAccount);
        }

        #region Account
        
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Signin(FormCollection collection)
        {
            var result = "";
            var now = DateTime.Now;
            var validCode = collection["validCode"] ?? "";
            var entity = new Zero.DB.Entity.Base.AccountPO { Valid = true, CreateTime = DateTime.Now };

            if (validCode == (Session[Zero.Utility.Config.SessionName + "register"] + ""))
            {
                var dbHelper = new Zero.DB.BLL.Base.AccountBO();

                var existsCount = dbHelper.ExecuteScalar(
                    "Select Count(1) AS RecordCount From Account Where Mobile=@Mobile ",
                    CommandType.Text,
                    new IDbDataParameter[]
                    {new SqlParameter("@Mobile", collection["Mobile"])});

                if (existsCount.ToInt() > 0)
                {
                    result = "该号码已经注册！";
                }
                else
                {
                    entity.FillFromCollection(collection);

                    entity.Valid = true;
                    entity.LastSignin = now;
                    entity.UpdateTime = now;
                    entity.CreateTime = now;

                    var salt = Guid.NewGuid().ToString().Replace("-", "").ToLower().Substring(10, 10);
                        entity.Salt = salt;
                    entity.LoginPassword = Karlrao.Utility.Crypto.HashAlgorithm.MD5(collection["Password"] + salt);

                    entity = dbHelper.Insert(entity);

                    if (entity != null)
                    {
                        SetAuthorizedAccountTiket(entity, true);
                        //TraineeHelper.Refresh(entity);
                    }
                }
            }

            return Content(result);
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var result = "";
            var mobile = Karlrao.Utility.Variables.Text.RegexHelper.SQLParse((collection["Mobile"] ?? "").ToLower());
            var loginPassword = collection["Password"];
            var validCode = collection["validCode"] ?? "";
            var url = Request["ReturnUrl"] ?? "";

            try
            {
                if (validCode == (Session[Zero.Utility.Config.SessionName + "login"] + ""))
                {
                    if ((mobile != "") && (loginPassword != ""))
                    {
                        var condition = string.Format(" Mobile='{0}' ", mobile);
                        var dbHelper = new Zero.DB.BLL.Base.AccountBO();
                        var trainee = dbHelper.FindSingle(condition);

                        if (trainee != null)
                        {
                            var now = DateTime.Now;

                            if (trainee.LoginPassword ==
                                Karlrao.Utility.Crypto.HashAlgorithm.MD5(loginPassword + trainee.Salt))
                            {
                                trainee.LastSignin = now;
                                dbHelper.Update(trainee);

                                Logger.Info("LOGIN");
                                SetAuthorizedAccountTiket(trainee, true);
                                //Data.Domain.TraineeHelper.Refresh(trainee);
                            }
                            else
                            {
                                result = "密码错误";
                                Logger.Error(result);
                            }
                        }
                        else
                        {
                            result = "账号不存在";
                            Logger.Error(result);
                        }
                    }
                }
                else
                {
                    result = "验证码错误";
                    Logger.Error(result);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            //return View();
            return Content(result); //RedirectToAction("Login", new { @ReturnUrl = url });
        }


        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Signout()
        {
            AccountSignOut();
            CurrentAccount = null;

            return RedirectToAction("Index");
        }

        #endregion

        #region Common Data

        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetValidateCode(string id)
        {
            var vCode = new Karlrao.Utility.Files.Image.ValidateCode();
            string code = vCode.CreateValidateCode(4);
            Session[Zero.Utility.Config.SessionName + id] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);

            if (string.IsNullOrEmpty(Request.ServerVariables["HTTP_REFERER"]))
            {
                return null;
            }

            return File(bytes, @"image/jpeg");
        }


        public ActionResult RenderJsonForBootstrapCalendar()
        {
            var events = CurrentAccount.Events;

            foreach (var kalendarEvent in events.Result)
            {
                kalendarEvent.EventStart = kalendarEvent.EventStart.ToDateTime().DateTimeToTimestamp() + "";
                kalendarEvent.EventEnd = kalendarEvent.EventEnd.ToDateTime().DateTimeToTimestamp() + "";
            }

            return Content(events.ObjToJsonContract());
        }

        public ActionResult RenderJsonForFullCalendar()
        {
            var events = CurrentAccount.Events;

            return Content(events.Result.ObjToJsonContract());
        }


        #endregion

        public ActionResult Test()
        {
            var request=new Zero.ApiTerminal.Clients.Request.WoowRequest
            {
                ChannelSymbol=1,
                Method= "RefreshToken",
                Channel=new Zero.ApiTerminal.Entities.Channel { AppId = "APPID"},
                Avatar=new Zero.ApiTerminal.Entities.Avatar { Code="CODE"},
                Data="REFRESHTOKEN"
            };

            //var r=new Zero.ApiTerminal.Clients.BrowserClient();
            //var resp = r.SendHttpRequest("http://proxy.atimer.cn/api/woow", false,"POST", request.ObjToJson(), null,null,null,"UTF-8","application/json", "application/json");

            var r=new BrowserClient();
            var resp = r.SendHttpRequest("http://proxy.atimer.cn/api/woow",false,"POST" ,request.ObjToJson(), null, null, null, "UTF-8", "application/json", "application/json");
            
            return Content(resp.SerializeXml());
        }

    }
}