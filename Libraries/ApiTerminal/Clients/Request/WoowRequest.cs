using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients.Request
{
    public class WoowRequest
    {
        public int ChannelSymbol { get; set; }

        public string Method { get; set; }

        public Entities.Channel Channel { get; set; }

        public Entities.Avatar Avatar { get; set; }

        public string Data { get; set; }
    }
}
