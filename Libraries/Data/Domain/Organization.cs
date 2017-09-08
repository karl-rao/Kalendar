using System.Collections.Generic;
using Kalendar.Zero.DB.Entity.Base;

namespace Kalendar.Zero.Data.Domain
{
    /// <summary>
    /// 组织后台实体类，继承库实体(DB.Entity.Base)
    /// </summary>
    public class Organization:DB.Entity.Base.OrganizationPO
    {

        /// <summary>
        /// 组织关系(成员间)
        /// </summary>
        public List<OrganizationMemberPO> OrganizationRelations { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public List<OrganizationDepartmentPO> Departments { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public List<AccountPO> Accounts { get; set; } 

        public Organization()
        {
            OrganizationRelations=new List<OrganizationMemberPO>();
            Departments=new List<OrganizationDepartmentPO>();
        }


    }
}