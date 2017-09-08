using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// Tag=标签数据缓存类
    /// </summary>
    public class Tag
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.TagPO> CacheList()
        {
            return new Common.CacheHelper<DB.Entity.Base.TagPO>().Find("1=1", false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.TagPO> InitCache()
        {
            return new Common.CacheHelper<DB.Entity.Base.TagPO>().Find("1=1", true, true);
        }

        /// <summary>
        /// 获取指定实例
        /// </summary>
        /// <param name="keyValue">The keyValue.</param>
        /// <returns></returns>
        public static DB.Entity.Base.TagPO GetEntity(object keyValue)
        {
            return CacheList().FindLast(o => (o.Id + "") == keyValue + "");
        }

        #endregion

    }
}
