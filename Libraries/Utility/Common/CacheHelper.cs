using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Kalendar.Zero.DB.Agent;
using Kalendar.Zero.DB.Entity.Ext;

namespace Kalendar.Zero.Utility.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CacheHelper<T> where T: Kalendar.Zero.DB.Entity.Base.BaseEntity,new()
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="forceRefresh"></param>
        /// <param name="createCache"></param>
        /// <returns></returns>
        public T FindById(object keyValue,bool forceRefresh=false,bool createCache=true)
        {
            T data=null;
            var cacheKey = string.Format(Config.CacheKey, keyValue) + typeof(T);

            var obj = Cache.Get<T>(cacheKey);
            if (obj == null||forceRefresh)
            {
                var trans = new Transaction();
                try
                {
                    trans.Begin();
                    var dbHelper = new MssqlHelper<T>();
                    data = dbHelper.FindById(keyValue, trans.DbConnection, trans.DbTrans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.RollBack();
                    Logger.Error(ex);
                }
                finally
                {
                    trans.Dispose();
                }

                if (data != null&& createCache)
                    Cache.Insert(cacheKey, data);
            }
            else
            {
                data = obj;
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="forceRefresh"></param>
        /// <param name="createCache"></param>
        /// <returns></returns>
        public List<T> Find(
            string condition, 
            bool forceRefresh = false,
            bool createCache = false)
        {
            var data=new List<T>();
            var cacheKey = string.Format(Config.CacheKey, condition.Md5()) + typeof(T);

            var obj = Cache.Get<List<T>>(cacheKey);
            if (obj == null || forceRefresh)
            {
                var trans = new Transaction();
                try
                {
                    trans.Begin();
                    var dbHelper = new MssqlHelper<T>();
                    data = dbHelper.Find(condition, trans.DbConnection, trans.DbTrans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.RollBack();
                    Logger.Error(ex);
                }
                finally
                {
                    trans.Dispose();
                }

                if (data != null&& createCache)
                    Cache.Insert(cacheKey, data);
            }
            else
            {
                data = obj;
            }
            return data;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderField"></param>
        /// <param name="isDescending"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="forceRefresh"></param>
        /// <param name="createCache"></param>
        /// <returns></returns>
        public StructSet<T> GetStructSet(
            string condition,
            string orderField,
            bool isDescending,
            int pageSize,
            int pageNo,
            bool forceRefresh = false,
            bool createCache = false)
        {
            var data = new StructSet<T>();
            var cacheKey = string.Format(Config.CacheKey, (condition+"."+pageSize+"."+pageNo).Md5()) + typeof(T);

            var obj = Cache.Get<StructSet<T>>(cacheKey);
            if (obj == null || forceRefresh)
            {
                var trans = new Transaction();
                try
                {
                    trans.Begin();
                    var dbHelper = new MssqlHelper<T>();
                    data = dbHelper.GetStructSet(condition,null,orderField,isDescending,pageSize,pageNo, trans.DbConnection, trans.DbTrans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.RollBack();
                    Logger.Error(ex);
                }
                finally
                {
                    trans.Dispose();
                }

                if (data != null && createCache)
                    Cache.Insert(cacheKey, data);
            }
            else
            {
                data = obj;
            }
            return data;
        }

    }
}
