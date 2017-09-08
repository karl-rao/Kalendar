using System.Collections.Generic;
using Kalendar.Zero.DB.Entity.Base;
using Kalendar.Zero.Utility.Common;


namespace Kalendar.Zero.Data.Domain
{
    /// <summary>
    /// 项目后台实体类，继承库实体(DB.Entity.Base)
    /// </summary>
    public class Project:DB.Entity.Base.ProjectPO
    {
        public List<DB.Entity.Base.SchedulePO> Schedules { get; set; }

        public Project()
        {
            Schedules=new List<SchedulePO>();
        }

        public Project(int id)
        {
            var entity = Utility.DataCache.Project.GetEntity(id);
            if (entity != null)
            {
                this.FillFrom(entity);
                Schedules = Utility.DataCache.Schedule.CacheList(entity.Id);
            }
            else
            {
                Schedules=new List<SchedulePO>();
            }
        }

        public Project(DB.Entity.Base.ProjectPO entity)
        {
            if (entity != null)
            {
                this.FillFrom(entity);
                Schedules = Utility.DataCache.Schedule.CacheList(entity.Id);
            }
            else
            {
                Schedules = new List<SchedulePO>();
            }
        }
    }
}