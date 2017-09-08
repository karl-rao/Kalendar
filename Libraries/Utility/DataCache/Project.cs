using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// Project=项目数据缓存类
    /// </summary>
    public class Project
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.ProjectPO> CacheList()
        {
            return new Common.CacheHelper<DB.Entity.Base.ProjectPO>().Find("1=1", false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.ProjectPO> InitCache()
        {
            return new Common.CacheHelper<DB.Entity.Base.ProjectPO>().Find("1=1", true, true);
        }

        /// <summary>
        /// 获取指定实例
        /// </summary>
        /// <param name="keyValue">The keyValue.</param>
        /// <returns></returns>
        public static DB.Entity.Base.ProjectPO GetEntity(object keyValue)
        {
            return CacheList().FindLast(o => (o.Id + "") == keyValue + "");
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <param name="keyValue">The obbject keyValue.</param>
        /// <returns></returns>
        public static string GetProjectName(object keyValue)
        {
           var entity = GetEntity(keyValue);
		   return entity != null ? entity.ProjectName   : "";
        }

        #endregion
        
		/// <summary>
        /// 可选项
		/// DropDownList DataSource
        /// </summary>
		public static List<SelectListItem> Options
        {
            get
            {
                var list = CacheList().FindAll(o => o.Valid);
								//.ToList();
                var options= list.Select(x => new SelectListItem
                                            {
                                                Value = x.Id + "",
                                                Text = x.ProjectName
                                            }).ToList();

                options.Insert(0,new SelectListItem {Value = "0",Text = "---------------"});
                return options;
            }
        }
    }
}
