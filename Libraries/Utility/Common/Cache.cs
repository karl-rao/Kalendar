using System;
using System.Collections.Generic;
using System.Web;

namespace Kalendar.Zero.Utility.Common
{
    /// <summary>
    /// 缓存管理公用类
    /// </summary>
    public class Cache
    {
        private static readonly log4net.ILog Logger
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static T Get<T>(string cacheKey)
        {
            switch (Config.CacheModule.ToLower())
            {
                case "json":
                    var json = HttpRuntime.Cache[cacheKey] ?? JsonCache.Get<T>(cacheKey);
                    return (T)json;
                case "xml":
                    var xml = HttpRuntime.Cache[cacheKey] ?? XmlCache.Get<T>(cacheKey);
                    return (T)xml;
                default:
                    System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                    var de= objCache[cacheKey];
                    return de != null ? (T)de : default(T);
            }
        }

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public static object Remove(string cacheKey)
        {
            switch (Config.CacheModule.ToLower())
            {
                case "json":
                    HttpRuntime.Cache.Remove(cacheKey);
                    try
                    {
                        JsonCache.Remove(cacheKey);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                    return false;
                case "xml":
                    HttpRuntime.Cache.Remove(cacheKey);
                    try
                    {
                        XmlCache.Remove(cacheKey);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                    return false;
                default:
                    System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                    return objCache.Remove(cacheKey);
            }
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void Clear()
        {
            switch (Config.CacheModule.ToLower())
            {
                default:
                    System.Web.Caching.Cache objCache = HttpRuntime.Cache;

                    var cacheKeys = new List<string>();
                    var cacheEnum = objCache.GetEnumerator();
                    while (cacheEnum.MoveNext())
                    {
                        cacheKeys.Add(cacheEnum.Key.ToString());
                    }
                    foreach (string cacheKey in cacheKeys)
                    {
                        objCache.Remove(cacheKey);
                    }
                    break;
            }
        }

        /// <summary>
        /// Inserts the specified cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="objObject">The obj object.</param>
        public static void Insert(string cacheKey, object objObject)
        {
            switch (Config.CacheModule.ToLower())
            {
                case "time":
                    Set(cacheKey, objObject, Config.CacheDuration);
                    break;
                case "json":
                    var jsonFile = JsonCache.Set(cacheKey, objObject);
                    Set(cacheKey, objObject, jsonFile);
                    break;
                case "xml":
                    var xmlFile = XmlCache.Set(cacheKey, objObject);
                    Set(cacheKey, objObject, xmlFile);
                    break;
                default:
                    Set(cacheKey, objObject);
                    break;
            }
        }

        #region 程序池缓存

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey">缓存名称</param>
        /// <param name="objObject">缓存值</param>
        public static void Set(string cacheKey, object objObject)
        {
            if (objObject != null)
            {
                System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                objCache.Insert(cacheKey, objObject);
            }
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey">缓存名称</param>
        /// <param name="objObject">缓存值</param>
        /// <param name="dependencyFileName">缓存依赖条件</param>
        public static void Set(string cacheKey, object objObject, string dependencyFileName)
        {
            if (objObject != null)
            {
                System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                objCache.Insert(cacheKey, objObject, new System.Web.Caching.CacheDependency(dependencyFileName));
            }
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey">缓存名称</param>
        /// <param name="objObject">缓存值</param>
        /// <param name="cacheDuration">缓存过期时间</param>
        public static void Set(string cacheKey, object objObject, int cacheDuration)
        {
            if (objObject != null)
            {
                System.Web.Caching.Cache objCache = HttpRuntime.Cache;
                objCache.Insert(cacheKey, objObject, null, DateTime.Now.AddMinutes(cacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }

        #endregion

    }
}
