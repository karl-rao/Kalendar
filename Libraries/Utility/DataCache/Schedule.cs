using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kalendar.Zero.DB.Entity.Base;



namespace Kalendar.Zero.Utility.DataCache
{
    /// <summary>
    /// Schedule=计划数据缓存类
    /// </summary>
    public class Schedule
    {
        #region BasicCache
        /// <summary>
        /// 计数
        /// </summary>
        /// <returns></returns>
        public static int CountAll()
        {
            var allProject = Project.CacheList();

            return allProject.Sum(i => CacheList(i.Id).Count);
        }

        /// <summary>
        /// Datas the Colection.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.SchedulePO> CacheList(int projectId)
        {
            if (projectId == 0)
                return new List<SchedulePO>();

            var condition = string.Format("Valid=1 AND ProjectId={0}", projectId);
            return new Common.CacheHelper<DB.Entity.Base.SchedulePO>().Find(condition, false, true);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns></returns>
        public static List<DB.Entity.Base.SchedulePO> InitCache(int projectId)
        {
            var condition = string.Format("Valid=1 AND ProjectId={0}", projectId);
            return new Common.CacheHelper<DB.Entity.Base.SchedulePO>().Find(condition, false, true);
        }
        
        #endregion

        /// <summary>
        /// Planned选项
        /// </summary>
        public static List<SelectListItem> PlannedOptions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem {Value = "0", Text = "未确认"},
                    new SelectListItem {Value = "1", Text = "确认中"},
                    new SelectListItem {Value = "2", Text = "已确认"}
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int GetMsonlineCycle(string val)
        {
            var result = 0;

            switch (val)
            {
                case "daily":
                    result = 1;
                    break;
                case "weekly":
                    result = 2;
                    break;
                case "absoluteMonthly":
                    result = 5;
                    break;
                case "absoluteYearly":
                    result = 6;
                    break;
            }

            return result;
        }

        /// <summary>
        /// 周期选项
        /// </summary>
        public static List<SelectListItem> CycleOptions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem {Value = "0",Text="一次性"},
                    new SelectListItem {Value = "1",Text="每天"},
                    new SelectListItem {Value = "2",Text="每周"},
                    new SelectListItem {Value = "3",Text="每旬"},
                    new SelectListItem {Value = "4",Text="每半月"},
                    new SelectListItem {Value = "5",Text="每月"},
                    new SelectListItem {Value = "6",Text="每年"},
                };
            }
        }

        /// <summary>
        /// 获取日期设置描述
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetDescription(DB.Entity.Base.SchedulePO entity)
        {
            var result = "";

            //result += Karlrao.Utility.WebHelper.Config.GetTextFromListItem(
            //    CycleOptions, entity.Cycle);

            switch (entity.Cycle)
            {
                case 0:
                    #region 0
                    result += entity.BeginDate.ToString("yyyy-MM-dd");
                    if (entity.BeginTime.ToString("HH:mm:ss") != "00:00:00")
                    {
                        result += " " + entity.BeginTime.ToString("HH:mm:ss");
                    }
                    break;
                #endregion

                case 1:
                    #region 1
                    if (entity.BeginTime.ToString("HH:mm:ss") != "00:00:00")
                    {
                        result +=" "+ entity.BeginTime.ToString("HH:mm:ss");
                    }
                    break;
                #endregion

                case 2:
                    #region 2

                    if (entity.BeginDay >= 0)
                        result += entity.BeginDay;
                    if (entity.EndDay > 0)
                        result += "~" + entity.EndDay;
                    break;

                #endregion
                    
                case 3:
                    #region 3

                    if (entity.BeginDay >= 0)
                        result += entity.BeginDay;
                    if (entity.EndDay > 0)
                        result += "~" + entity.EndDay;
                    break;

                #endregion

                case 4:
                    #region 4

                    if (entity.BeginDay >= 0)
                        result += entity.BeginDay;
                    if (entity.EndDay > 0)
                        result += "~" + entity.EndDay;
                    break;

                #endregion
                    
                case 5:
                    #region 5

                    if (entity.BeginDay >= 0)
                        result += entity.BeginDay;
                    if (entity.EndDay > 0)
                        result += "~" + entity.EndDay;
                    break;

                #endregion

                case 6:
                    #region 6
                    
                    result += entity.BeginDay.ToString("MM-dd");
                    if (entity.EndDate.ToString("MM-dd") != "00-00")
                        result += "~" + entity.EndDate.ToString("MM-dd");
                    break;

                    #endregion

            }

            return result;
        }

    }
}
