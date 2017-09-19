using System;

namespace Kalendar.Zero.ApiTerminal.Entities
{
    /// <summary>
    /// 用户身份
    /// </summary>
    public class Avatar
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public int ChannelId { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string ChannelIdentity { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 令牌生成时间
        /// </summary>
        public DateTime TokenGenerated { get; set; }

        /// <summary>
        /// 令牌过期时间
        /// </summary>
        public DateTime TokenExpires { get; set; }

        /// <summary>
        /// 上次同步时间
        /// </summary>
        public DateTime SynchroTime { get; set; }
    }
}
