using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;
using System.Web.Mvc;
using Kalendar.Zero.Data.Controls;
using Kalendar.Zero.DB.Agent;
using Kalendar.Zero.DB.Entity.Base;
using Kalendar.Zero.Utility.Common;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

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
            var entity = new Zero.DB.Entity.Base.AccountPO {Valid = true, CreateTime = DateTime.Now};

            if (validCode == (Session[Zero.Utility.Config.SessionName + "register"] + ""))
            {
                var trans = new Transaction();
                try
                {
                    trans.Begin();
                    var dbHelper = new Zero.DB.Agent.MssqlHelper<AccountPO>();

                    var existsCount = dbHelper.ExecuteScalar(
                        "Select Count(1) AS RecordCount From Account Where Mobile=@Mobile ",
                        CommandType.Text,
                        new IDbDataParameter[]
                        {new SqlParameter("@Mobile", collection["Mobile"])}, trans.DbConnection, trans.DbTrans);

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
                        entity.LoginPassword = (collection["Password"] + salt).Md5();

                        entity = dbHelper.Insert(entity, trans.DbConnection, trans.DbTrans);

                        if (entity != null)
                        {
                            SetAuthorizedAccountTiket(entity, true);
                            //TraineeHelper.Refresh(entity);
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.RollBack();
                    Logger.Error(ex);
                }
                finally
                {
                    trans.Dispose();
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
            var mobile = collection["Mobile"].SQLParse();
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
                        var trans = new Transaction();

                        try
                        {
                            trans.Begin();
                            var dbHelper = new Zero.DB.Agent.MssqlHelper<AccountPO>();
                            var trainee = dbHelper.FindSingle(condition, trans.DbConnection, trans.DbTrans);

                            if (trainee != null)
                            {
                                var now = DateTime.Now;

                                if (trainee.LoginPassword == (loginPassword + trainee.Salt).Md5())
                                {
                                    var traineeNew = new Zero.DB.Entity.Base.AccountPO();
                                    traineeNew.FillFrom(trainee);
                                    traineeNew.LastSignin = now;
                                    dbHelper.Update(trainee, traineeNew, trans.DbConnection, trans.DbTrans);

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
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                            trans.RollBack();
                        }
                        finally
                        {
                            trans.Dispose();
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
            var vCode = new Zero.Utility.Common.ValidateCode();
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

        public ActionResult LocalTest()
        {
            var channel = Zero.Utility.DataCache.Channel.GetEntity(1);
            var avatar = Zero.Utility.DataCache.AccountAvatars.CacheList(10).FindLast(o => o.Id == 1);

            var helper=new Zero.Data.Clients.DataHelper { Channel=channel,Avatar=avatar};
            var cs=helper.ReadContacts();

            return Content(cs.SerializeXml());
        }

        public ActionResult Mail()
        {

            var channel = Zero.Utility.DataCache.Channel.GetEntity(2);
            var avatar = Zero.Utility.DataCache.AccountAvatars.CacheList(13).FindLast(o => o.Id == 5);

            var paser = new Zero.Data.Clients.DataHelper
            {
                Channel = channel,
                Avatar = avatar
            };

            Logger.Debug("TEST");
            var resp = paser.ReadMessages();
            Logger.Info(resp);
            return Content("");
        }

        public ActionResult Test()
        {
            var channel = Zero.Utility.DataCache.Channel.GetEntity(2);
            var avatar = Zero.Utility.DataCache.AccountAvatars.CacheList(13).FindLast(o => o.Id == 5);

            var paser = new Zero.Data.Clients.DataHelper
            {
                Channel = channel,
                Avatar = avatar
            };
            
            Logger.Debug("TEST");
            var resp = paser.ReadSchedules();
            Logger.Info(resp);
            //var request=new Zero.ApiTerminal.Clients.Request.WoowRequest
            //{
            //    ChannelSymbol=1,
            //    Method= "RefreshToken",
            //    Channel=new Zero.ApiTerminal.Entities.Channel { AppId = "APPID"},
            //    Avatar=new Zero.ApiTerminal.Entities.Avatar { Code="CODE"},
            //    Data="REFRESHTOKEN"
            //};

            ////var r=new Zero.ApiTerminal.Clients.BrowserClient();
            ////var resp = r.SendHttpRequest("http://proxy.atimer.cn/api/woow", false,"POST", request.ObjToJson(), null,null,null,"UTF-8","application/json", "application/json");

            //var r=new BrowserClient();
            //var resp = r.SendHttpRequest("http://proxy.atimer.cn/api/woow",false,"POST" ,request.ObjToJson(), null, null, null, "UTF-8", "application/json", "application/json");
            
            return Content("");
        }

        public ActionResult Testgu()
        {
            var content =
                "{ \"id\": \"101041433066104582113\",\"name\": \"赵马\",\"given_name\": \"马\",\"family_name\": \"赵\",\"link\": \"https://plus.google.com/101041433066104582113\",\"picture\": \"https://lh6.googleusercontent.com/-aQIfycAFfeU/AAAAAAAAAAI/AAAAAAAAAII/dbmnSFPzJk0/photo.jpg\",\"gender\": \"male\",\"locale\": \"zh-CN\"}";

            var u = content.JsonToObjContract<Zero.ApiTerminal.Clients.Response.GoogleUser>();

            return Content(u.SerializeXml());
        }

        /// <summary>
        /// ceygaswmlktlbgja
        /// </summary>
        /// <returns></returns>
        public ActionResult Caldav()
        {
            //gzky-zztx-nnta-awzy
            ///("http://dav.mail.189.cn/cal/", "18121119302@189.cn", "1029824z");
            var server = new Zero.ApiTerminal.CalDav.Client.Server
                //("http://dav.mail.189.cn/cal/", "18121119302@189.cn", "1029824z");
            ("https://p02-caldav.icloud.com/", "zhaoma@foxmail.com", "gzky-zztx-nnta-awzy");
            if (server.Supports("MKCALENDAR"))
                server.CreateCalendar("me");
            var sets = server.GetCalendars();
            Response.Write(sets.Length);
            var calendar = sets[0];
            var e = new Zero.ApiTerminal.CalDav.Event
            {
                Description = "this is a description",
                Summary = "summary",
                Sequence = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds,
            };
            calendar.Save(e);


            return Content("");
        }

    }
}