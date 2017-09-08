using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Karlrao.Utility.WebHelper;

namespace Kalendar.Zero.Utility.Controls
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BackendController : Controller
    {
        public static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region  导出到CSV文件并且提示下载
        /// <summary>
        /// 导出到CSV文件并且提示下载
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public void DataToCSV(string fileName,string data)
        {
            Response.ClearHeaders();
            Response.Clear();
            Response.Expires = 0;
            Response.BufferOutput = true;
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}.csv", System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8)));
            Response.ContentType = "text/h323;charset=gbk";
            Response.Write(data);
            Response.End();
        }

        #endregion

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
                //cookie.Domain = Config.SiteDomain;
                cookie.Path = "/";
                //cookie.Expires = DateTime.Now.AddYears(1);

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
        public void UserSignOut()
        {
            FormsAuthentication.SignOut();
            WriteCookieToClient(FormsAuthentication.FormsCookieName, "");

            Session[Config.SessionName] = null;
            Session.Abandon();
        }

        /// <summary>
        /// 保存管理员身份信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveCookie"></param>
        /// <returns></returns>
        public bool SetAuthorizedAdminTiket(DB.Entity.Base.AccountPO entity, bool saveCookie)
        {
            Session[Config.SessionName] = entity;

            try
            {
                var ticket = new FormsAuthenticationTicket(1, entity.Email, DateTime.Now, DateTime.Now.AddMonths(1),
                                                           true, entity.Id + "");
                string encTicket = FormsAuthentication.Encrypt(ticket);
                if (saveCookie)
                {
                    WriteCookieToClient(FormsAuthentication.FormsCookieName, encTicket);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 管理员信息
        /// </summary>
        public DB.Entity.Base.AccountPO AdminInfo
        {
            get
            {
                DB.Entity.Base.AccountPO adminInfo = null;
                try
                {
                    if (Session[Config.SessionName] != null)
                        adminInfo = (DB.Entity.Base.AccountPO)Session[Config.SessionName];
                    else
                        adminInfo = ReadAdminInfoFromCookie();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
                if (adminInfo == null)
                    UserSignOut();

                return adminInfo;
            }
        }
        
        /// <summary>
        /// 从客户端Cookie中读取管理员信息
        /// </summary>
        /// <returns></returns>
        private DB.Entity.Base.AccountPO ReadAdminInfoFromCookie()
        {
            FormsAuthenticationTicket ticket = null;
            try
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                    ticket = FormsAuthentication.Decrypt(
                        Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            }
            catch (Exception)
            {
                ticket = null;
            }
            if (ticket != null)
            {
                var adminDal = new DB.BLL.Base.AccountBO();
                DB.Entity.Base.AccountPO entity = adminDal.FindByID(ticket.UserData.ToInt());

                if (entity != null)
                    Session[Config.SessionName] = entity;
                return entity;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageno"></param>
        /// <param name="addition"></param>
        public void NextPage(
            int shopid,
            string controllerName,
            string actionName,
            int pagesize,
            int pageno,
            string addition="")
        {
            string nextUrl = string.Format(
                "/{0}/{1}/{2}?pagesize={3}&pageno={4}" + addition,
                controllerName, actionName,shopid,
                pagesize, pageno);
            //Response.Write(NextUrl);
            Response.Write(string.Format("<meta http-equiv=\"Refresh\" content=\"1;URL={0}\" />", nextUrl));
        }
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
		public string RenderBoolean(bool val)
        {
            var template = "<img src='/Content/{0}.png'/>";
            return string.Format(template, val ? "true" : "false");
        }
    }
}
