using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kalendar.Zero.Utility.Controls
{
    /// <summary>
    /// 页基类
    /// </summary>
    public partial class BasicPage : Page
    {
        #region 解决ViewState过于庞大的问题

        //protected override object LoadPageStateFromPersistenceMedium()
        //{
        //    string viewStateID = (string)((Pair)base.LoadPageStateFromPersistenceMedium()).Second;
        //    string stateStr = (string)Cache[viewStateID];
        //    if (stateStr == null)
        //    {
        //        string fn = Path.Combine(this.Request.PhysicalApplicationPath, @"App_Data/ViewState/" + viewStateID);
        //        stateStr = File.ReadAllText(fn);
        //    }
        //    return new ObjectStateFormatter().Deserialize(stateStr);
        //}

        //protected override void SavePageStateToPersistenceMedium(object state)
        //{
        //    string value = new ObjectStateFormatter().Serialize(state);
        //    string viewStateID = (DateTime.Now.Ticks + (long)this.GetHashCode()).ToString(); //产生离散的id号码
        //    string fn = Path.Combine(this.Request.PhysicalApplicationPath, @"App_Data/ViewState/" + viewStateID);
        //    //ThreadPool.QueueUserWorkItem(File.WriteAllText(fn, value));
        //    File.WriteAllText(fn, value);
        //    Cache.Insert(viewStateID, value);
        //    base.SavePageStateToPersistenceMedium(viewStateID);
        //}

        #endregion
		/// <summary>
        /// 日志处理
        /// </summary>
        public static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Page.DataBind();
        }

        #region 登录登出

        /// <summary>
        /// 保存用户票据信息
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="keyValue">The key value.</param>
        /// <param name="saveTicket">if set to <c>true</c> [save ticket].</param>
        public void SaveAuthenticationTicket(object obj, string displayName, object keyValue, bool saveTicket)
        {
            Session[Config.SessionName] = obj;

            var ticket = new FormsAuthenticationTicket(
                1,
                displayName,
                DateTime.Now,
                saveTicket ? DateTime.Now.AddMonths(1) : DateTime.Now,
                true,
                keyValue.ToString(),
                "/");

            string encTicket = FormsAuthentication.Encrypt(ticket);

            Common.Cookie.SetClientCookie(FormsAuthentication.FormsCookieName, encTicket);
        }
		
        /// <summary>
        /// 记录最后活跃时间
        /// </summary>
        public void SaveTimerCookie()
        {
            string timerCookieName = Config.SiteDomain + ".LastAction";
            string timerCookieValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Common.Cookie.SetClientCookie(timerCookieName, timerCookieValue);
        }

        /// <summary>
        /// 退出登录状态
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
            Common.Cookie.SetClientCookie(FormsAuthentication.FormsCookieName, "");

            Session[Config.SessionName] = null;
            Session.Abandon();
        }

        #endregion

        #region IP SessionID GUID ClientCode
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <value>The IP.</value>
        public string IP
        {
            get
            {
                return Common.VariableParser.IP;
            }
        }

        /// <summary>
        /// 获取客户端会话SessionID
        /// </summary>
        /// <value>The session ID.</value>
        public string SessionID
        {
            get
            {
                return Common.VariableParser.SessionID;
            }
        }

        /// <summary>
        /// GUIDs this instance.
        /// </summary>
        /// <returns></returns>
        public string GUID
        {
            get { return Common.VariableParser.GUID; }
        }

        /// <summary>
        /// 客户端编号
        /// </summary>
        /// <value>The client id.</value>
        public string ClientCode
        {
            get
            {
                return Common.VariableParser.ClientCode;
            }
        }

        #endregion
        
        #region 分析请求

        /// <summary>
        /// 请求参数的加密
        /// </summary>
        /// <param name="inValue">The in value.</param>
        /// <returns></returns>
        public string EncryptQueringString(string inValue)
        {
            return HttpContext.Current.Server.UrlEncode(Common.Encryption64.Encrypt(inValue));
        }

        /// <summary>
        /// Gets the HT value.
        /// </summary>
        /// <param name="ht">The ht.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public string GetHTValue(Hashtable ht, string key, string defaultValue)
        {
            if (ht.ContainsKey(key))
            {
                return ht[key].ToString();
            }
            return defaultValue;
        }

        /// <summary>
        /// Queries the table.
        /// </summary>
        /// <returns></returns>
        public Hashtable QueryTable()
        {
            //SetPage();
            string inStr = Request.QueryString.ToString();
            if (inStr != "")
            {
                var ht = new Hashtable();
                string ecStr = Server.UrlDecode(inStr);
                string dcStr = Common.Encryption64.Decrypt(ecStr);

                if (dcStr == "")
                    return null;

                string[] keys = dcStr.Split(new[] { '&' });

                for (int i = 0; i < keys.Length; i++)
                {
                    string[] keyItems = keys[i].Split(new[] { '=' });
                    if (keyItems.Length > 1)
                    {
                        ht.Add(keyItems[0].ToLower(), keyItems[1]);
                    }
                }

                if (ht.Count > 0)
                {
                    return ht;
                }
                return null;
            }
            return null;
        }

        

        #endregion

        #region 页面属性


        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>The page title.</value>
        public string PageTitle{ get;set;}

        /// <summary>
        /// Gets the site title.
        /// </summary>
        /// <value>The site title.</value>
        public string SiteTitle { get; set; }
        /// <summary>
        /// Gets the site keywords.
        /// </summary>
        /// <value>The site keywords.</value>
        public string SiteKeywords { get; set; }
        /// <summary>
        /// Gets the site description.
        /// </summary>
        /// <value>The site description.</value>
        public string SiteDescription { get; set; }

        /// <summary>
        /// Gets the page URL.
        /// </summary>
        /// <value>The page URL.</value>
        public string PageUrl
        {
            get
            {
                return string.Format("http://{0}{1}", Request.ServerVariables["SERVER_NAME"],
                                     Request.ServerVariables["SCRIPT_NAME"]);
            }
        }

        /// <summary>
        /// Gets the page refer.
        /// </summary>
        /// <value>The page refer.</value>
        public string PageRefer
        {
            get {
                return string.IsNullOrEmpty(Request.ServerVariables["HTTP_REFERER"])
                           ? ""
                           : Request.ServerVariables["HTTP_REFERER"]; }
        }

        #endregion

        #region client Script

        /// <summary>
        /// Notices the specified msg.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public void Notice(string msg)
        {
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Notice", "<script>confirm('" + msg + "');</script>");
        }

        /// <summary>
        /// Warnings the specified msg.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public void Warning(string msg)
        {
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Notice", "<script>alert('" + msg + "');</script>");
        }

        /// <summary>
        /// Confirms the specified msg.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="url">The URL.</param>
        public void Confirm(string msg, string url)
        {
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Notice", "<script>if(confirm('" + msg + "')){document.location='" + url + "';}</script>");
        }

        /// <summary>
        /// Confirms the specified msg.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="urlOK">The URL OK.</param>
        /// <param name="urlCancel">The URL cancel.</param>
        public void Confirm(string msg, string urlOK, string urlCancel)
        {
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Notice", "<script>if(confirm('" + msg + "')){document.location='" + urlOK + "';}else{document.location='" + urlCancel + "';}</script>");
        }

        #endregion

        #region 常用函数

        /// <summary>
        /// Inits the drop down list.
        /// </summary>
        /// <param name="ddl">The DDL.</param>
        /// <param name="dataList">The data list.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="appendRootText">The append root text.</param>
        /// <param name="appendRootValue">The append root value.</param>
        /// <param name="rootEnabled">if set to <c>true</c> [root enabled].</param>
        public void InitDropDownList(
            DropDownList ddl,
            IEnumerable dataList,
            object selectedValue,
            string appendRootText,
            string appendRootValue,
            bool rootEnabled)
        {
            if (dataList != null)
            {
                ddl.DataSource = dataList;
                ddl.DataBind();
            }
            if(appendRootText!="")
                ddl.Items.Insert(0, new ListItem(appendRootText, appendRootValue, rootEnabled));
            if(selectedValue!=null)
                for (var i = 0; i < ddl.Items.Count; i++)
                {
                    if (ddl.Items[i].Value != selectedValue.ToString()) continue;
                    ddl.SelectedIndex = i;
                    break;
                }
        }

        /// <summary>
        /// Inits the drop down list.
        /// </summary>
        /// <param name="ddl">The DDL.</param>
        /// <param name="selectedValue">The selected value.</param>
        public void InitDropDownList(
            DropDownList ddl,
            object selectedValue)
        {
            if(selectedValue!=null)
                for (var i = 0; i < ddl.Items.Count; i++)
                {
                    if (ddl.Items[i].Value != selectedValue.ToString()) continue;
                    ddl.SelectedIndex = i;
                    break;
                }
        }

        /// <summary>
        /// Inits the radio button list.
        /// </summary>
        /// <param name="rbl">The RBL.</param>
        /// <param name="dataList">The data list.</param>
        /// <param name="selectedValue">The selected value.</param>
        public void InitRadioButtonList(
            RadioButtonList rbl,
            IEnumerable dataList,
            object selectedValue)
        {
            if (dataList != null)
            {
                rbl.DataSource = dataList;
                rbl.DataBind();
            }
            if (selectedValue != null)
                for (var i = 0; i < rbl.Items.Count; i++)
                {
                    if (rbl.Items[i].Value != selectedValue.ToString()) continue;
                    rbl.SelectedIndex = i;
                    break;
                }
        }

        /// <summary>
        /// Inits the check box list.
        /// </summary>
        /// <param name="cbl">The CBL.</param>
        /// <param name="dataList">The data list.</param>
        /// <param name="selectedValues">The selected values.</param>
        public void InitCheckBoxList(
            CheckBoxList cbl,
            IEnumerable dataList,
            List<string> selectedValues)
        {
            if (dataList != null)
            {
                cbl.DataSource = dataList;
                cbl.DataBind();
            }
            if (selectedValues != null)
                for (var i = 0; i < cbl.Items.Count; i++)
                {
                    cbl.Items[i].Selected = selectedValues.Contains(cbl.Items[i].Value);
                }
        }

        #endregion
    }
}
