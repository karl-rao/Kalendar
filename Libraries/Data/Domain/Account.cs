using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Kalendar.Zero.Data.Entities;
using Kalendar.Zero.DB.Entity.Base;
using Kalendar.Zero.Utility.Common;


namespace Kalendar.Zero.Data.Domain
{
    /// <summary>
    /// 用户后台实体类，继承库实体(DB.Entity.Base)
    /// </summary>
    public class Account:DB.Entity.Base.AccountPO
    {
        public Account()
        {
            LoadData(null);
        }

        public Account(int accountId)
        {
            var entity = Utility.DataCache.Account.GetEntity(accountId);
            LoadData(entity);
        }

        public Account(AccountPO entity)
        {
            LoadData(entity);
        }

        private void LoadData(AccountPO entity)
        {
            if (entity != null)
            {
                this.FillFrom(entity);

                Avatars = Utility.DataCache.AccountAvatars.CacheList(entity.Id);
                Contacts = Utility.DataCache.AccountContacts.CacheList(entity.Id);
                Logs = Utility.DataCache.AccountLog.CacheList(entity.Id);
                Messages = Utility.DataCache.AccountMessages.CacheList(entity.Id);
                Notes = Utility.DataCache.AccountNote.CacheList(entity.Id);
                Relations = Utility.DataCache.AccountRelation.CacheList(entity.Id);

                Styles = Utility.DataCache.DataStyle.CacheList(entity.Id);

                OrganizationRelations = Utility.DataCache.OrganizationMember.CacheList(0,entity.Id);

                Organizations = Utility.DataCache.Organization.CacheList()
                    .FindAll(o => OrganizationRelations.Any(x => x.OrganizationId == o.Id));

                Subscribes = Utility.DataCache.Subscribe.CacheList(entity.Id, 0);

                Projects = Utility.DataCache.Project.CacheList()
                    .FindAll(o =>o.IsPublic||o.CreatorAccountId==entity.Id|| Subscribes.Any(x => x.ProjectId == o.Id));
                Schedules = new List<SchedulePO>();
                foreach (var projectPo in Projects)
                {
                    Schedules.AddRange(Utility.DataCache.Schedule.CacheList(projectPo.Id));
                }
            }
            else
            {
                Avatars=new List<AccountAvatarsPO>();
                Contacts=new List<AccountContactsPO>();
                Logs = new List<AccountLogPO>();
                Messages=new List<AccountMessagesPO>();
                Notes = new List<AccountNotePO>();
                Relations = new List<AccountRelationPO>();

                Styles = new List<DataStylePO>();

                OrganizationRelations = new List<OrganizationMemberPO>();
                Organizations = new List<OrganizationPO>();
                Projects = new List<ProjectPO>();
                Schedules = new List<SchedulePO>();
                Subscribes = new List<SubscribePO>();
            }
        }

        #region BaseEntities

        /// <summary>
        /// 用户日志数据
        /// </summary>
        public List<AccountLogPO> Logs { get; set; }  

        /// <summary>
        /// 用户笔记数据
        /// </summary>
         public List<AccountNotePO> Notes { get; set; }

        /// <summary>
        /// 用户关系数据
        /// </summary>
        public List<AccountRelationPO> Relations { get; set; }

        /// <summary>
        /// 分身
        /// </summary>
        public List<AccountAvatarsPO> Avatars { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<AccountContactsPO> Contacts { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public List<AccountMessagesPO> Messages { get; set; }



        /// <summary>
        /// 用户样式
        /// </summary>
        public List<DataStylePO> Styles { get; set; } 

        /// <summary>
        /// 组织关系(成员间)
        /// </summary>
        public List<OrganizationMemberPO> OrganizationRelations { get; set; }

        /// <summary>
        /// 参与组织
        /// </summary>
        public List<OrganizationPO> Organizations { get; set; }

        /// <summary>
        /// 订阅
        /// </summary>
         public List<SubscribePO> Subscribes { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public List<ProjectPO> Projects { get; set; }

        /// <summary>
        /// 计划
        /// </summary>
        public List<SchedulePO> Schedules { get; set; }

        #endregion

        #region 输出

        public object CurrentData { get; set; }

        public string ViewProjectIds { get; set; }

        public Data.Entities.KalendarEvents Events
        {
            get
            {
                var data = new Data.Entities.KalendarEvents
                {
                    Success = 1,
                    Result = new List<Data.Entities.KalendarEvent>()
                };

                if (!string.IsNullOrEmpty(ViewProjectIds))
                {
                    if (Id == 0)
                    {
                        Projects = new List<ProjectPO>();
                    }
                    var ids = ViewProjectIds.Split(',');
                    foreach (var id in ids)
                    {
                        if (!Projects.Any(o => o.Id + "" == id))
                        {
                            var p = Utility.DataCache.Project.GetEntity(id);
                            if (p != null)
                                Projects.Add(p);
                        }
                    }
                    Schedules = new List<SchedulePO>();
                    foreach (var projectPo in Projects)
                    {
                        Schedules.AddRange(Utility.DataCache.Schedule.CacheList(projectPo.Id));
                    }
                }

                foreach (var schedulePo in Schedules)
                {
                    var durations = GetDurations(schedulePo);
                    foreach (var timeDuration in durations)
                    {
                        data.Result.Add(new KalendarEvent
                        {
                            EventId = schedulePo.Id,
                            EventClass = GetClass(schedulePo),
                            EventClassName = GetClass(schedulePo),
                            EventTitle = GetTitle(schedulePo),
                            TimeText =
                                timeDuration.Begin.Value.ToLocalTime().ToString("HH:mm") + " ~ " +
                                timeDuration.End.Value.ToLocalTime().ToString("HH:mm"),
                            EventStart =
                                timeDuration.Begin.Value.ToLocalTime()
                                    .ToString("yyyy-MM-ddTHH:mm:sszzzz", DateTimeFormatInfo.InvariantInfo),
                            EventEnd =
                                timeDuration.End.Value.ToLocalTime()
                                    .ToString("yyyy-MM-ddTHH:mm:sszzzz", DateTimeFormatInfo.InvariantInfo)
                        });
                    }
                }

                return data;
            }
        }

        #endregion

        #region 私有方法

        private string GetClass(SchedulePO schedule)
        {
            var css = "";

            var project = Projects.FindLast(o => o.Id == schedule.ProjectId);
            if (project != null)
            {
                var style = Styles.FindLast(o => o.Id == project.DataStyleId);
                if (style != null)
                {
                    css = "klds_" + style.Id;
                }
            }

            return css;
        }

        private string GetTitle(SchedulePO schedule)
        {
            var note = Notes.FindLast(o => o.ScheduleId == schedule.Id);
            return note != null 
                ? note.Notes 
                :string.IsNullOrEmpty( schedule.ScheduleTitle)?Utility.DataCache.Project.GetProjectName(schedule.ProjectId):schedule.ScheduleTitle;
        }

        private List<TimeDuration> GetDurations(SchedulePO schedule)
        {
            var subscribe = Subscribes.FindLast(o => o.ProjectId == schedule.ProjectId);
            var planStart = subscribe == null ? DateTime.Now : subscribe.CreateTime;
            var planEnd = planStart.AddYears(1);

            var data = new List<TimeDuration>();

            switch (schedule.Cycle)
            {
                case 0://一次
                    data.Add(new TimeDuration
                    {
                        Begin = new DateTime(
                            schedule.BeginDate.Year,
                            schedule.BeginDate.Month,
                            schedule.BeginDate.Day,
                            schedule.BeginTime.Hour,
                            schedule.BeginTime.Minute,
                            schedule.BeginTime.Second
                            ),
                        End = new DateTime(
                            schedule.EndDate.Year,
                            schedule.EndDate.Month,
                            schedule.EndDate.Day,
                            schedule.EndTime.Hour,
                            schedule.EndTime.Minute,
                            schedule.EndTime.Second
                            )
                    });
                    break;

                case 1://每天
                    var currentDay1 = planStart;
                    while (currentDay1 <= planEnd)
                    {
                        data.Add(new TimeDuration
                        {
                            Begin = new DateTime(
                                currentDay1.Year,
                                currentDay1.Month,
                                currentDay1.Day,
                            schedule.BeginTime.Hour,
                            schedule.BeginTime.Minute,
                            schedule.BeginTime.Second
                                ),
                            End = new DateTime(
                                currentDay1.Year,
                                currentDay1.Month,
                                currentDay1.Day,
                            schedule.EndTime.Hour,
                            schedule.EndTime.Minute,
                            schedule.EndTime.Second
                                )
                        });

                        currentDay1 = currentDay1.AddDays(1);
                    }
                    break;

                case 2:
                    var currentDay2 = planStart;
                    while (currentDay2 <= planEnd)
                    {
                        var appendDay2 = false;
                        var d2 = new TimeDuration();

                        if ((int)currentDay2.DayOfWeek == schedule.BeginDay)
                        {
                            d2.Begin = new DateTime(
                                currentDay2.Year,
                                currentDay2.Month,
                                currentDay2.Day,
                            schedule.BeginTime.Hour,
                            schedule.BeginTime.Minute,
                            schedule.BeginTime.Second
                                );
                        }

                        if ((int)currentDay2.DayOfWeek == schedule.EndDay)
                        {
                            appendDay2 = true;
                            d2.End = new DateTime(
                                currentDay2.Year,
                                currentDay2.Month,
                                currentDay2.Day,
                            schedule.EndTime.Hour,
                            schedule.EndTime.Minute,
                            schedule.EndTime.Second
                                );
                        }

                        currentDay2 = currentDay2.AddDays(1);
                        if(appendDay2)
                            data.Add(d2);
                    }

                    break;

                case 3:
                    var currentDay3 = planStart;
                    while (currentDay3 <= planEnd)
                    {
                        var appendDay3 = false;
                        var d3 = new TimeDuration();

                        if (currentDay3.Day % 10 == schedule.BeginDay)
                        {
                            d3.Begin = new DateTime(
                                currentDay3.Year,
                                currentDay3.Month,
                                currentDay3.Day,
                            schedule.BeginTime.Hour,
                            schedule.BeginTime.Minute,
                            schedule.BeginTime.Second
                                );
                        }

                        if (currentDay3.Day % 10 == schedule.EndDay)
                        {
                            appendDay3 = true;
                            d3.End = new DateTime(
                                currentDay3.Year,
                                currentDay3.Month,
                                currentDay3.Day,
                            schedule.EndTime.Hour,
                            schedule.EndTime.Minute,
                            schedule.EndTime.Second
                                );
                        }

                        currentDay3 = currentDay3.AddDays(1);
                        if (appendDay3)
                            data.Add(d3);
                    }
                    break;


                case 4:
                    var currentDay4 = planStart;
                    while (currentDay4 <= planEnd)
                    {
                        var appendDay4 = false;
                        var d4 = new TimeDuration();

                        if (currentDay4.Day % 15 == schedule.BeginDay)
                        {
                            d4.Begin = new DateTime(
                                currentDay4.Year,
                                currentDay4.Month,
                                currentDay4.Day,
                            schedule.BeginTime.Hour,
                            schedule.BeginTime.Minute,
                            schedule.BeginTime.Second
                                );
                        }

                        if (currentDay4.Day % 15 == schedule.EndDay)
                        {
                            appendDay4 = true;
                            d4.End = new DateTime(
                                currentDay4.Year,
                                currentDay4.Month,
                                currentDay4.Day,
                            schedule.EndTime.Hour,
                            schedule.EndTime.Minute,
                            schedule.EndTime.Second
                                );
                        }

                        currentDay4 = currentDay4.AddDays(1);
                        if (appendDay4)
                            data.Add(d4);
                    }
                    break;


                case 5:
                    var currentDay5 = planStart;
                    while (currentDay5 <= planEnd)
                    {
                        var appendDay5 = false;
                        var d5 = new TimeDuration();

                        if (currentDay5.Day == schedule.BeginDay)
                        {
                            d5.Begin = new DateTime(
                                currentDay5.Year,
                                currentDay5.Month,
                                currentDay5.Day,
                            schedule.BeginTime.Hour,
                            schedule.BeginTime.Minute,
                            schedule.BeginTime.Second
                                );
                        }

                        if (currentDay5.Day == schedule.EndDay)
                        {
                            appendDay5 = true;
                            d5.End = new DateTime(
                                currentDay5.Year,
                                currentDay5.Month,
                                currentDay5.Day,
                            schedule.EndTime.Hour,
                            schedule.EndTime.Minute,
                            schedule.EndTime.Second
                                );
                        }

                        currentDay5 = currentDay5.AddDays(1);
                        if (appendDay5)
                            data.Add(d5);
                    }
                    break;


                case 6:
                    var currentDay6 = planStart;
                    while (currentDay6 <= planEnd)
                    {
                        var appendDay6 = false;
                        var d6 = new TimeDuration();

                        if (currentDay6.DayOfYear == schedule.BeginDay)
                        {
                            d6.Begin = new DateTime(
                                currentDay6.Year,
                                currentDay6.Month,
                                currentDay6.Day,
                            schedule.BeginTime.Hour,
                            schedule.BeginTime.Minute,
                            schedule.BeginTime.Second
                                );
                        }

                        if (currentDay6.DayOfYear == schedule.EndDay)
                        {
                            appendDay6 = true;
                            d6.End = new DateTime(
                                currentDay6.Year,
                                currentDay6.Month,
                                currentDay6.Day,
                            schedule.EndTime.Hour,
                            schedule.EndTime.Minute,
                            schedule.EndTime.Second
                                );
                        }

                        currentDay6 = currentDay6.AddDays(1);
                        if (appendDay6)
                            data.Add(d6);
                    }
                    break;
            }

            return data;
        }

        #endregion
    }
}