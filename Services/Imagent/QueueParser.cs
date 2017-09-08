using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalendar.Zero.Utility.Common;

namespace Kalendar.Zero.Imagent
{
    public class QueueParser
    {
        private static readonly log4net.ILog Logger
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string JsonFile { get; set; }

        public QueueParser()
        {
            
        }

        public QueueParser(string file)
        {
            JsonFile = file;
        }

        public void Execute()
        {
            Logger.Info("START PARSE"+ JsonFile);
            var fileKey = System.IO.Path.GetFileNameWithoutExtension(JsonFile);
            var data = Utility.Common.Cache.Get<DB.Entity.Ext.AgentQueue>(fileKey);
            Logger.Info(data.SerializeXml());
            var page = 1;
            var contacts=new List<DB.Entity.Base.AccountContactsPO>();
            var messages=new List<DB.Entity.Base.AccountMessagesPO>();
            var schedules=new List<DB.Entity.Base.SchedulePO>();

            switch (data.Channel.ChannelSymbol)
            {
                case 1:
                    try
                    {
                        #region 同步数据

                        var ms = new Data.Clients.MsonlineHelper {Avatar = data.Avatar, Channel = data.Channel};
                        ms.Avatar = ms.RefreshToken(data.Avatar.RefreshToken);
                        ms.Avatar = ms.ReadAvatar();

                        var msContacts = ms.ReadContacts(page);
                        while (msContacts.Any())
                        {
                            foreach (var accountContactsPo in msContacts)
                            {
                                if (contacts.All(o => o.ChannelIdentity != accountContactsPo.ChannelIdentity))
                                {
                                    contacts.Add(accountContactsPo);
                                }
                            }
                            page++;
                            msContacts = ms.ReadContacts(page);
                        }

                        page = 1;

                        var msMessages = ms.ReadMessages(page);
                        while (msMessages.Any())
                        {
                            foreach (var accountMessagesPo in msMessages)
                            {
                                if (messages.All(o => o.ChannelIdentity != accountMessagesPo.ChannelIdentity))
                                {
                                    messages.Add(accountMessagesPo);
                                }
                            }
                            page++;
                            msMessages = ms.ReadMessages(page);
                        }

                        page = 1;

                        var msSchedules = ms.ReadSchedules(page);
                        while (msSchedules.Any())
                        {
                            foreach (var schedulePo in msSchedules)
                            {
                                if (schedules.All(o => o.ScheduleIdentity != schedulePo.ScheduleIdentity))
                                {
                                    schedules.Add(schedulePo);
                                }
                            }
                            page++;
                            msSchedules = ms.ReadSchedules(page);
                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                    break;

                case 2:
                    try
                    {
                        #region 同步数据

                        var google = new Data.Clients.GoogleHelper { Avatar = data.Avatar, Channel = data.Channel };
                        google.Avatar = google.RefreshToken(data.Avatar.RefreshToken);
                        google.Avatar = google.ReadAvatar();

                        var googleContacts = google.ReadContacts(page);
                        while (googleContacts.Any())
                        {
                            foreach (var accountContactsPo in googleContacts)
                            {
                                if (contacts.All(o => o.ChannelIdentity != accountContactsPo.ChannelIdentity))
                                {
                                    contacts.Add(accountContactsPo);
                                }
                            }
                            page++;
                            googleContacts = google.ReadContacts(page);
                        }

                        page = 1;

                        var googleMessages = google.ReadMessages(page);
                        while (googleMessages.Any())
                        {
                            foreach (var accountMessagesPo in googleMessages)
                            {
                                if (messages.All(o => o.ChannelIdentity != accountMessagesPo.ChannelIdentity))
                                {
                                    messages.Add(accountMessagesPo);
                                }
                            }
                            page++;
                            googleMessages = google.ReadMessages(page);
                        }

                        page = 1;

                        var googleSchedules = google.ReadSchedules(page);
                        while (googleSchedules.Any())
                        {
                            foreach (var schedulePo in googleSchedules)
                            {
                                if (schedules.All(o => o.ScheduleIdentity != schedulePo.ScheduleIdentity))
                                {
                                    schedules.Add(schedulePo);
                                }
                            }
                            page++;
                            googleSchedules = google.ReadSchedules(page);
                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                    break;
            }

            try
            {
                Data.Domain.AccountHelper.SaveAccount(
                    data.Channel,
                    null,
                    data.Avatar,
                    contacts,
                    messages,
                    schedules
                    );
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            //System.IO.File.Move(JsonFile, JsonFile.Replace("queue.json","q.txt"));
        }
    }
}
