using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.Data
{
    public class HtmlHelper
    {
        #region 导航

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="pageSize">分页页码</param>
        /// <param name="pageNo"></param>
        /// <param name="quringString"></param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="displayText"></param>
        /// <param name="aclass"></param>
        /// <returns></returns>
        private static string RenderPaginationUrl(string controllerName, string actionName, int pageSize, int pageNo, string quringString, int currentPage, string displayText, string aclass)
        {
            var url = string.Format("/{0}/{1}?pageSize={2}&pageNo={3}{4}",
                                 controllerName,
                                 actionName,
                                 pageSize,
                                 pageNo,
                                 quringString);
            var classString = aclass == ""
                                  ? (pageNo == currentPage ? " class='active'" : "")
                                  : aclass;

            return string.Format("<li {1}><a href='{2}'>{0}</a></li>", displayText, classString, url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo">当前页</param>
        /// <param name="quringString"></param>
        /// <returns></returns>
        public static string RenderPagination(string controllerName, string actionName, int recordCount, int pageSize, int pageNo, string quringString)
        {
            if (recordCount == 0)
                return "";

            var sb = new StringBuilder();

            var pageCount = recordCount % pageSize == 0 ? recordCount / pageSize : (recordCount / pageSize + 1);

            if (pageCount <= 1)
                return "";

            int indexBegin, indexEnd;
            if (pageCount > 7)
            {
                if ((pageNo > 5) && (pageNo < pageCount - 4))
                {
                    indexBegin = pageNo - 3;
                    indexEnd = indexBegin + 7;
                }
                else
                {
                    if ((pageNo <= 5))
                    {
                        indexBegin = 1;
                        indexEnd = indexBegin + 7;
                    }
                    else
                    {
                        indexEnd = pageCount;
                        indexBegin = indexEnd - 7;
                    }
                }
            }
            else
            {
                indexBegin = 1;
                indexEnd = pageCount;
            }

            indexBegin = Math.Max(1, indexBegin);
            indexEnd = Math.Min(pageCount, indexEnd);

            sb.Append("<div><ul class=\"pagination\">");
            sb.Append(
                RenderPaginationUrl(
                    controllerName,
                    actionName,
                    pageSize,
                    pageNo > 1 ? pageNo - 1 : 1,
                    quringString,
                    pageNo,
                    "&laquo;",
                    " aria-label='Previous'"));

            for (int i = indexBegin; i <= indexEnd; i++)
            {
                sb.Append(
                    RenderPaginationUrl(
                        controllerName,
                        actionName,
                        pageSize,
                        i,
                        quringString,
                        pageNo,
                        i + "",
                        ""));
            }
            sb.Append(
                RenderPaginationUrl(
                    controllerName,
                    actionName,
                    pageSize,
                    pageNo < pageCount ? pageNo + 1 : pageCount,
                    quringString,
                    pageNo,
                    "&raquo;",
                    " aria-label='Next'"));
            //sb.Append(string.Format("<span>第{0}页，共{1}页</span></div>", pageNo, pageCount));
            sb.Append("</ul></div>");

            return sb.ToString();
        }

        #endregion
    }
}
