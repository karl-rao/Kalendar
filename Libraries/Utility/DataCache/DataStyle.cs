using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// DataStyle=风格数据缓存类
    /// </summary>
    public class DataStyle
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.DataStylePO> CacheList(int accountId)
        {
            return new Common.CacheHelper<DB.Entity.Base.DataStylePO>().Find("1=1", false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.DataStylePO> InitCache(int accountId)
        {
            return new Common.CacheHelper<DB.Entity.Base.DataStylePO>().Find("1=1", true, true);
        }

        #endregion

        /// <summary>
        /// 可选项
        /// DropDownList DataSource
        /// </summary>
        public static List<SelectListItem> Options(int accountId)
        {
            var list = CacheList(accountId).FindAll(o => o.Valid);
            //.ToList();
            var options = list.Select(x => new SelectListItem
            {
                Value = x.Id + "",
                Text = x.StyleName
            }).ToList();

            options.Insert(0, new SelectListItem { Value = "0", Text = "---------------" });
            return options;
        }
    }
}
