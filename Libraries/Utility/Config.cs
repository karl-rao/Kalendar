using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using Kalendar.Zero.Utility.Common;

namespace Kalendar.Zero.Utility
{
    /// <summary>
    /// 项目配置节
    /// 数据库连接
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public const string AssemblyFileName = "kzu";

        /// <summary>
        /// 缓存key 前缀
        /// </summary>
        public const string CacheKeyPrefix = AssemblyFileName + ".CK";

        /// <summary>
        /// 会话名称
        /// </summary>
        public static string SessionName = AssemblyFileName + ".SK";

        /// <summary>
        /// 
        /// </summary>
        public static string SessionNameTemp = AssemblyFileName + ".SKII";

        /// <summary>
        /// 
        /// </summary>
        public static string CacheKey = DB.Config.AppSetting("CacheKey", "sc.{0}.");

        /// <summary>
        /// 
        /// </summary>
        public static string CacheModule = DB.Config.AppSetting("CacheModule", "xml");

        /// <summary>
        /// 
        /// </summary>
        public static int CacheDuration = DB.Config.AppSetting("CacheDuration", "1800").ToInt();

        /// <summary>
        /// 
        /// </summary>
        public static string CacheFolder = DB.Config.AppSetting("CacheFolder", "");

    }
}
