using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.Imagent
{
    public class FileTimer
        :System.Timers.Timer
    {
        private static readonly log4net.ILog Logger 
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 执行周期
        /// </summary>
        public DB.Entity.Ext.AgentQueue AgentQueue { get; set; }

        public void GenerateQueue()
        {
            try
            {
                Utility.DataCache.AgentQueue.Append(AgentQueue);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
