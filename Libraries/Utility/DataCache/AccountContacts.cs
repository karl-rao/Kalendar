using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;


namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// AccountContacts=用户联系人数据缓存类
    /// </summary>
    public class AccountContacts
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.AccountContactsPO> CacheList(int accountId)
        {
            var condition = $"Valid=1 AND AccountId={accountId}";
            return new Common.CacheHelper<DB.Entity.Base.AccountContactsPO>().Find(condition, false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.AccountContactsPO> InitCache(int accountId)
        {
            var condition = $"Valid=1 AND AccountId={accountId}";
            return new Common.CacheHelper<DB.Entity.Base.AccountContactsPO>().Find(condition, true, true);
        }

        #endregion

    }
}
