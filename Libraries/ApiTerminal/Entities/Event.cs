using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Entities
{
    public class Event
    {
        public int AvatarId { get; set; }

        public string ChannelIdentity { get; set; }

        public string EventSubject { get; set; }

        public string EventLead { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
        public int Cycle { get; set; }

        public string Weblink { get; set;  }
    }
}
