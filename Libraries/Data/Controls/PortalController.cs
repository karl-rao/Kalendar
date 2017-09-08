using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kalendar.Zero.DB.Agent;
using Kalendar.Zero.Utility.Common;


namespace Kalendar.Zero.Data.Controls
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class PortalController : Controller
    {
        /// <summary>
        /// Logger
        /// </summary>
        public static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// 写入客户端Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        private void WriteCookieToClient(string cookieName, string cookieValue)
        {
            var cookie = new HttpCookie(cookieName, cookieValue);
            try
            {
                cookie.Path = "/";
                cookie.Expires = DateTime.Now.AddDays(14);

                Response.Cookies.Set(cookie);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        public void AccountSignOut()
        {
            FormsAuthentication.SignOut();
            WriteCookieToClient(FormsAuthentication.FormsCookieName, "");

            Session[Utility.Config.SessionName] = null;
            Session.Abandon();
        }

        /// <summary>
        /// 保存会员身份信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveCookie"></param>
        /// <returns></returns>
        public bool SetAuthorizedAccountTiket(DB.Entity.Base.AccountPO entity, bool saveCookie)
        {
            Session[Utility.Config.SessionName] = entity;

            try
            {
                var ticket = new FormsAuthenticationTicket(
                    1,
                    entity.NickName,//DataCache.Account.GetNickName( entity.Id), 
                    DateTime.Now,
                    saveCookie ? DateTime.Now.AddMonths(1) : DateTime.Now.AddDays(1),
                    true, entity.Id + "");
                string encTicket = FormsAuthentication.Encrypt(ticket);
                WriteCookieToClient(FormsAuthentication.FormsCookieName, encTicket);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
        public static string GUID
        {
            get { return Guid.NewGuid().ToString().Replace("-", "").ToLower(); }
        }
        /// <summary>
        /// 客户端编号
        /// </summary>
        /// <value>
        /// The client id.
        /// </value>
        public static string ClientCode
        {
            get
            {
                string cookieName = Utility.Config.SessionName + ".Client";

                if ((System.Web.HttpContext.Current.Request.Cookies[cookieName] == null) || (string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies[cookieName].Value)))
                {
                    var clientCookie = new HttpCookie(cookieName, GUID)
                    {
                        //Domain = Utility.Config.SiteDomain,
                        Path = "/",
                        Expires = DateTime.Now.AddMonths(1)
                    };
                    System.Web.HttpContext.Current.Response.Cookies.Add(clientCookie);
                }

                return System.Web.HttpContext.Current.Request.Cookies[cookieName].Value;
            }
        }

        private string CurrentAccountKey
        {
            get
            {
                return AccountId > 0
                        ? Utility.Config.SessionNameTemp + "." + AccountId
                        : Utility.Config.SessionNameTemp + "." + ClientCode;
            }
        }

        public Data.Domain.Account CurrentAccount
        {
            get
            {
                var cacheKey = CurrentAccountKey;
                var objType = Utility.Common.Cache.Get<Domain.Account>(cacheKey);
                if (objType == null||AccountInfo!=null)
                {
                    var acc = new Data.Domain.Account(AccountInfo);
                    Utility.Common.Cache.Insert(cacheKey, acc);

                    return acc;
                }

                return objType;
            }
            set
            {
                var cacheKey = CurrentAccountKey;
                if (value == null)
                {
                    Utility.Common.Cache.Remove(cacheKey);
                }
                else
                {
                    Utility.Common.Cache.Insert(cacheKey, value);
                    //Session[Utility.Config.SessionNameTemp] = value;
                }
            }
        }

        /// <summary>
        /// 会员信息
        /// </summary>
        public DB.Entity.Base.AccountPO AccountInfo
        {
            get
            {
                DB.Entity.Base.AccountPO accountInfo = null;
                try
                {
                    if (Session[Utility.Config.SessionName] != null)
                    {
                        accountInfo = (DB.Entity.Base.AccountPO) Session[Utility.Config.SessionName];
                    }
                    else
                    {
                        accountInfo = ReadAccountInfoFromCookie();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
                if (accountInfo == null)
                    AccountSignOut();

                return accountInfo;
            }
        }

        /// <summary>
        /// 当前会员主键
        /// </summary>
        public int AccountId
        {
            get { return AccountInfo == null ? 0 : AccountInfo.Id; }
        }
        
        /// <summary>
        /// 从客户端Cookie中读取会员信息
        /// </summary>
        /// <returns></returns>
        private DB.Entity.Base.AccountPO ReadAccountInfoFromCookie()
        {
            FormsAuthenticationTicket ticket = null;
            try
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    var cookieValue = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                    if (!string.IsNullOrEmpty(cookieValue))
                        ticket = FormsAuthentication.Decrypt(cookieValue);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                ticket = null;
            }
            if (ticket != null)
            {
                var entity = Utility.DataCache.Account.GetEntity(ticket.UserData.ToInt());
                //var bllAccount = new DB.BLL.Base.AccountBO();
                //DB.Entity.Base.AccountPO entity = bllAccount.FindByID(ticket.UserData.ToInt());

                if (entity != null)
                    Session[Utility.Config.SessionName] = entity;
                return entity;
            }
            return null;
        }
        
        ///// <summary>
        ///// 客户端推广渠道
        ///// </summary>
        //public DB.Entity.Base.MarketingPO ClientMarketingInfo
        //{
        //    get
        //    {
        //        var marketingCode = Utility.Common.Cookie.GetValue(Utility.Config.MarketingCookieName);
        //        if (!string.IsNullOrEmpty(marketingCode))
        //        {
        //            return DataCache.Marketing.GetEntityByCode(marketingCode);
        //        }
        //        return null;
        //    }
        //}

        /// <summary>
        /// 客户端推广渠道主键
        /// </summary>
        //public int ClientMarketingId
        //{
        //    get { return ClientMarketingInfo != null ? ClientMarketingInfo.Id : 0; }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="AccountId"></param>
        ///// <returns></returns>
        //public bool LogForRegister(int AccountId)
        //{
        //    try
        //    {
        //        var now = DateTime.Now;
        //        var pointLog = new DB.Entity.Base.AccountPointLogPO
        //                           {
        //                               CreateTime = now,
        //                               AccountId = AccountId,
        //                               Deleted = false,
        //                               Expense = 0,
        //                               ExpireDate = now.AddYears(1),
        //                               Income = 50,
        //                               Note = "注册",
        //                               OrdersId = 0,
        //                               PointType = 1,
        //                               UpdateTime = now
        //                           };
        //        new DB.BLL.Base.AccountPointLogBO().Insert(pointLog);
        //        UpdateAccountPoint(AccountId, 50);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //    return true;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="AccountId"></param>
        ///// <returns></returns>
        //public bool LogForLogin(int AccountId)
        //{
        //    try
        //    {
        //        var now = DateTime.Now;

        //        var bllPointLog = new DB.BLL.Base.AccountPointLogBO();
        //        var exists = bllPointLog.Count(
        //            string.Format("AccountId={0} AND PointType=2 AND DateDiff(DAY,CreateTime,'{1}')=0", AccountId,
        //                          now), null);

        //        if (exists== 0)
        //        {
        //            var pointLog = new DB.Entity.Base.AccountPointLogPO
        //                               {
        //                                   CreateTime = now,
        //                                   AccountId = AccountId,
        //                                   Deleted = false,
        //                                   Expense = 0,
        //                                   ExpireDate = now.AddYears(1),
        //                                   Income = 10,
        //                                   Note = "登录",
        //                                   OrdersId = 0,
        //                                   PointType = 2,
        //                                   UpdateTime = now
        //                               };
        //            bllPointLog.Insert(pointLog);
        //            UpdateAccountPoint(AccountId, 10);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //    return true;
        //}

        /// <summary>
        /// 联合登录保存用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="friendlyName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DB.Entity.Base.AccountPO SaveAccount(
            string openId,
            string friendlyName,
            string key
            )
        {
            var now = DateTime.Now;
            var trans = new Transaction();
            try
            {
                trans.Begin();
                if (openId != "")
                {
                    var dbHelper = new DB.Agent.MssqlHelper<DB.Entity.Base.AccountPO>();
                    var entity = dbHelper.FindSingle(string.Format("PassportCode='{0}'", openId),trans.DbConnection,trans.DbTrans);

                    if (entity == null)
                    {
                        entity = new DB.Entity.Base.AccountPO
                                     {
                                         NickName = friendlyName,
                                         PassportCode = openId,
                                         PassportKey = key,
                                         Balance = 0,
                                         CreateTime = now,
                                         UpdateTime = now,
                                         Credit = 0,
                                         Valid = false,
                                         LastSignin = now
                                     };
                        entity = dbHelper.Insert(entity, trans.DbConnection, trans.DbTrans);
                    }
                    else
                    {
                        var newEntity=new DB.Entity.Base.AccountPO();
                        newEntity.FillFrom(entity);

                        newEntity.PassportKey = key;
                        newEntity.LastSignin = now;
                        newEntity.UpdateTime = now;

                        dbHelper.Update(entity,newEntity, trans.DbConnection, trans.DbTrans);
                    }

                    return entity;
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

            return null;
        }
    }
}
