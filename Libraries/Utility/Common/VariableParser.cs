using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Kalendar.Utility.Common
{
    /// <summary>
    /// 文字处理
    /// </summary>
    public class VariableParser
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region 文字处理/正则表达式

        /// <summary>
        /// 防止注入
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string SQLParse(string text)
        {
            var sqlExp = new Regex(@"\s*\'\s+|\s(and|exec|insert|select|delete|update|count|drop|table|\*|\%|chr|mid|master|truncate|char|declare)\s");

            return sqlExp.Replace(text, "");
        }

        /// <summary>
        /// 清理HTML
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string StripHTML(string html)
        {
            string strOutput = html;

            var scriptRegExp = new Regex("<scr" + "ipt[^>.]*>[\\s\\S]*?</sc" + "ript>", RegexOptions.IgnoreCase & RegexOptions.Compiled & RegexOptions.Multiline & RegexOptions.ExplicitCapture);
            strOutput = scriptRegExp.Replace(strOutput, "");

            var styleRegex = new Regex("<style[^>.]*>[\\s\\S]*?</style>", RegexOptions.IgnoreCase & RegexOptions.Compiled & RegexOptions.Multiline & RegexOptions.ExplicitCapture);
            strOutput = styleRegex.Replace(strOutput, "");

            var objRegExp = new Regex("<(.|\\n)+?>", RegexOptions.IgnoreCase & RegexOptions.Compiled & RegexOptions.Multiline);
            strOutput = objRegExp.Replace(strOutput, "");

            objRegExp = new Regex("<[^>]+>", RegexOptions.IgnoreCase & RegexOptions.Compiled & RegexOptions.Multiline);

            strOutput = objRegExp.Replace(strOutput, "");

            strOutput = strOutput.Replace("&lt;", "<");
            strOutput = strOutput.Replace("&gt;", ">");
            strOutput = strOutput.Replace("&nbsp;", " ");

            return strOutput;
        }

        /// <summary>
        /// Strips the HTML but image.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string StripHTMLButImage(string html){
            string strOutput = html;

            Regex objRegExp = new Regex("<[^img][^>]*>", RegexOptions.IgnoreCase & RegexOptions.Compiled & RegexOptions.Multiline);
            strOutput = objRegExp.Replace(strOutput, "");

            strOutput = strOutput.Replace("&lt;", "<");
            strOutput = strOutput.Replace("&gt;", ">");
            //&nbsp; 
            strOutput = strOutput.Replace("&nbsp;", " ");

            return strOutput;
        }


        /// <summary>
        /// Strips the deny word.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="denyWords">The deny words.</param>
        /// <returns></returns>
        public static string StripDenyWord(string content, string denyWords)
        {
            var sqlExp = new Regex(string.Format(".*({0}).*", denyWords));
            return sqlExp.Replace(content, "");
        }

        /// <summary>
        /// Strips the bad word.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="badWords">The bad words.</param>
        /// <param name="fixWord">The fix word.</param>
        /// <returns></returns>
        public static string StripBadWord(string content, string badWords, string fixWord)
        {
            var sqlExp = new Regex(string.Format("({0})", badWords));
            return sqlExp.Replace(content, fixWord);
        }

        /// <summary>
        /// Masks the string.
        /// </summary>
        /// <param name="inStr">The in STR.</param>
        /// <param name="outLength">Length of the out.</param>
        /// <returns></returns>
        public static string MaskString(object inStr, int outLength)
        {
            string temp = StripHTML(inStr.ToString());
            int j = 0;
            int k = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if (Regex.IsMatch(temp.Substring(i, 1), @"[\u4e00-\u9fa5]+"))
                {
                    j += 2;
                }
                else
                {
                    j += 1;
                }
                if (j <= outLength * 2)
                {
                    k += 1;
                }
                if (j >= outLength * 2)
                {
                    return temp.Substring(0, k);
                }
            }
            return temp;
        }

        #endregion

        #region Mask

        /// <summary>
        /// Masks the money.
        /// </summary>
        /// <param name="inM">The in M.</param>
        /// <returns></returns>
        public static string MaskMoney(object inM)
        {
            try
            {
                return Convert.ToDecimal(inM) == Convert.ToInt32(inM)
                           ? Convert.ToDecimal(inM).ToString("0")
                           : Convert.ToDecimal(inM).ToString("0.00");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return "-";
            }
        }

        /// <summary>
        /// Masks the int32.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns></returns>
        public static int MaskInt32(object val)
        {
            return MaskInt32(val, 0);
        }


        /// <summary>
        /// Masks the int32.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static int MaskInt32(object val,int defaultValue)
        {
            try
            {
                return Convert.ToInt32(val);
            }catch(Exception )
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Masks the int64.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns></returns>
        public static long MaskInt64(object val)
        {
            return MaskInt64(val, 0);
        }


        /// <summary>
        /// Masks the int32.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static long MaskInt64(object val, long defaultValue)
        {
            try
            {
                return Convert.ToInt64(val);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Masks the decimal.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns></returns>
        public static decimal MaskDecimal(object val)
        {
            return MaskDecimal(val, 0);
        }

        /// <summary>
        /// Masks the decimal.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static decimal MaskDecimal(object val, decimal defaultValue)
        {
            try
            {
                return Convert.ToDecimal(val);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Masks the date time.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns></returns>
        public static DateTime MaskDateTime(object val)
        {
            return MaskDateTime(val, DateTime.Now);
        }

        /// <summary>
        /// Masks the date time.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static DateTime MaskDateTime(object val, DateTime defaultValue)
        {
            try
            {
                return Convert.ToDateTime(val);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

		/// <summary>
        /// Masks the date diff.
        /// </summary>
        /// <param name="inTime">The in time.</param>
        /// <returns></returns>
        public static string MaskDateDiff(object inTime)
        {
            if (inTime == null) return "";
            TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(inTime));

            string r = "";
            if (ts.TotalDays > 0)
                r += ts.TotalDays.ToString("0") + "天";
            if (ts.Hours > 0)
                r += ts.Hours.ToString("0") + "小时";
            if (ts.Minutes > 0)
                r += ts.Minutes.ToString("0") + "分钟";

            return r;
        }
		/// <summary>
        /// Masks the second.
        /// </summary>
        /// <param name="inSecond">The in second.</param>
        /// <returns></returns>
        public static string MaskSecond(object inSecond)
        {
            var result = "";
            try
            {
                var second = Convert.ToInt32(inSecond);

                int h = second/3600;
                int hs = second%3600;
                int m = hs / 60;
                int ms = hs % 60;

                result += h > 0 ? (h + "时") : "";
                result += m > 0 ? (m + "分") : "";
                result += ms > 0 ? (ms + "秒") : "";

            }catch(Exception )
            {
                result= "&nbsp;";
            }

            return result;
        }

        #endregion

        #region Genarator

        /// <summary>
        /// Lists the item text.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string ListItemText(string content, int length)
        {
            var sb = new StringBuilder();
            for (int i = 1; i < length; i++)
            {
                sb.Append("　");
            }
            sb.Append(content);
            return sb.ToString();
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <value>The IP.</value>
        public static string IP
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != ""
                           ? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]
                           : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
        }

        /// <summary>
        /// 获取客户端会话SessionID
        /// </summary>
        /// <value>The session ID.</value>
        public static string SessionID
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }

        /// <summary>
        /// GUIDs this instance.
        /// </summary>
        /// <returns></returns>
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
                string cookieName = Config.SessionName + ".Client";

                if ((HttpContext.Current.Request.Cookies[cookieName] == null) || (string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[cookieName].Value)))
                {
                    var clientCookie = new HttpCookie(cookieName, GUID)
                    {
                        Domain = Config.SiteDomain,
                        Path = "/",
                        Expires = DateTime.Now.AddMonths(1)
                    };
                    HttpContext.Current.Response.Cookies.Add(clientCookie);
                }

                return HttpContext.Current.Request.Cookies[cookieName].Value;
            }
        }

        /// <summary>
        /// Ms the d5.
        /// </summary>
        /// <param name="inString">The in string.</param>
        /// <returns></returns>
        public static string MD5(string inString)
        {
            var md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(inString))).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Ms the d5.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="charset">The charset.</param>
        /// <returns></returns>
        public static string MD5(string text, string charset)
        {
            var sb = new StringBuilder(32);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(charset).GetBytes(text));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the ip2 long.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns></returns>
        public static long GetIp2Long(string ip)
        {
            long result = 0;
            var ips = ip.Split(new[] {'.'});
            if (ips.Length == 4)
            {
                result = long.Parse(ips[3]) + long.Parse(ips[2])*256 + long.Parse(ips[1])*256*256 +
                         long.Parse(ips[0])*256*256*256;
            }
            return result;
        }


        #endregion
		
    }
}
