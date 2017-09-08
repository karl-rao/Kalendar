using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;


namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// AccountNote=用户笔记数据缓存类
    /// </summary>
    public class AccountNote
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.AccountNotePO> CacheList(int accountId)
        {
            var condition = $"Valid=1 AND AccountId={accountId}";
            return new Common.CacheHelper<DB.Entity.Base.AccountNotePO>().Find(condition, false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.AccountNotePO> InitCache(int accountId)
        {
            var condition = $"Valid=1 AND AccountId={accountId}";
            return new Common.CacheHelper<DB.Entity.Base.AccountNotePO>().Find(condition, true, true);
        }

        #endregion

    }
}
