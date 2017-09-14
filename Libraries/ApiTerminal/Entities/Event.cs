using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Entities
{
    /// <summary>
    /// 日历事件
    /// </summary>
    public class Event
    {
        /// <summary>
        /// 用户身份
        /// </summary>
        public int AvatarId { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string ChannelIdentity { get; set; }

        /// <summary>
        /// 事件主题
        /// </summary>
        public string EventSubject { get; set; }

        /// <summary>
        /// 事件导语
        /// </summary>
        public string EventLead { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// 执行周期
        /// </summary>
        public int Cycle { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Weblink { get; set;  }
    }
}
