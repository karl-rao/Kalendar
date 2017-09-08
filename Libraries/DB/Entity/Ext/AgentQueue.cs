using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kalendar.Zero.DB.Entity.Ext
{
    /// <summary>
    /// 服务器任务队列
    /// </summary>
    public class AgentQueue
         : Base.BaseEntity
    {
        /// <summary>
        /// 渠道信息
        /// </summary>
        public Base.ChannelPO Channel { get; set; }

        /// <summary>
        /// 用户身份
        /// </summary>
        public Base.AccountAvatarsPO Avatar { get; set; }
    }
}
