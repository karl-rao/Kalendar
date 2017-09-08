using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients.Response
{
    [Serializable]
    [DataContract]
    public class MsonlineMessage
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "receivedDateTime")]
        public string ReceivedDateTime { get; set; }
        
        [DataMember(Name = "subject")]
        public string Subject { get; set; }
        
        [DataMember(Name = "importance")]
        public string Importance { get; set; }

        [DataMember(Name = "webLink")]
        public string WebLink { get; set; }
        
        [DataMember(Name = "sender")]
        public MsonlineAnyone Sender { get; set; }

    }
}
