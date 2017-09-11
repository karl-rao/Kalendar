using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients
{
    public abstract class BaseHelper
    {
        public  readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
          
        public Entities.Channel Channel { get; set; }

        public Entities.Avatar Avatar { get; set; }

        public BaseHelper() { }

        /// <summary>
        /// 
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

        public abstract string Signin();

        public abstract Entities.Avatar ExchangeToken(string code);

        public abstract Entities.Avatar RefreshToken(string refreshToken);

        public abstract Entities.Avatar ReadAvatar();

        public abstract List<Entities.Message> ReadMessages(int page = 1);

        public abstract List<Entities.Contact> ReadContacts(int page = 1);

        public abstract List<Entities.Event> ReadEvents(int page = 1);

        public abstract Entities.Event CreateEvent(Entities.Event eventInfo);

        public abstract bool CancelEvent(Entities.Event eventInfo);

        public abstract Entities.Event UpdateEvent(Entities.Event eventInfo);

        #endregion

        /// <summary>
        /// 
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
                    {"Authorization", $"Bearer {Avatar.Token}"}
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
