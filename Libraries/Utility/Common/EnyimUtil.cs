using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching.Memcached;

namespace Kalendar.Utility.Common
{
	/// <summary>
    /// Memcached操作类
    /// </summary>
    public class EnyimUtil
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly Enyim.Caching.MemcachedClient Enyim = new Enyim.Caching.MemcachedClient();

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool Set(string key, object value)
        {
            bool result = true;
            //Enyim.Caching.MemcachedClient enyim = null;
            try
            {
                //enyim = new Enyim.Caching.MemcachedClient();
                Enyim.Store(StoreMode.Set, key, value);
            }catch(Exception ex)
            {
                Logger.Error(ex);
                result= false;
            }

            return result;
        }
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static object Get(string key)
        {
            try
            {
                //enyim = new Enyim.Caching.MemcachedClient();
                Enyim.Get(key);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
		/// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            bool result = true;
            try
            {
                Enyim.Remove(key);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result = false;
            }
            return result;
        }
		/// <summary>
        /// Flushes all.
        /// </summary>
        /// <returns></returns>
        public static bool FlushAll()
        {
            bool result = true;
            try
            {
                Enyim.FlushAll();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result = false;
            }
            return result;
        }
		/// <summary>
        /// Increments the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delta">The delta.</param>
        /// <returns></returns>
        public static ulong Increment(string key, ulong defaultValue,ulong delta)
        {
            ulong result = 0;
            try
            {
                result = Enyim.Increment(key, defaultValue, delta);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }
		/// <summary>
        /// Decrements the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delta">The delta.</param>
        /// <returns></returns>
        public static ulong Decrement(string key, ulong defaultValue, ulong delta)
        {
            ulong result = 0;
            try
            {
                result = Enyim.Decrement(key, defaultValue, delta);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }
    }
}
