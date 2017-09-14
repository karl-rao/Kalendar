using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients
{
    /// <summary>
    /// 处理器基类
    /// </summary>
    public abstract class BaseHelper
    {
        /// <summary>
        /// 应用名称申明
        /// </summary>
        public const string ApplicationName = "Atimer.cn";

        /// <summary>
        /// 日志记录
        /// </summary>
        public  readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// 渠道
        /// </summary>
        public Entities.Channel Channel { get; set; }

        /// <summary>
        /// 用户身份
        /// </summary>
        public Entities.Avatar Avatar { get; set; }

        protected BaseHelper() { }

        /// <summary>
        /// 微软中日历周期参数转换
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public int GetMsonlineCycle(string val)
        {
            var result = 0;

            switch (val)
            {
                case "daily":
                    result = 1;
                    break;
                case "weekly":
                    result = 2;
                    break;
                case "absoluteMonthly":
                    result = 5;
                    break;
                case "absoluteYearly":
                    result = 6;
                    break;
            }

            return result;
        }

        #region 方法示例

        /// <summary>
        /// 登录地址
        /// </summary>
        /// <returns></returns>
        public abstract string Signin();

        /// <summary>
        /// oauth用code获取token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public abstract Entities.Avatar ExchangeToken(string code);

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public abstract Entities.Avatar RefreshToken(string refreshToken);
        
        /// <summary>
        /// 读取用户身份信息
        /// </summary>
        /// <returns></returns>
        public abstract Entities.Avatar ReadAvatar();

        /// <summary>
        /// 读取用户消息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public abstract List<Entities.Message> ReadMessages(int page = 1);

        /// <summary>
        /// 读取用户联系人
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public abstract List<Entities.Contact> ReadContacts(int page = 1);

        /// <summary>
        /// 读取用户日历事件
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public abstract List<Entities.Event> ReadEvents(int page = 1);

        /// <summary>
        /// 创建日历事件
        /// </summary>
        /// <param name="eventInfo"></param>
        /// <returns></returns>
        public abstract Entities.Event CreateEvent(Entities.Event eventInfo);

        /// <summary>
        /// 取消日历事件
        /// </summary>
        /// <param name="eventInfo"></param>
        /// <returns></returns>
        public abstract bool CancelEvent(Entities.Event eventInfo);

        /// <summary>
        /// 更新日历事件
        /// </summary>
        /// <param name="eventInfo"></param>
        /// <returns></returns>
        public abstract Entities.Event UpdateEvent(Entities.Event eventInfo);

        #endregion

        /// <summary>
        /// GET读取REST
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string ReadApi(string url)
        {
            Logger.Debug("READ API...");
            try
            {
                var kv = new Dictionary<string, string>
                {
                    {"Authorization", $"Bearer {Avatar.Token}"},
                    {"www-authenticate", $"Bearer {Avatar.Token}"}
                };

                var r = new BrowserClient();
                var response = r.SendHttpRequest(url, true, "GET", "", kv, null, null, "utf-8", "application/json", "application/x-www-form-urlencoded");

                Logger.Info(response);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return "";
        }

    }
}
