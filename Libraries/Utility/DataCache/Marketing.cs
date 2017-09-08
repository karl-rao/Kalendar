using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// Marketing=推广渠道数据缓存类
    /// </summary>
    public class Marketing
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.MarketingPO> CacheList()
        {
            return new Common.CacheHelper<DB.Entity.Base.MarketingPO>().Find("1=1", false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.MarketingPO> InitCache()
        {
            return new Common.CacheHelper<DB.Entity.Base.MarketingPO>().Find("1=1", true, true);
        }

        /// <summary>
        /// 获取指定实例
        /// </summary>
        /// <param name="keyValue">The keyValue.</param>
        /// <returns></returns>
        public static DB.Entity.Base.MarketingPO GetEntity(object keyValue)
        {
            return CacheList().FindLast(o => (o.Id + "") == keyValue + "");
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <param name="keyValue">The obbject keyValue.</param>
        /// <returns></returns>
        public static string GetLabelName(object keyValue)
        {
           var entity = GetEntity(keyValue);
		   return entity != null ? entity.MarketingName   : "";
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
                return list.Select(x => new SelectListItem
                                            {
                                                Value = x.Id + "",
                                                Text = x.MarketingName
                                            }).ToList();
            }
        }
        
    }
}
