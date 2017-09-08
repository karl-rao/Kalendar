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
    public class MsonlineInfo
    {
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
