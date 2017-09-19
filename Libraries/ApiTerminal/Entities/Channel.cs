using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Entities
{
    /// <summary>
    /// 渠道
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 应用Secret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 应用Key
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 回叫地址
        /// </summary>
        public string CodeCallback { get; set; }

        /// <summary>
        /// 渠道参数
        /// </summary>
        public string Parameters { get; set; }

    }
}
