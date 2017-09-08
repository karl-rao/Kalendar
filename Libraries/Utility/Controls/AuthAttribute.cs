using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Kalendar.Zero.Utility.Controls
{
    /// <summary> 
    /// 权限验证 
    /// </summary> 
    public class AuthAttribute : AuthorizeAttribute
    {
        /// <summary> 
        /// ActionName 
        /// </summary> 
        public string ActionName { get; set; }

        /// <summary>
        /// ControllerName
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary> 
        /// 验证权限（action执行前会先执行这里） 
        /// </summary> 
        /// <param name="filterContext"></param> 
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var id = HttpContext.Current.User.Identity as FormsIdentity;

            if (id != null && id.IsAuthenticated)
            {
                var adminId = id.Ticket.UserData;
                var adminEntity = Utility.DataCache.Account.GetEntity(adminId);
                if (!adminEntity.IsRoot)
                {
                    filterContext.Result = new ContentResult
                    {
                        Content = "<script>alert('权限验证不通过！');history.go(-1);</script>"
                    };
                }
            }
            else
            {
                filterContext.Result = new ContentResult
                {
                    Content =
                        string.Format(
                            "<script>alert('请先登录！');window.location.href='{0}';</script>",
                            FormsAuthentication.LoginUrl)
                };
            }
        }
    } 
}
