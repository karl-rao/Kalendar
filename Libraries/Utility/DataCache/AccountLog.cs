using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;


namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// AccountLog=用户日志数据缓存类
    /// </summary>
    public class AccountLog
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.AccountLogPO> CacheList(int accountId)
        {
            var condition = $"Valid=1 AND AccountId={accountId}";
            return new Common.CacheHelper<DB.Entity.Base.AccountLogPO>().Find(condition, false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.AccountLogPO> InitCache(int accountId)
        {
            var condition = $"Valid=1 AND AccountId={accountId}";
            return new Common.CacheHelper<DB.Entity.Base.AccountLogPO>().Find(condition, true, true);
        }

        #endregion

    }
}
