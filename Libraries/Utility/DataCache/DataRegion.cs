using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// DataRegion=地区数据缓存类
    /// </summary>
    public class DataRegion
    {
        #region BasicCache
        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.DataRegionPO> CacheList()
        {
            return new Common.CacheHelper<DB.Entity.Base.DataRegionPO>().Find("1=1", false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.DataRegionPO> InitCache()
        {
            return new Common.CacheHelper<DB.Entity.Base.DataRegionPO>().Find("1=1", true, true);
        }

        /// <summary>
        /// 获取指定实例
        /// </summary>
        /// <param name="keyValue">The keyValue.</param>
        /// <returns></returns>
        public static DB.Entity.Base.DataRegionPO GetEntity(object keyValue)
        {
            return CacheList().FindLast(o => (o.Id + "") == keyValue + "");
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <param name="keyValue">The obbject keyValue.</param>
        /// <returns></returns>
        public static string GetRegionName(object keyValue)
        {
           var entity = GetEntity(keyValue);
		   return entity != null ? entity.RegionName   : "";
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
                var regions = new List<SelectListItem>();

                AppendRegionsTree(list, regions, 0, 1);

                return regions;
            }
        }

        /// <summary>
        /// 递归添加地区树
        /// </summary>
        /// <param name="oriList"></param>
        /// <param name="newList"></param>
        /// <param name="parentId"></param>
        /// <param name="depth"></param>
        private static void AppendRegionsTree(
            List<DB.Entity.Base.DataRegionPO> oriList,
            List<SelectListItem> newList,
            int parentId,
            int depth)
        {
            var rs = oriList.FindAll(o => o.ParentId == parentId).OrderBy(o => o.DisplayRank)
                .ThenBy(o => o.RegionName);
            foreach (var regionPO in rs)
            {
                var txt = "";
                for (int i = 0; i < depth; i++)
                {
                    txt += "　";
                }
                txt += regionPO.RegionName;
                newList.Add(new SelectListItem { Text = txt, Value = regionPO.Id + "" });

                AppendRegionsTree(oriList, newList, regionPO.Id, depth + 1);
            }
        }

        /// <summary>
        /// 可选项(加空)
        /// </summary>
        public static List<SelectListItem> OptionsWithNull
        {
            get
            {
                var list = Options;
                list.Insert(0, new SelectListItem { Text = "请选择地区", Value = "0" });
                return list;
            }
        }

        /// <summary>
        /// 获取地区路径
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public static string RenderHierarchy(int regionId)
        {
            var result = "";
            var entity = GetEntity(regionId);
            if (entity != null)
            {
                while (entity != null)
                {
                    result = "'" + entity.RegionName + "'"
                             + (result == "" ? "" : ",")
                             + result;

                    entity = GetEntity(entity.ParentId);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取地区下拉层级预选值
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public static string RenderHierarchyPreselect(int regionId)
        {
            var result = "";
            var entity = GetEntity(regionId);
            if (entity != null)
            {
                while (entity != null)
                {
                    result = "'" + entity.RegionName + "'"
                             + (result == "" ? "" : ":")
                             + result;

                    entity = GetEntity(entity.ParentId);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取地区路径
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public static string RenderHierarchyShow(int regionId)
        {
            var result = "";
            var entity = GetEntity(regionId);
            if (entity != null)
            {
                while (entity != null)
                {
                    result = entity.RegionName
                             + (result == "" ? "" : ",")
                             + result;

                    entity = GetEntity(entity.ParentId);
                }
            }
            return result;
        }

        #region Children

        /// <summary>
        /// 获取所有子地区
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<DB.Entity.Base.DataRegionPO> GetChildren(int id)
        {
            var list = CacheList().FindAll(o => o.Valid);
            //.ToList();
            var children = new List<DB.Entity.Base.DataRegionPO>();

            AppendChildren(list, children, id);

            return children;
        }

        /// <summary>
        /// 递归获取子地区
        /// </summary>
        /// <param name="oriList"></param>
        /// <param name="newList"></param>
        /// <param name="parentId"></param>
        private static void AppendChildren(
            List<DB.Entity.Base.DataRegionPO> oriList,
            List<DB.Entity.Base.DataRegionPO> newList,
            int parentId)
        {
            var rs = oriList.FindAll(o => o.ParentId == parentId).OrderBy(o => o.DisplayRank)
                .ThenBy(o => o.RegionName);
            foreach (var c in rs)
            {
                newList.Add(c);

                AppendChildren(oriList, newList, c.Id);
            }
        }

        /// <summary>
        /// 获取指定地区及子地区的主键列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<int> GetChildrenRegionIds(int id)
        {
            var result = new List<int> { id };

            var children = GetChildren(id);
            foreach (var dataRegionPO in children)
            {
                if (result.Contains(dataRegionPO.Id))
                    result.Add(dataRegionPO.Id);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetChildrenRegionIdstring(int id)
        {
            var l = GetChildrenRegionIds(id);

            var result = l.Aggregate("", (current, i) => current + (i + ",")) + "0";

            return result;
        }

        #endregion

        #region Json

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string Json()
        {
            var regions = CacheList().FindAll(o => o.Valid && o.Applied).OrderBy(o => o.DisplayRank).ThenBy(o => o.RegionName).ToList();

            var sb = new StringBuilder();
            sb.Append("var regions={");

            RenderRegionTree(regions, sb, 0);

            sb.Append("};");

            return sb.ToString();
        }

        /// <summary>
        /// 输出地区数据树
        /// </summary>
        /// <param name="regions"></param>
        /// <param name="sb"></param>
        /// <param name="parentId"></param>
        private static void RenderRegionTree(List<DB.Entity.Base.DataRegionPO> regions, StringBuilder sb, int parentId)
        {
            var children = regions.FindAll(o => o.ParentId == parentId);
            for (int i = 0; i < children.Count; i++)
            {
                var r = children[i];

                sb.Append(string.Format("\"{0}\":", r.RegionName));
                if (regions.Any(o => o.ParentId == r.Id))
                {
                    sb.Append("{");
                    RenderRegionTree(regions, sb, r.Id);
                    sb.Append("}");
                }
                else
                {
                    sb.Append(string.Format("\"{0}\"", r.Id));
                }

                if (i < children.Count - 1)
                    sb.Append(",");
            }
        }

        #endregion

    }
}
