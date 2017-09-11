using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalendar.Zero.DB.Agent;
using Kalendar.Zero.DB.Entity.Base;
using Kalendar.Zero.Utility.Common;
using log4net.Repository.Hierarchy;

namespace Kalendar.Zero.Data.Domain
{
    public class AccountHelper
    {
        private static readonly log4net.ILog Logger 
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Account Get(int id)
        {
            if (id == 0)
                return new Account();
            var cacheKey = Utility.Config.CacheKeyPrefix + ".acc." + id;
            var model = Utility.Common.Cache.Get<Account>(cacheKey);

            if (model == null)
            {
                return Refresh(id);
            }

            return model;
        }

        public static Account Refresh(int id)
        {
            var cacheKey = Utility.Config.CacheKeyPrefix + ".acc." + id;
            var model = new Account(id);

            Utility.Common.Cache.Insert(cacheKey, model);

            return model;
        }

        public static Account Refresh(DB.Entity.Base.AccountPO entity)
        {
            var cacheKey = Utility.Config.CacheKeyPrefix + ".acc." + entity.Id;
            var model = new Account(entity);

            Utility.Common.Cache.Insert(cacheKey, model);

            return model;
        }

        public static AccountPO SaveAccount(
            ChannelPO channel,
            AccountPO account,
            AccountAvatarsPO avatar,
            List<AccountContactsPO> contacts,
            List<AccountMessagesPO> messages,
            List<SchedulePO> schedules   
            )
        {
            var now = DateTime.Now;

            var trans = new Transaction();
            try
            {
                trans.Begin();

                #region DBOP

                var bllAccount = new DB.Agent.MssqlHelper<AccountPO>();
                var bllAccountAvatars = new DB.Agent.MssqlHelper<AccountAvatarsPO>();
                var bllAccountContacts= new DB.Agent.MssqlHelper<AccountContactsPO>();
                var bllAccountMessages= new DB.Agent.MssqlHelper<AccountMessagesPO>();
                var bllProject= new DB.Agent.MssqlHelper<ProjectPO>();
                var bllSchedule= new DB.Agent.MssqlHelper<SchedulePO>();

                #region avatar and account

                var condition =
                    account == null
                        ? string.Format("ChannelId={0} AND ChannelIdentity='{1}'", avatar.ChannelId,
                            avatar.ChannelIdentity)
                        : string.Format("ChannelId={0} AND ChannelIdentity='{1}' AND AccountId={2}", avatar.ChannelId,
                            avatar.ChannelIdentity, account.Id);
                
                var avatarExists = bllAccountAvatars.FindSingle(condition, trans.DbConnection, trans.DbTrans);
                if (avatarExists == null)
                {
                    if (account == null)
                    {
                        account = new DB.Entity.Base.AccountPO
                        {
                            NickName = avatar.DisplayName,
                            Valid = true,
                            UpdateTime = now,
                            CreateTime = now,
                            LastSignin=now,
                            MobileVerifyTime = now,
                            EmailVerifyTime=now
                        };
                        Logger.Debug(account.SerializeXml());
                        account = bllAccount.Insert(account, trans.DbConnection, trans.DbTrans);
                    }

                    avatarExists = new AccountAvatarsPO
                    {
                        ChannelId = avatar.ChannelId,
                        ChannelIdentity = avatar.ChannelIdentity,
                        DisplayName = avatar.DisplayName,
                        Code = avatar.Code,
                        Token = avatar.Token,
                        TokenGenerated = avatar.TokenGenerated,
                        TokenExpires = avatar.TokenExpires,
                        UpdateTime = avatar.UpdateTime,
                        RefreshToken = avatar.RefreshToken,
                        AccountId = account.Id,
                        Valid=true,
                        CreateTime=now,
                        SynchroTime=now,
                        SynchroDuration = 600
                    };

                    avatarExists.UpdateTime = now;
                    Logger.Debug(avatarExists.SerializeXml());

                    avatar = bllAccountAvatars.Insert(avatarExists, trans.DbConnection, trans.DbTrans);
                }
                else
                {
                    avatar.Id = avatarExists.Id;
                    avatar.AccountId = avatarExists.AccountId;
                    avatar.Valid = avatarExists.Valid;

                    bllAccountAvatars.Update(avatarExists,avatar, trans.DbConnection, trans.DbTrans);

                    account = bllAccount.FindById(avatar.AccountId, trans.DbConnection, trans.DbTrans);
                }

                Utility.DataCache.AccountAvatars.InitCache(account.Id);

                #endregion

                var all = Refresh(account);

                #region contacts

                foreach (var a in contacts)
                {
                    var a0 =
                        all.Contacts.FindLast(
                            o =>
                                o.ChannelId == a.ChannelId && o.ChannelIdentity == a.ChannelIdentity &&
                                o.AccountId == account.Id);
                    if (a0 == null)
                    {
                        a0=new AccountContactsPO
                        {
                            AccountId=account.Id,
                            ChannelId=a.ChannelId,
                            ChannelIdentity=a.ChannelIdentity,
                            DisplayName = a.DisplayName,
                            Detail=a.Detail,
                            CreateTime=now,
                            Valid=true,
                            UpdateTime=now
                        };
                        a0 = bllAccountContacts.Insert(a0,  trans.DbConnection, trans.DbTrans);
                    }
                    else
                    {
                        a0.DisplayName = a.DisplayName;
                        a0.Detail = a.Detail;
                        a0.UpdateTime = now;

                        bllAccountContacts.Update(a,a0,  trans.DbConnection, trans.DbTrans);
                    }
                }
                Utility.DataCache.AccountContacts.InitCache(account.Id);

                #endregion

                #region messages

                foreach (var b in messages)
                {
                    var b0 =
                        all.Messages.FindLast(
                            o =>
                                o.ChannelId == b.ChannelId
                                && o.ChannelIdentity == b.ChannelIdentity &&
                                o.ToAccountId == account.Id);
                    if (b0 == null)
                    {
                        b0 = new AccountMessagesPO
                        {
                            ToAccountId=account.Id,
                            ChannelId = b.ChannelId,
                            ChannelIdentity = b.ChannelIdentity,
                            MessageType = b.MessageType,
                            MessageSubject = b.MessageSubject,
                            MessageContent = b.MessageContent,
                            CreateTime = b.CreateTime,
                            Valid = true,
                            Weblink=b.Weblink,
                            UpdateTime = now
                        };
                        b0 = bllAccountMessages.Insert(b0,  trans.DbConnection, trans.DbTrans);
                    }
                    else
                    {
                        b0.MessageType = b.MessageType;
                        b0.MessageSubject = b.MessageSubject;
                        b0.MessageContent = b.MessageContent;
                        b0.Weblink = b.Weblink;
                        b0.UpdateTime = now;
                        b0.CreateTime = b.CreateTime;

                        bllAccountMessages.Update(b,b0,  trans.DbConnection, trans.DbTrans);
                    }
                }
                Utility.DataCache.AccountMessages.InitCache(account.Id);

                #endregion

                #region p&s

                var projectCode = avatar.Code + "." + account.Id + "." + channel.Id;
                var projectExists = Utility.DataCache.Project.CacheList().FindLast(o =>
                    o.ChannelId == channel.Id
                    && o.CreatorAccountId == account.Id
                    && o.ProjectCode == projectCode);
                
                var projectSchedules = new List<SchedulePO>();

                if (projectExists == null)
                {
                    projectExists = new ProjectPO
                    {
                        ProjectCode = projectCode,
                        Valid = true,
                        CreateTime = now,
                        UpdateTime = now,
                        ProjectName = avatar.DisplayName + "@" + channel.ChannelName,
                        ChannelId = channel.Id,
                        CreatorAccountId = account.Id,
                        EnterDeadline=now
                    };
                    Logger.Debug(projectExists.SerializeXml());
                    projectExists = bllProject.Insert(projectExists,  trans.DbConnection, trans.DbTrans);
                }
                else
                {
                    projectSchedules = Utility.DataCache.Schedule.CacheList(projectExists.Id);
                }

                Utility.DataCache.Project.InitCache();

                foreach (var c in schedules)
                {
                    var c0 =
                        projectSchedules.FindLast(
                            o =>
                                o.ProjectId == projectExists.Id
                                && o.ScheduleIdentity == c.ScheduleIdentity);
                    if (c0 == null)
                    {
                        c0 = new SchedulePO
                        {
                            ProjectId = projectExists.Id,
                            ScheduleIdentity = c.ScheduleIdentity,
                            Cycle = c.Cycle,
                            ScheduleTitle = c.ScheduleTitle,
                            ScheduleLead = c.ScheduleLead,
                            BeginDate = c.BeginDate,
                            BeginTime = c.BeginTime,
                            EndDate = c.EndDate,
                            EndTime = c.EndTime,
                            Weblink = c.Weblink,
                            CreateTime = now,
                            Valid = true,
                            UpdateTime = now
                        };
                        c0 = bllSchedule.Insert(c0,  trans.DbConnection, trans.DbTrans);
                    }
                    else
                    {
                        c0.Cycle = c.Cycle;
                        c0.ScheduleTitle = c.ScheduleTitle;
                        c0.ScheduleLead = c.ScheduleLead;
                        c0.BeginDate = c.BeginDate;
                        c0.BeginTime = c.BeginTime;
                        c0.EndDate = c.EndDate;
                        c0.EndTime = c.EndTime;
                        c0.Weblink = c.Weblink;
                        c0.UpdateTime = now;

                        bllSchedule.Update(c,c0,  trans.DbConnection, trans.DbTrans);
                    }
                }
                Utility.DataCache.Schedule.InitCache(projectExists.Id);

                #endregion

                #endregion
                trans.Commit();

                Logger.Info("处理完结");
            }
            catch (Exception ex)
            {
                trans.RollBack();
                Logger.Error(ex);
            }
            finally
            {
                trans.Dispose();
            }

            return account;
        }
    }
}
