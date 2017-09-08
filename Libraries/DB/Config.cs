using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace Kalendar.Zero.DB
{
    /// <summary>
    /// 项目配置节
    /// 数据库连接/版本
    /// </summary>
    public class Config
    {
        #region 读取配置节

        private static readonly ConnectionStringSettingsCollection ConnectionStrings = ConfigurationManager.ConnectionStrings;

        private static readonly NameValueCollection AppSettings = ConfigurationManager.AppSettings;

        #endregion

        public static string ConnectionMssqlString = ConnectionString("ConnectMSSQL");

        public static string ConnectionString(string key)
        {
            return ConnectionStrings[key] != null
                ? ConnectionStrings[key].ConnectionString
                : "";
        }

        public static string AppSetting(string key, string defaultValue)
        {
            return AppSettings[key] ?? defaultValue;
        }

    }
}
