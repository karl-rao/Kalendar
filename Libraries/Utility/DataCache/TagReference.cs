using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// TagReference=标签关联数据缓存类
    /// </summary>
    public class TagReference
    {
        private const string CacheName = Config.CacheKeyPrefix + ".Base.TagReference";
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.TagReferencePO> CacheList(int tagId, int projectId)
        {
            var condition = " Valid=1 ";
            if (tagId > 0)
                condition += string.Format(" AND TagId={0} ", tagId);
            if (projectId > 0)
                condition += string.Format(" AND ProjectId={0} ", projectId);

            return new Common.CacheHelper<DB.Entity.Base.TagReferencePO>().Find(condition, false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.TagReferencePO> InitCache(int tagId, int projectId)
        {
            var condition = " Valid=1 ";
            if (tagId > 0)
                condition += string.Format(" AND TagId={0} ", tagId);
            if (projectId > 0)
                condition += string.Format(" AND ProjectId={0} ", projectId);

            return new Common.CacheHelper<DB.Entity.Base.TagReferencePO>().Find(condition, true, true);
        }

        #endregion

    }
}
