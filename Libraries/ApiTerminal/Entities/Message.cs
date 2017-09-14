using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Entities
{
    /// <summary>
    /// 用户消息
    /// </summary>
    public class Message
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
        /// 消息主题
        /// </summary>
        public string MessageSubject { get; set; }

        /// <summary>
        /// 消息正文
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Weblink { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
