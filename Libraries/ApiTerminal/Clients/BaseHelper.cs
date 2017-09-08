using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients
{
    public class BaseHelper
    {
        public  readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
          
        public Entities.Channel Channel { get; set; }

        public Entities.Avatar Avatar { get; set; }


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

        public string Signin()
        {
            return "";
        }

        public Entities.Avatar ExchangeToken(string code)
        {
            return new Entities.Avatar();
        }

        public Entities.Avatar RefreshToken(string refreshToken)
        {
            return new Entities.Avatar();
        }

        public Entities.Avatar ReadAvatar()
        {
            return new Entities.Avatar();
        }

        public List<Entities.Message> ReadMessages(int page = 1)
        {
            return new List<Entities.Message>();
        }

        public List<Entities.Contact> ReadContacts(int page = 1)
        {
            return new List<Entities.Contact>();
        }

        public List<Entities.Event> ReadEvents(int page = 1)
        {
            return new List<Entities.Event>();
        }

        public Entities.Event CreateEvent(Entities.Event eventInfo)
        {
            return eventInfo;
        }

        public bool CancelEvent(Entities.Event eventInfo)
        {
            return false;
        }

        public Entities.Event UpdateEvent(Entities.Event eventInfo)
        {
            return eventInfo;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string ReadApi(string url)
        {
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
