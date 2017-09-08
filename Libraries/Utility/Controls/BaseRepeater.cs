using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Kalendar.Utility.Controls", "dpc")]

namespace Kalendar.Zero.Utility.Controls
{
    /// <summary>
    /// Repeater
    /// </summary>
    [DefaultProperty("")]
    [ToolboxData("<{0}:BaseRepeater runat=server></{0}:BaseRepeater>")]
    public class BaseRepeater<T> : Repeater where T:DB.Entity.Base.BaseEntity,new()
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
                    return ViewState["Condition"]+"";
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
        /// Gets or sets the list field.
        /// </summary>
        /// <value>The list field.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("list")]
        [Localizable(true)]
        public string TemplateModule
        {
            get
            {
                try
                {
                    return ViewState["TemplateModule"].ToString();
                }
                catch (Exception) { return "list"; }
            }
            set
            {
                ViewState["TemplateModule"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the notice for data null.
        /// </summary>
        /// <value>
        /// The notice for data null.
        /// </value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string NoticeForDataNull
        {
            get
            {
                try
                {
                    return ViewState["NoticeForDataNull"].ToString();
                }
                catch (Exception) { return ""; }
            }
            set
            {
                ViewState["NoticeForDataNull"] = value;
            }
        }

        /// <summary>
        /// 输出Repeater
        /// </summary>
        /// <param name="output">The output.</param>
        public override void RenderControl(HtmlTextWriter output)
        {
            var dbHelper = new DB.BLL.Base.BaseBLL<T>();
            var list = dbHelper.Find(Condition, null, OrderField, IsDescending == 1, PageSize, AbsolutePage);
            if (list != null)
            {
                DataSource = list;

                string itemTemplateFile = string.Format(Config.ItemTemplatePath, TemplateModule);
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(itemTemplateFile)))
                {
                    ItemTemplate = Page.LoadTemplate(itemTemplateFile);
                }

                DataBind();

                Render(output);
            }
            else
            {
                output.Write(NoticeForDataNull);
            }
        }
    }
}