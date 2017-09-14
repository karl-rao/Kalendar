using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Entities
{
    /// <summary>
    /// 联系人
    /// </summary>
    public class Contact
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
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 明细信息
        /// </summary>
        public string Detail { get; set; }
    }
}
