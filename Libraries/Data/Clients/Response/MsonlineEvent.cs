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
    public class MsonlineEvent
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "bodyPreview")]
        public string BodyPreview { get; set; }
        
        [DataMember(Name = "start")]
        public MsonlineTime Start { get; set; }

        [DataMember(Name = "end")]
        public MsonlineTime End { get; set; }

        [DataMember(Name = "location")]
        public MsonlineLocation Location { get; set; }

        [DataMember(Name = "showAs")]
        public string ShowAs { get; set; }

        [DataMember(Name = "webLink")]
        public string WebLink { get; set; }

        [DataMember(Name = "recurrence")]
        public MsonlineRecurrence Recurrence { get; set; }
        
    }
}
