using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Entities
{
    public class Message
    {
        public int AvatarId { get; set; }

        public string ChannelIdentity { get; set; }

        public string MessageSubject { get; set; }

        public string MessageContent { get; set; }

        public string Weblink { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
