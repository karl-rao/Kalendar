using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;


namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// AccountMessages=用户消息数据缓存类
    /// </summary>
    public class AccountMessages
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.AccountMessagesPO> CacheList(int accountId)
        {
            var condition = $"Valid=1 AND ToAccountId={accountId}";
            return new Common.CacheHelper<DB.Entity.Base.AccountMessagesPO>().Find(condition, false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.AccountMessagesPO> InitCache(int accountId)
        {
            var condition = $"Valid=1 AND ToAccountId={accountId}";
            return new Common.CacheHelper<DB.Entity.Base.AccountMessagesPO>().Find(condition, false, true);
        }

        #endregion

    }
}
