using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// AgentQueue=服务器任务缓存类
    /// </summary>
    public class AgentQueue
    {
        /// <summary>
        /// 
        /// </summary>
        private const string CacheName = Config.CacheKeyPrefix + ".{0}.{1}.AgentQueue";
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public static string CacheKey(DB.Entity.Ext.AgentQueue queue)
        {
            return string.Format(CacheName, queue.Channel.Id, queue.Avatar.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public static bool Exists(DB.Entity.Ext.AgentQueue queue)
        {
            var key = CacheKey(queue);
            var filePath = string.Format(Utility.Config.CacheFolder, key);

            return File.Exists(filePath);
        }

        /// <summary>
        /// 新建任务
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public static bool Append(DB.Entity.Ext.AgentQueue queue)
        {
            try
            {
                var cacheKey = CacheKey(queue);
                Common.JsonCache.Set(cacheKey, queue);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }
        
    }
}
