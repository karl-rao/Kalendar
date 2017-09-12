using System;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using Kalendar.Zero.Data.Domain;
using Kalendar.Zero.DB.Entity.Ext;
using Kalendar.Zero.Utility.Common;

namespace Kalendar.Zero.Imagent
{
    public partial class Aproxy : ServiceBase
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Aproxy()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Info("SERVICE START...");

            var path = System.IO.Path.GetDirectoryName(Utility.Config.CacheFolder);
            Logger.Info(path);

            var fw = new FileSystemWatcher
            {
                Path = path,
                Filter = "*AgentQueue",
                IncludeSubdirectories = true,
                NotifyFilter =
                    NotifyFilters.Attributes
                    | NotifyFilters.CreationTime
                    | NotifyFilters.DirectoryName
                    | NotifyFilters.FileName
                    | NotifyFilters.LastAccess
                    | NotifyFilters.LastWrite
                    | NotifyFilters.Security
                    | NotifyFilters.Size,
                EnableRaisingEvents = true
            };

            fw.Created += new FileSystemEventHandler(OnProcess);
            fw.Changed += new FileSystemEventHandler(OnProcess);
            fw.Deleted += new FileSystemEventHandler(OnProcess);
            
            fw.Renamed += new RenamedEventHandler(OnRenamed);

            LoadTimers();
        }

        private void LoadTimers()
        {
            try
            {
                var dbHelper = new CacheHelper<Zero.DB.Entity.Base.AccountAvatarsPO>();
                var condition =
                    " Valid=1 AND ([SynchroTime] IS NULL OR DateDiff(second,[SynchroTime],GETDATE())>=[SynchroDuration])";

                var avatars = dbHelper.Find(condition);
                if (avatars != null && avatars.Any())
                {
                    foreach (var accountAvatarsPo in avatars)
                    {
                        var t = new FileTimer
                        {
                            AgentQueue = new AgentQueue
                            {
                                Avatar = accountAvatarsPo,
                                Channel = Utility.DataCache.Channel.GetEntity(accountAvatarsPo.ChannelId)
                            },
                            Interval = accountAvatarsPo.SynchroDuration*1000
                        };

                        t.Elapsed += FileTimerElapsed;
                        t.Enabled = true;
                        t.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void FileTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var timer = (FileTimer) sender;

            if (!Utility.DataCache.AgentQueue.Exists(timer.AgentQueue))
            {
                timer.Enabled = false;
                timer.Stop();

                timer.GenerateQueue();

                timer.Enabled = true;
                timer.Start();
            }
        }

        #region Util

        private static void OnProcess(object source, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    OnCreated(source, e);
                    break;
                case WatcherChangeTypes.Changed:
                    OnChanged(source, e);
                    break;
                case WatcherChangeTypes.Deleted:
                    OnDeleted(source, e);
                    break;
            }
        }

        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            Logger.Info($"文件新建事件处理逻辑 {e.ChangeType}  {e.FullPath}  {e.Name}");

            new QueueParser(e.FullPath).Execute();
        }
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Logger.Info($"文件改变事件处理逻辑{e.ChangeType}  {e.FullPath}  {e.Name}");
        }

        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            Logger.Info($"文件删除事件处理逻辑{e.ChangeType}  {e.FullPath}   {e.Name}");
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            Logger.Info($"文件重命名事件处理逻辑{e.ChangeType}  {e.FullPath}  {e.Name}");
        }

        #endregion

        protected override void OnStop()
        {
            Logger.Info("SERVICE STOPED");
        }
    }
}
