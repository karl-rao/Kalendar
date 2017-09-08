using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Kalendar.Utility.Controls", "dpc")]

namespace Kalendar.Zero.Utility.Controls
{
    /// <summary>
    /// 导航
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BaseNavigator runat=server></{0}:BaseNavigator>")]
        public class BaseNavigator<T> : WebControl where T : Karlrao.Utility.MSSQL.Entity.BaseEntity, new()
    {
        #region 控件属性

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("1=1")]
        [Localizable(true)]
        public string Condition
        {
            get
            {
                try
                {
                    return ViewState["Condition"] + "";
                }
                catch (Exception) { return "1=1"; }
            }

            set
            {
                ViewState["Condition"] = value;
            }
        }

        /// <summary>
        /// 页面尺寸
        /// </summary>
        /// <value>The size of the page.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("50")]
        [Localizable(true)]
        public int PageSize
        {
            get
            {
                try
                {
                    return (int)ViewState["PageSize"];
                }
                catch (Exception) { return 50; }
            }

            set
            {
                ViewState["PageSize"] = value;
            }
        }
        /// <summary>
        /// 当前页码
        /// </summary>
        /// <value>The absolute page.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("1")]
        [Localizable(true)]
        public int AbsolutePage
        {
            get
            {
                try
                {
                    return (int)ViewState["AbsolutePage"];
                }
                catch (Exception) { return 1; }
            }

            set
            {
                ViewState["AbsolutePage"] = value;
            }
        }
        /// <summary>
        /// 排序方式
        /// </summary>
        /// <value>The order field.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(" UpdateTime desc")]
        [Localizable(true)]
        public string OrderField
        {
            get
            {
                try
                {
                    return ViewState["OrderField"].ToString();
                }
                catch (Exception) { return " UpdateTime desc"; }
            }

            set
            {
                ViewState["OrderField"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the is descending.
        /// </summary>
        /// <value>
        /// The is descending.
        /// </value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int IsDescending
        {
            get
            {
                try
                {
                    return (int)ViewState["IsDescending"];
                }
                catch (Exception) { return 0; }
            }

            set
            {
                ViewState["IsDescending"] = value;
            }
        }

        #endregion

        /// <summary>
        /// 导航页地址
        /// </summary>
        /// <value>The page URL.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("list.aspx")]
        [Localizable(true)]
        public string PageUrl
        {
            get
            {
                try
                {
                    return (String)ViewState["PageUrl"];
                }
                catch (Exception)
                {
                    return "list.aspx";
                }
            }

            set
            {
                ViewState["PageUrl"] = value;
            }
        }

        /// <summary>
        /// 输出导航HTML
        /// </summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(ResultNavigator());
        }

        /// <summary>
        /// Results the navigator.
        /// </summary>
        /// <returns></returns>
        protected string ResultNavigator()
        {
            var dbHelper = new Karlrao.Utility.MSSQL.BLL.BaseBLL<T>();
            int recordCount = dbHelper.Count(Condition, null);
            int pageCount = (recordCount % PageSize == 0 ? recordCount / PageSize : recordCount / PageSize + 1);

            var sb = new StringBuilder();

            sb.Append("<div class='PagerHolder'>");
            sb.Append(GetUrl(1, "首页", "StartItem"));

            int iStart = Math.Max(1, AbsolutePage - 3);
            int iEnd = Math.Min(pageCount, iStart + 6);

            for (int iLoop = iStart; iLoop <= iEnd; iLoop++)
            {
                if (iLoop == AbsolutePage)
                {
                    sb.Append(GetUrl(iLoop, iLoop.ToString(), "AbsolutePagerItem"));
                }
                else
                {
                    sb.Append(GetUrl(iLoop, iLoop.ToString(), "PagerItem"));
                }
            }

            sb.Append(GetUrl(pageCount, "末页", "EndItem"));
            sb.Append(string.Format("<div class='{1}'><a href='' title='一共{0}条记录被发现'>{0} 条记录</a></div>", recordCount, "CountItem"));
            sb.Append("</div>");

            return sb.ToString();
        }
        /// <summary>
        /// 加密链接地址
        /// </summary>
        /// <param name="pageNo">The page no.</param>
        /// <param name="text">The text.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns></returns>
        private string GetUrl(int pageNo, string text, string cssClass)
        {
            string queringString = @"Condition={0}&PageSize={1}&AbsolutePage={2}&OrderField={3}&IsDescending={4}";

            queringString = string.Format(
                queringString,
                HttpContext.Current.Server.UrlEncode(Condition), 
                PageSize,
                pageNo,
                OrderField,
                IsDescending);
            queringString = HttpContext.Current.Server.UrlEncode(Common.Encryption64.Encrypt(queringString));

            return string.Format("<div class='{3}'><a href='{0}?{1}'>{2}</a></div>", PageUrl, queringString, text, cssClass);
        }
    }
}