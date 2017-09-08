using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// Channel=渠道数据缓存类
    /// </summary>
    public class Channel
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.ChannelPO> CacheList()
        {
            return new Common.CacheHelper<DB.Entity.Base.ChannelPO>().Find("1=1", false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.ChannelPO> InitCache()
        {
            return new Common.CacheHelper<DB.Entity.Base.ChannelPO>().Find("1=1", true, true);
        }

        /// <summary>
        /// 获取指定实例
        /// </summary>
        /// <param name="keyValue">The keyValue.</param>
        /// <returns></returns>
        public static DB.Entity.Base.ChannelPO GetEntity(object keyValue)
        {
            return CacheList().FindLast(o => (o.Id + "") == keyValue + "");
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <param name="keyValue">The obbject keyValue.</param>
        /// <returns></returns>
        public static string GetChannelName(object keyValue)
        {
           var entity = GetEntity(keyValue);
		   return entity != null ? entity.ChannelName   : "";
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
                                                Text = x.ChannelName
                                            }).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<SelectListItem> ChannelSymbolOptions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem {Value = "1", Text = "Outlook"},
                    new SelectListItem {Value = "2", Text = "Google"}
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetChannelSymbol(object value)
        {
            var option = ChannelSymbolOptions.FindLast(o => o.Value == (value + ""));
            return option != null ? option.Text : "";
        }
    }
}
