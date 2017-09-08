using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// DataLanguage=语种数据缓存类
    /// </summary>
    public class DataLanguage
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.DataLanguagePO> CacheList()
        {
            return new Common.CacheHelper<DB.Entity.Base.DataLanguagePO>().Find("1=1", false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.DataLanguagePO> InitCache()
        {
            return new Common.CacheHelper<DB.Entity.Base.DataLanguagePO>().Find("1=1", true, true);
        }

        /// <summary>
        /// 获取指定实例
        /// </summary>
        /// <param name="keyValue">The keyValue.</param>
        /// <returns></returns>
        public static DB.Entity.Base.DataLanguagePO GetEntity(object keyValue)
        {
            return CacheList().FindLast(o => (o.Id + "") == keyValue + "");
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <param name="keyValue">The obbject keyValue.</param>
        /// <returns></returns>
        public static string GetLanguageName(object keyValue)
        {
           var entity = GetEntity(keyValue);
		   return entity != null ? entity.LanguageName   : "";
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
                                                Text = x.LanguageName
                                            }).ToList();
            }
        }
    }
}
