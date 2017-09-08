using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.Data.Clients.Response
{
    [Serializable]
    [DataContract]
    public class MsonlineTime
    {
        [DataMember(Name = "dateTime")]
        public string DateTime { get; set; }

        [DataMember(Name = "timeZone")]
        public string TimeZone { get; set; }
    }
}
