using System;
using System.Web;

namespace Kalendar.Utility.Common
{
    /// <summary>
    /// 缓存管理公用类
    /// </summary>
    public class Cookie
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Sets the client cookie.
        /// </summary>
        /// <param name="cookieName">Name of the cookie.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <returns></returns>
        public static bool SetClientCookie(string cookieName, object cookieValue)
        {
            try
            {
                var myCookie = new HttpCookie(cookieName)
                {
                    Expires = DateTime.Now.AddYears(30),
                    Value = cookieValue.ToString()
                };
				if(Config.SiteDomain!="")
					myCookie.Domain=Config.SiteDomain;
                HttpContext.Current.Response.Cookies.Set(myCookie);
				return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
				return false;
            }
        }


        /// <summary>
        /// 获取一个Cookie值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
            {
                return HttpContext.Current.Server.HtmlEncode(cookie.Value);
            }
            return string.Empty;
        }

        /// <summary>
        /// 保存一个cookie值
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetValue(string key, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);

            if (cookie != null)
            {
                HttpContext.Current.Request.Cookies.Remove(key);
            }
            cookie = new HttpCookie(key, value);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        public static void SetValue(string key, string value, DateTime expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);

            if (cookie != null)
            {
                HttpContext.Current.Request.Cookies.Remove(key);
            }
            cookie = new HttpCookie(key, value) {Expires = expires};
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 移除一个cookie值
        /// </summary>
        /// <param name="key">The key.</param>
        public static void Remove(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
            {
                cookie.Expires = System.DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

	}
}
