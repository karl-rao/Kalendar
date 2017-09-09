using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.Data
{
    public class Config
    {
        public static string SiteTitle = "UKONG";

        public static string ProxyApiUri =DB.Config.AppSetting("ProxyApiUri","http://proxy.atimer.cn/api/woow");
    }
}
