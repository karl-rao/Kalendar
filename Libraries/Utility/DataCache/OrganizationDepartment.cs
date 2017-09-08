using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// OrganizationDepartment=组织成员数据缓存类
    /// </summary>
    public class OrganizationDepartment
    {
        private const string CacheName = Config.CacheKeyPrefix + ".Base.OrganizationDepartment";
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.OrganizationDepartmentPO> CacheList(int organizationId)
        {
            var condition = " Valid=1 ";
            if (organizationId > 0)
                condition += string.Format(" AND OrganizationId={0} ", organizationId);
            return new Common.CacheHelper<DB.Entity.Base.OrganizationDepartmentPO>().Find(condition, false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.OrganizationDepartmentPO> InitCache(int organizationId)
        {
            var condition = " Valid=1 ";
            if (organizationId > 0)
                condition += string.Format(" AND OrganizationId={0} ", organizationId);
            return new Common.CacheHelper<DB.Entity.Base.OrganizationDepartmentPO>().Find(condition, true, true);
        }
        
        #endregion
        
    }
}
