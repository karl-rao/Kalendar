using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// OrganizationMember=组织成员数据缓存类
    /// </summary>
    public class OrganizationMember
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.OrganizationMemberPO> CacheList(int organizationId, int accountId)
        {
            var condition = " Valid=1 ";
            if (accountId > 0)
                condition += string.Format(" AND AccountId={0} ", accountId);
            if (organizationId > 0)
                condition += string.Format(" AND OrganizationId={0} ", organizationId);

            return new Common.CacheHelper<DB.Entity.Base.OrganizationMemberPO>().Find(condition, false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.OrganizationMemberPO> InitCache(int organizationId, int accountId)
        {
            var condition = " Valid=1 ";
            if (accountId > 0)
                condition += string.Format(" AND AccountId={0} ", accountId);
            if (organizationId > 0)
                condition += string.Format(" AND OrganizationId={0} ", organizationId);

            return new Common.CacheHelper<DB.Entity.Base.OrganizationMemberPO>().Find(condition, false, true);
        }
        
        #endregion
        
    }
}
