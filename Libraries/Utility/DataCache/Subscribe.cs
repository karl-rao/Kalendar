using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// Subscribe=订阅数据缓存类
    /// </summary>
    public class Subscribe
    {
        private const string CacheName = Config.CacheKeyPrefix + ".Base.Subscribe";
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.SubscribePO> CacheList(int accountId,int projectId)
        {
            var condition = " Valid=1 ";
            if (accountId > 0)
                condition += string.Format(" AND AccountId={0} ", accountId);
            if (projectId > 0)
                condition += string.Format(" AND ProjectId={0} ", projectId);

            return new Common.CacheHelper<DB.Entity.Base.SubscribePO>().Find(condition, false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.SubscribePO> InitCache(int accountId, int projectId)
        {
            var condition = " Valid=1 ";
            if (accountId > 0)
                condition += string.Format(" AND AccountId={0} ", accountId);
            if (projectId > 0)
                condition += string.Format(" AND ProjectId={0} ", projectId);

            return new Common.CacheHelper<DB.Entity.Base.SubscribePO>().Find(condition, true, true);
        }
        
        #endregion

    }
}
