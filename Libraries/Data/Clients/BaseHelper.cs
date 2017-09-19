using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalendar.Zero.Utility.Common;

namespace Kalendar.Zero.Data.Clients
{
    public class BaseHelper
    {
        public  readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DB.Entity.Base.ChannelPO Channel { get; set; }

        public DB.Entity.Base.AccountAvatarsPO Avatar { get; set; }

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
                return response.Content;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return "";
        }
    }
}
