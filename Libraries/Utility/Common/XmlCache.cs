using System;
using System.IO;

namespace Kalendar.Zero.Utility.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlCache
    {
        private static readonly log4net.ILog Logger
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            try
            {
                var now = DateTime.Now;
                var filePath = string.Format(Config.CacheFolder, key);
                if (!File.Exists(filePath)) return default(T);

                var s = FileParser.ReadFile(filePath, "UTF-8");
                if (!string.IsNullOrEmpty(s))
                {
                    var data = s.DeserializeXml<T>();
                    Logger.Info("反序列化 spent " + DateTime.Now.Subtract(now).TotalMilliseconds + " ms");
                    if (data != null)
                    {
                        Cache.Set(key, data, filePath);
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Set(string key, object value)
        {
            var now = DateTime.Now;
            var filePath = string.Format(Config.CacheFolder, key);

            try
            {
                var data = value.SerializeXml();
                filePath=FileParser.WriteFile(filePath, data,"UTF-8");
                
                Logger.Info(
                    $"写入文件缓存，{key + ":" + filePath},{data.Length} spent {DateTime.Now.Subtract(now).TotalMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return filePath;
        }

        /// <summary>
        /// 移除缓存文件
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            try
            {
                var filePath = string.Format(Config.CacheFolder, key);
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
