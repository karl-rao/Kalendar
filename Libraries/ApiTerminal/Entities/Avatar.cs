using System;

namespace Kalendar.Zero.ApiTerminal.Entities
{
    public class Avatar
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }

        public string ChannelIdentity { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }
        public string Token { get; set; }

        public string RefreshToken { get; set; }
        public DateTime TokenGenerated { get; set; }

        public DateTime TokenExpires { get; set; }
    }
}
