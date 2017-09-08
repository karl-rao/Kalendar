using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Kalendar.Zero.Data.Entities
{
    [Serializable]
    [DataContract]
    public class KalendarEvents
    {
        [DataMember(Name = "success")]
        public int Success { get; set; }

        [DataMember(Name = "result")]
        public List<KalendarEvent> Result { get; set; }
    }
}
